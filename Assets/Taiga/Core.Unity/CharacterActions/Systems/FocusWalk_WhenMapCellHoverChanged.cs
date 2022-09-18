using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.CharacterAction;
using Taiga.Core.CharacterAction.Move;
using Taiga.Core.Map;
using Taiga.Core.Unity.MapInput;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterActions
{

    public class FocusWalk_WhenMapCellHoverChanged : ReactiveSystem<GameEntity>
    {
        GameContext game;

        public FocusWalk_WhenMapCellHoverChanged(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var action = game
                .AsCharacterActionContext()
                .CurrentAction
                .Value;
            var actionMove = action.AsCharacterAction_Move();

            var mapInputContext = game.AsMapInputContext();
            if (mapInputContext.PointingMapCell == null)
            {
                actionMove.Unfocus();
                return;
            }

            var hoverCell = mapInputContext
                .PointingMapCell
                .Value;
            var possiblePositions = actionMove.PossiblePositions;
            if (possiblePositions.Contains(hoverCell.Position))
            {
                actionMove.Focus(hoverCell.Position);
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            var action = game.AsCharacterActionContext().CurrentAction;
            return action?.ActionType == CharacterActionType.Move && !action.Value.IsExecuted;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                MapInputEvents.OnCellHover.AddedOrRemoved()
            );
        }
    }
}
