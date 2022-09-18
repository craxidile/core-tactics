using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Placement;
using Taiga.Core.CharacterAction;
using Taiga.Core.CharacterAction.Attack;
using Taiga.Core.Unity.MapInput;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterActions
{
    public class FocusAttack_WhenMapCellHoverChanged : ReactiveSystem<GameEntity>
    {
        GameContext game;

        public FocusAttack_WhenMapCellHoverChanged(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var action = game
                .AsCharacterActionContext()
                .CurrentAction.Value;
            var actionAttack = action.AsCharacterAction_Attack();

            var mapInputContext = game.AsMapInputContext();

            if (mapInputContext.PointingMapCell == null)
            {
                //actionMove.Unfocus();
                return;
            }


            var pointerPosition = mapInputContext.PointerPosition.Value;
            var pointingCell = mapInputContext.PointingMapCell.Value;

            var character = game
                .AsCharacterContext()
                .GetCharacter(action.CharacterId);

            var characterPosition = character
                .AsCharacter_Placement()
                .Position;

            var direction = (pointerPosition - characterPosition.ToVector2())
                .GetAngle()
                .GetNearestMapDirection();

            actionAttack.Focus(
                direction: direction,
                position: pointingCell.Position
            );
        }

        protected override bool Filter(GameEntity entity)
        {
            var action = game.AsCharacterActionContext().CurrentAction;
            return (action?.ActionType == CharacterActionType.Attack || action?.ActionType == CharacterActionType.SpecialAttack)
                && !action.Value.IsExecuted;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                MapInputEvents.OnCellHover.AddedOrRemoved(),
                MapInputEvents.OnPointer.AddedOrRemoved()
            );
        }
    }
}
