using System.Collections.Generic;
using Entitas;
using Taiga.Core.Map;

namespace Taiga.Core.Unity.MapInput
{
    internal class UpdateMapCellHover_WhenPointerPositionChanged : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public UpdateMapCellHover_WhenPointerPositionChanged(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var mapContext = game.AsMapContext();
            if (game.hasMap_PointerPosition)
            {
                var newHoverPosition = game
                    .map_PointerPosition
                    .position
                    .Round();

                var hoverCellEntity = game.mapCell_PointerHoverEntity;

                if (hoverCellEntity != null)
                {
                    var hoverCell = hoverCellEntity.AsMapCell(game);
                    if (hoverCell.Position == newHoverPosition)
                    {
                        return;
                    }
                    else
                    {
                        hoverCellEntity.isMapCell_PointerHover = false;
                    }
                }

                if (mapContext.IsMapCellExist(newHoverPosition))
                {
                    var cell = mapContext.GetMapCell(newHoverPosition);
                    cell.entity.isMapCell_PointerHover = true;
                }
            }
            else
            {
                game.isMapCell_PointerHover = false;
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Map_PointerPosition.AddedOrRemoved());
        }
    }
}
