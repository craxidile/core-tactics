using UnityEngine;

namespace Taiga.Core.Character.Move
{

    public static class CharacterEntity_MovementExtensions
    {
        public static CharacterEntity_Movement AsCharacter_Movement(this IGameScopedEntity entity)
        {
            return new CharacterEntity_Movement(entity.context, entity.entity);
        }

        public static CharacterEntity_Movement AsCharacter_Movement(this GameEntity entity, GameContext context)
        {
            return new CharacterEntity_Movement(context, entity);
        }
    }

    public struct CharacterEntity_Movement : IGameScopedEntity
    {
        public CharacterEntity_Movement(GameContext context, GameEntity entity)
        {
            this.context = context;
            this.entity = entity;
        }

        public GameContext context { get; private set; }

        public GameEntity entity { get; private set; }

        public int Length => entity.character_MoveProperty.length;

        public void SetProperty(int length)
        {
            entity.AddCharacter_MoveProperty(newLength: length);
        }
    }
}
