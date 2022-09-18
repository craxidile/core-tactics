using System.Collections.Generic;
using Taiga.Core.Character;
using UnityEngine;

namespace Taiga.Core.Map
{
    public static class MapContextExtensions
    {
        public static MapContext AsMapContext(this GameContext game)
        {
            return new MapContext(game);
        }
    }

    public struct MapContext
    {
        GameContext game;

        public MapContext(GameContext game)
        {
            this.game = game;
        }

        public bool IsMapCellExist(Vector2Int position)
        {
            return game.GetEntityWithMapCell(position) != null;
        }

        public MapCellEntity GetMapCell(Vector2Int position)
        {
            var entity = game.GetEntityWithMapCell(position);
            return entity.AsMapCell(game);
        }

        public bool Raycast(
            Vector2Int origin,
            MapDirection direction,
            int length,
            out MapCellEntity lastMapCell
        )
        {
            var unitVector = direction.GetUnitVector();
            lastMapCell = GetMapCell(origin);
            for (var i = 1; i <= length; i++)
            {
                var position = origin + (unitVector * i);
                if (!IsMapCellExist(position))
                {
                    return true;
                }
                lastMapCell = GetMapCell(position);
            }
            lastMapCell = default;
            return false;
        }
    }

}
