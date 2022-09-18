using UnityEngine;

namespace Taiga.Core.Character.Health
{

    public static class CharacterEntity_HealthExtensions
    {
        public static CharacterEntity_Health AsCharacter_Health(this IGameScopedEntity entity)
        {
            return new CharacterEntity_Health(entity.context, entity.entity);
        }

        public static CharacterEntity_Health AsCharacter_Health(this GameEntity entity, GameContext context)
        {
            return new CharacterEntity_Health(context, entity);
        }
    }

    public struct CharacterEntity_Health : IGameScopedEntity
    {
        public CharacterEntity_Health(GameContext context, GameEntity entity)
        {
            this.context = context;
            this.entity = entity;
        }

        public GameContext context { get; private set; }

        public GameEntity entity { get; private set; }

        public bool IsAlive => !entity.isCharacter_HealthDead;

        public int Health => entity.character_HealthPoint.value;

        public int MaxHealth => entity.character_HealthProperty.maxHealth;

        public void SetupHealth(int point)
        {
            entity.AddCharacter_HealthProperty(
                newMaxHealth: point
            );
            entity.AddCharacter_HealthPoint(point);
        }

        public void Damage(
            int point,
            int? sourceCharacterId = null
        )
        {
            var character = this.AsCharacter();
            var damageEntity = context.CreateEntity();
            damageEntity.AddCharacterDamage(
                newSourceCharacterId: sourceCharacterId,
                newCharacterId: character.Id,
                newDamage: point
            );
        }
    }
}
