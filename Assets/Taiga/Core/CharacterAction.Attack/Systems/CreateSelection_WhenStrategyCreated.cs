using System.Collections.Generic;
using Entitas;
using UnityEditor;
using UnityEngine;

namespace Taiga.Core.CharacterAction.Attack
{
    internal class CreateSelection_WhenStrategyCreated : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public CreateSelection_WhenStrategyCreated(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var value = entity.characterAction_AttackStrategy.value;
                entity.AddCharacterAction_AttackStrategySelection(value.Selection);
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.CharacterAction_AttackStrategy.Added()
            );
        }

    }
}
