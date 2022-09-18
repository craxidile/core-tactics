using UnityEngine;

namespace Taiga.Core.Character.Health
{

    public static class CharacterDamageEntityExtensions
    {
        public static CharacterDamageEntity AsCharacterDamage(this IGameScopedEntity entity)
        {
            return new CharacterDamageEntity(entity.context, entity.entity);
        }

        public static CharacterDamageEntity AsCharacterDamage(this GameEntity entity, GameContext context)
        {
            return new CharacterDamageEntity(context, entity);
        }
    }

    public struct CharacterDamageEntity : IGameScopedEntity
    {
        public CharacterDamageEntity(GameContext context, GameEntity entity)
        {
            this.context = context;
            this.entity = entity;
        }

        public GameContext context { get; private set; }

        public GameEntity entity { get; private set; }

        public int Damage => entity.characterDamage.damage;

        public int CharacterId => entity.characterDamage.characterId;

        public int? SourceCharacterId => entity.characterDamage.sourceCharacterId;

    }
}
