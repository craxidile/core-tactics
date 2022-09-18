using System.Collections.Generic;
using Taiga.Core.Map;
using UnityEngine;

namespace Taiga.Core.Unity.MapInput
{
    public static class MapInputContextExtensions
    {
        public static MapInputContext AsMapInputContext(this GameContext game)
        {
            return new MapInputContext(game);
        }
    }

    public struct MapInputContext
    {
        GameContext game;

        public MapInputContext(GameContext game)
        {
            this.game = game;
        }

        public Vector2? PointerPosition => game.map_PointerPositionEntity?.map_PointerPosition?.position;

        public MapCellEntity? PointingMapCell => game.mapCell_PointerHoverEntity?.AsMapCell(game);
    }

}
