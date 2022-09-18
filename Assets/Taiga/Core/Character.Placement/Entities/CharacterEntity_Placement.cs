using UnityEngine;

namespace Taiga.Core.Character.Placement
{

    public static class CharacterEntity_PlacementExtensions
    {
        public static CharacterEntity_Placement AsCharacter_Placement(this IGameScopedEntity entity)
        {
            return new CharacterEntity_Placement(entity.context, entity.entity);
        }

        public static CharacterEntity_Placement AsCharacter_Placement(this GameEntity entity, GameContext context)
        {
            return new CharacterEntity_Placement(context, entity);
        }
    }

    public struct CharacterEntity_Placement : IGameScopedEntity
    {
        public CharacterEntity_Placement(GameContext context, GameEntity entity)
        {
            this.context = context;
            this.entity = entity;
        }

        public GameContext context { get; private set; }

        public GameEntity entity { get; private set; }

        public Vector2Int Position => entity.character_Position.value;

        public MapDirection Facing => entity.character_FacingDirection.value;

        public void SetPlacement(
            Vector2Int position,
            MapDirection facing
        )
        {
            if (!entity.hasCharacter_Position || entity.character_Position.value != position)
            {
                entity.ReplaceCharacter_Position(position);
            }

            if (!entity.hasCharacter_FacingDirection || entity.character_FacingDirection.value != facing)
            {
                entity.ReplaceCharacter_FacingDirection(facing);
            }
        }
    }
}
