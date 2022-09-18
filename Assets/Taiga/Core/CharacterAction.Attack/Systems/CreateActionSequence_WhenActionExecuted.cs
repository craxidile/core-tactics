using System.Collections.Generic;
using System.Linq;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Attack;
using Taiga.Core.Character.Health;
using Taiga.Core.Character.Placement;
using Taiga.Core.CharacterSequence;
using Taiga.Core.Map;
using UnityEngine;
using UnityEngine.Assertions;

namespace Taiga.Core.CharacterAction.Attack
{
    internal class CreateActionSequence_WhenActionExecuted : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public CreateActionSequence_WhenActionExecuted(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var action = entity.AsCharacterAction(game);
                var actionAttack = action.AsCharacterAction_Attack();
                var sequenceContext = game.AsCharacterSequenceContext();
                var attacktype = entity
                    .characterAction_AttackType
                    .value;

                var attackDirection = entity
                    .characterAction_AttackPrediction
                    .attackDirection;

                var damageInfos = entity
                    .characterAction_AttackPrediction
                    .damageInfos;

                var attackSequence = sequenceContext.CreateInitialAttack(
                    characterId: action.CharacterId,
                    attackType: attacktype,
                    direction: attackDirection
                );

                foreach (var damageInfo in damageInfos)
                {
                    CreateDamaged(attackSequence, damageInfo);
                }

                if (entity.hasCharacterAction_SpecialAttackReady) entity.RemoveCharacterAction_SpecialAttackReady();
            }
        }

        public void CreateDamaged(
            CharacterSequenceEntity_Attack attackSequence,
            CharacterDamageInfo damageInfo
        )
        {

            if (damageInfo.level == DamageLevel.Miss)
            {
                attackSequence.CreateBlocked(
                    damageInfo.characterId,
                    bumpDirection: damageInfo.fromDirection
                );
                return;
            }

            var damagedSequence = attackSequence.CreateDamaged(
                characterId: damageInfo.characterId,
                damage: damageInfo.damage,
                bumpDirection: damageInfo.fromDirection
            );

            var damagedCharacter = damagedSequence
                .AsCharacterSequence()
                .Character;

            var damagedCharacterPosition = damagedCharacter
                .AsCharacter_Placement()
                .Position;

            if (damageInfo.bumpInfo == null)
            {
                return;
            }

            var bumpInfo = damageInfo.bumpInfo.Value;
            if (bumpInfo.type == BumpType.Fly)
            {
                damagedSequence.EndPosition = bumpInfo.toPosition;
            }

            if (bumpInfo.type == BumpType.Drag)
            {
                var bumpDelta = damagedCharacterPosition - bumpInfo.toPosition;
                var bumpDirection = bumpDelta
                    .GetMapDirection()
                    .GetOppsite();
                var bumpLength = Mathf.RoundToInt(bumpDelta.magnitude);

                CreateDragBumped(
                    damagedSequence,
                    bumpDirection: bumpDirection,
                    bumpLength: bumpLength,
                    bumpStreakCount: 0
                );
            }
        }

        void CreateDragBumped(
            CharacterSequenceEntity_Damaged damagedSequence,
            MapDirection bumpDirection,
            int bumpLength,
            int bumpStreakCount
        )
        {
            var characterPlacementContext = game.AsCharacterPlacementContext();

            var character = damagedSequence
                .AsCharacterSequence()
                .Character;

            var characterPosition = character
                .AsCharacter_Placement()
                .Position;

            var mapContext = game.AsMapContext();

            var isHitBound = mapContext.Raycast(
                origin: characterPosition,
                direction: bumpDirection,
                length: bumpLength,
                out var mapCell
            );

            if (isHitBound)
            {
                damagedSequence.BumpPosition = mapCell.Position;
                damagedSequence.EndPosition = characterPosition + (bumpDirection.GetUnitVector() * (bumpLength - 1));
                return;
            }

            var isBumped = characterPlacementContext.Raycast(
                origin: characterPosition,
                direction: bumpDirection,
                length: bumpLength,
                out var bumpedCharacter
            );

            if (!isBumped)
            {
                damagedSequence.EndPosition = characterPosition + (bumpDirection.GetUnitVector() * bumpLength);
                return;
            }

            var isBumpedCharacterAlive = bumpedCharacter
                .AsCharacter_Health()
                .IsAlive;

            if (!isBumpedCharacterAlive)
            {
                damagedSequence.BumpPosition = bumpedCharacter
                    .AsCharacter_Placement()
                    .Position;

                damagedSequence.BumpPosition = mapCell.Position;
                damagedSequence.EndPosition = characterPosition + (bumpDirection.GetUnitVector() * (bumpLength - 1));
                return;
            }

            var bumpedCharacterPosition = bumpedCharacter
                .AsCharacter_Placement()
                .Position;

            if (bumpStreakCount == 0)
            {

                var bumpDamage = game
                    .GetProvider<ICharacterAttackPreset>()
                    .BumpDamage;

                var bumpedCharacterDamagedSequence = damagedSequence.CreateDamaged(
                    bumpedCharacter.Id,
                    damage: bumpDamage,
                    bumpDirection: bumpDirection
                );

                CreateDragBumped(
                    bumpedCharacterDamagedSequence,
                    bumpDirection: bumpDirection,
                    bumpStreakCount: bumpStreakCount + 1,
                    bumpLength: 1
                );

                // In case bumped character position doesn't change
                if (bumpedCharacterDamagedSequence.EndPosition == null ||
                    bumpedCharacterDamagedSequence.EndPosition != bumpedCharacterPosition)
                {
                    damagedSequence.EndPosition = characterPosition + (bumpDirection.GetUnitVector() * bumpLength);
                }
                else
                {
                    damagedSequence.BumpPosition = bumpedCharacterPosition;
                    damagedSequence.EndPosition = bumpedCharacterPosition - bumpDirection.GetUnitVector();
                }
            }

            if (bumpStreakCount == 1)
            {
                damagedSequence.CreateBlocked(
                    bumpedCharacter.Id,
                    bumpDirection: bumpDirection
                );

                damagedSequence.EndPosition = characterPosition + (bumpDirection.GetUnitVector() * (bumpLength - 1));
                damagedSequence.BumpPosition = bumpedCharacterPosition;
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            if (!entity.hasCharacterAction_AttackPrediction) return false;
            var actionType = entity.AsCharacterAction(game).ActionType;
            return actionType == CharacterActionType.Attack ||
                (actionType == CharacterActionType.SpecialAttack && entity.hasCharacterAction_SpecialAttackReady);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {

            return context.CreateCollector(
                GameMatcher.AnyOf(
                    CharacterActionEvents.OnActionExecute// ,
                    // CharacterActionAttackEvents.OnSpecialAttackReady
                )
            );
        }

    }

}
