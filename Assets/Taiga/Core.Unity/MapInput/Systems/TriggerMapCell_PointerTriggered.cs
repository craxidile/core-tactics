using Entitas;
using Taiga.Core.Map;
using UnityEngine;

namespace Taiga.Core.Unity.MapInput
{
    internal class TriggerMapCell_PointerTriggered : IInitializeSystem
    {

        GameContext game;
        MapGroundInput input;

        public TriggerMapCell_PointerTriggered(Contexts contexts)
        {
            this.game = contexts.game;
        }

        public void Initialize()
        {
            input = GameObject.FindObjectOfType<MapGroundInput>();
            input.OnPointerTrigger += HandleTrigger;
        }

        private void HandleTrigger()
        {
            var entity = game.map_PointerPositionEntity;
            if (entity != null)
            {
                entity.isMap_PointerTrigger = true;
            }

            var cellPosition = input.PointerPosition.Round();
            var mapContext = game.AsMapContext();
            if (mapContext.IsMapCellExist(cellPosition))
            {
                var cell = mapContext.GetMapCell(cellPosition);
                cell.entity.isMapCell_PointerTrigger = true;
            }
        }
    }
}
