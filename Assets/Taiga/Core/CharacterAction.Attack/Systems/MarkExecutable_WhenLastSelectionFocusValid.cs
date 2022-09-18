using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character.Attack;

namespace Taiga.Core.CharacterAction.Attack
{
    internal class MarkExecutable_WhenLastSelectionFocusValid : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public MarkExecutable_WhenLastSelectionFocusValid(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var action = entity.AsCharacterAction(game);
                action.CanExecute = true;
            }
        }

        protected override bool Filter(GameEntity entity) => entity
            .characterAction_AttackStrategySelection
            .value is IAttackStrategy_FinalSelection;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.CharacterAction_AttackStrategySelection_FocusValid
            );
        }

    }
}
