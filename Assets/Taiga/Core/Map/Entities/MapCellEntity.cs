using UnityEngine;

namespace Taiga.Core.Map
{

    public static class MapCellEntityExtensions
    {
        public static MapCellEntity AsMapCell(this IGameScopedEntity entity)
        {
            return new MapCellEntity(entity.context, entity.entity);
        }

        public static MapCellEntity AsMapCell(this GameEntity entity, GameContext context)
        {
            return new MapCellEntity(context, entity);
        }
    }

    public struct MapCellEntity : IGameScopedEntity
    {
        public MapCellEntity(GameContext context, GameEntity entity)
        {
            this.context = context;
            this.entity = entity;
        }

        public GameContext context { get; private set; }

        public GameEntity entity { get; private set; }

        public Vector2Int Position => entity.mapCell.position;
    }
}
