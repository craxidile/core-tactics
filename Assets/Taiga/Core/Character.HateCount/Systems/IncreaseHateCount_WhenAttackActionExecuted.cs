using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character.SpecialAttack.Entities;
using Taiga.Core.CharacterAction;
using UnityEngine;

namespace Taiga.Core.Character.HateCount.Systems
{
    public class IncreaseHateCount_WhenAttackActionExecuted : ReactiveSystem<GameEntity>
    {
        private GameContext game;

        public IncreaseHateCount_WhenAttackActionExecuted(Contexts contexts) : base(contexts.game)
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
                Debug.Log($">>hate_count_point<< expected_character (increase) {characterId}");

                var character = game
                    .AsCharacterContext()
                    .GetCharacter(characterId)
                    .Value;

                var hateCount = character.AsCharacter_HateCount();
                Debug.Log($">>hate_count_point<< increase");
                hateCount.IncreasePoint();
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