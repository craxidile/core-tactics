using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;

namespace Taiga.Core.CharacterAction.Attack
{
    internal class CreateSelectionPossiblePositions_WhenSelectionChanged : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public CreateSelectionPossiblePositions_WhenSelectionChanged(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var selection = entity.characterAction_AttackStrategySelection.value;

                entity.ReplaceCharacterAction_AttackStrategySelection_PossiblePositions(
                    selection.SelectablePositions
                );
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.CharacterAction_AttackStrategySelection
            );
        }

    }
}
