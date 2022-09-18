using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character.SpecialAttack.Entities;
using Taiga.Core.CharacterAction;
using UnityEngine;

namespace Taiga.Core.Character.SpecialAttack.Systems
{
    public class AddAttackPoint_WhenActionExecuted : ReactiveSystem<GameEntity>
    {
        private GameContext game;
        
        public AddAttackPoint_WhenActionExecuted(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var action = entity
                    .AsCharacterAction(game);

                var characterId = action.CharacterId;
                Debug.Log($">>special_attack_point<< expected_character {characterId}");

                var character = game
                    .AsCharacterContext()
                    .GetCharacter(characterId)
                    .Value;

                var specialAttack = character.AsCharacter_SpecialAttack();
                Debug.Log($">>special_attack_point<< add_move");
                specialAttack.AddPoint(CharacterEntity_SpecialAttack.SpecialAttackPointType.AttackPoint);
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.AsCharacterAction(game).ActionType == CharacterActionType.Attack;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(CharacterActionEvents.OnActionExecute);
        }
    }
}