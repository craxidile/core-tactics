using System.Collections.Generic;
using Entitas;
using Taiga.Core.CharacterAction;
using Taiga.Core.Unity.MapInput;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterActions
{
    public class ExecuteMoveAction_WhenCellTrigger : ReactiveSystem<GameEntity>
    {
        GameContext game;

        public ExecuteMoveAction_WhenCellTrigger(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var actionContext = game.AsCharacterActionContext();
            var action = actionContext.CurrentAction.Value;
            action.Execute();
        }

        protected override bool Filter(GameEntity entity)
        {
            var action = game.AsCharacterActionContext().CurrentAction;
            if (action == null)
            {
                return false;
            }

            if (!action.Value.CanExecute)
            {
                return false;
            }

            var actionType = action.Value.ActionType;

            return actionType == CharacterActionType.Move;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                MapInputEvents.OnCellTrigger
            );
        }
    }
}
