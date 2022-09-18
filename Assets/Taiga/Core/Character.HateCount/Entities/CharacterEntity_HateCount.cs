using System;

namespace Taiga.Core.Character.HateCount
{
    public static class CharacterEntity_HateCountExtensions
    {
        public static CharacterEntity_HateCount AsCharacter_HateCount(this IGameScopedEntity entity)
        {
            return new CharacterEntity_HateCount(entity.context, entity.entity);
        }

        public static CharacterEntity_HateCount AsCharacter_HateCount(this GameEntity entity,
            GameContext context)
        {
            return new CharacterEntity_HateCount(context, entity);
        }
    }

    public struct CharacterEntity_HateCount : IGameScopedEntity
    {
        public GameContext context { get; private set; }
        public GameEntity entity { get; private set; }

        public int Point => entity.character_HateCountPoint.value;


        public CharacterEntity_HateCount(GameContext context, GameEntity entity)
        {
            this.context = context;
            this.entity = entity;
        }

        public void SetupHateCount(int point = 0)
        {
            entity.AddCharacter_HateCountPoint(point);
        }

        public void AddPoint(int pointToAdd)
        {
            var updatedPoint = Math.Max(0, Point + pointToAdd);
            entity.ReplaceCharacter_SpecialAttackCurrentPoint(updatedPoint);
        }

        public void IncreasePoint() => AddPoint(1);
        public void DecreasePoint() => AddPoint(-1);
        public void ResetPoint() => entity.ReplaceCharacter_SpecialAttackPoint(0);
    }
}