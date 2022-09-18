using System.Collections.Generic;
using System.Linq;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Placement;
using Taiga.Core.CharacterSequence;
using Taiga.Core.Unity.Audio.Providers;
using Taiga.Core.Unity.Camera;
using Taiga.Core.Unity.CharacterAnimation;
using Taiga.Core.Unity.Demo.Providers;
using Taiga.Core.Unity.Preset;
using Taiga.Core.Unity.UltimateAttack.Providers;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterSequenceAnimation
{
    internal class AnimateDamaged_WhenAnimationStarted : ReactiveSystem<GameEntity>
    {
        public GameContext game;

        public AnimateDamaged_WhenAnimationStarted(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var characterContext = game.AsCharacterContext();
            var sequenceContext = game.AsCharacterSequenceContext();
            foreach (var entity in entities)
            {
                var sequence = entity
                    .AsCharacterSequence(game);

                var damagedSequence = entity
                    .AsCharacterSequence_Damaged(game);

                var character = characterContext
                    .GetCharacter(sequence.CharacterId)
                    .Value;

                var characterAnimator = character
                    .AsGameObject()
                    .GetComponent<CharacterAnimator>();
                characterAnimator.AudioPreset = game.GetProvider<IGameAudioPreset>();
                characterAnimator.UltimateAttackPreset = game.GetProvider<IGameUltimateAttackPreset>();

                if (damagedSequence.SourceSequence.IsAttack())
                {
                    var attackSequence = damagedSequence
                        .SourceSequence
                        .AsCharacterSequence_Attack();

                    var attackCharacterId = attackSequence
                        .AsCharacterSequence()
                        .CharacterId;

                    var direction = attackSequence.Direction;
                    var attackerCharacter = characterContext
                        .GetCharacter(attackCharacterId);

                    var attackerPosition = attackerCharacter
                        .AsCharacter_Placement()
                        .Position;

                    var attackerDirection = attackSequence.Direction;
                    var characterPosition = character
                        .AsCharacter_Placement()
                        .Position;

                    var attackPositionOffset = (characterPosition - attackerPosition)
                        .NormalizeByDirection(attackerDirection.GetNormalizeDirection());

                    characterAnimator.Damaged(
                        attackType: attackSequence.AttackType,
                        bumpDirection: damagedSequence.BumpDirection,
                        endPosition: damagedSequence.EndPosition,
                        bumpPosition: damagedSequence.BumpPosition,
                        offsetRelativeToAttacker: attackPositionOffset,
                        onBump: () => HandleDamagedBumped(damagedSequence),
                        attackerName: attackerCharacter.Value.Name,
                        attackerPosition: attackerPosition,
                        attackerCharacter: attackerCharacter.AsCharacter()
                    );
                }
                else
                {
                    characterAnimator.BumpDamaged(
                        bumpDirection: damagedSequence.BumpDirection,
                        endPosition: damagedSequence.EndPosition,
                        bumpPosition: damagedSequence.BumpPosition,
                        onBump: () => HandleDamagedBumped(damagedSequence)
                    );
                }

                characterAnimator.SetFinishCallbackOnce(
                    () => sequence.entity.isCharacterSequence_Animating = false
                );
            }
        }

        void HandleDamagedBumped(CharacterSequenceEntity_Damaged damaged)
        {

            var consequences = damaged.AsCharacterSequence().Consequences;
            foreach (var consequence in consequences)
            {
                consequence.entity.isCharacterSequence_Animating = true;
            }
        }

        protected override bool Filter(GameEntity entity) => entity
            .AsCharacterSequence(game)
            .IsDamaged() && entity.camera_AttackStatusChanged.status == CameraAttackStatus.Ready ;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            // return context.CreateCollector(GameMatcher.CharacterSequence_Animating);
            return context.CreateCollector(GameMatcher.Camera_AttackStatusChanged);
        }
    }

}
