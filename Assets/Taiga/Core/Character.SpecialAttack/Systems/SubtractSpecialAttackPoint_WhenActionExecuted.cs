using System.Collections.Generic;
using System.Linq;
using Entitas;
using Taiga.Core.Character.Attack;
using Taiga.Core.Character.SpecialAttack.Entities;
using Taiga.Core.CharacterAction;
using Taiga.Core.CharacterAction.Attack;
using UnityEngine;

namespace Taiga.Core.Character.SpecialAttack.Systems
{
    public class SubtractSpecialAttackPoint_WhenActionExecuted : ReactiveSystem<GameEntity>
    {
        private GameContext game;

        public SubtractSpecialAttackPoint_WhenActionExecuted(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            Debug.Log($">>special_attack_point<< subtract point");
            foreach (var entity in entities)
            {
                var attackType = entity
                    .characterAction_AttackType
                    .value;

                var action = entity
                    .AsCharacterAction(game);

                var characterId = action.CharacterId;

                var character = game
                    .AsCharacterContext()
                    .GetCharacter(characterId)
                    .Value;

                var attackUnitBounds = character
                    .AsCharacter_Attack()
                    .SpecialAttackUnitBounds;

                if (attackUnitBounds.All(aub => aub?.attackType != attackType)) return;

                var attackUnitBound = attackUnitBounds.First(aub => aub?.attackType == attackType);
                var unitUsage = attackUnitBound?.unitUsage;

                Debug.Log($">>special_attack_point<< expected_character {characterId} {attackType} {unitUsage}");

                var specialAttack = character.AsCharacter_SpecialAttack();
                Debug.Log($">>special_attack_point<< subtract from {specialAttack.Point} {action}");
                specialAttack.AddPoint(-unitUsage.Value);
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.AsCharacterAction(game).ActionType == CharacterActionType.SpecialAttack;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(CharacterActionEvents.OnActionExecute);
        }
    }
}