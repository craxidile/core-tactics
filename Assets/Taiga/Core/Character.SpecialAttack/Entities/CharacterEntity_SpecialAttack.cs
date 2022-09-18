using System;
using UnityEngine;

namespace Taiga.Core.Character.SpecialAttack.Entities
{
    public static class CharacterEntity_SpecialAttackExtensions
    {
        public static CharacterEntity_SpecialAttack AsCharacter_SpecialAttack(this IGameScopedEntity entity)
        {
            return new CharacterEntity_SpecialAttack(entity.context, entity.entity);
        }

        public static CharacterEntity_SpecialAttack AsCharacter_SpecialAttack(this GameEntity entity,
            GameContext context)
        {
            return new CharacterEntity_SpecialAttack(context, entity);
        }
    }

    public struct CharacterEntity_SpecialAttack : IGameScopedEntity
    {
        public const int MaxPoint = 300;
        public const int GaugePoint = MaxPoint / 3;

        public enum SpecialAttackPointType
        {
            WalkPoint = 20,
            AttackPoint = 40,
            WaitPoint = 20,
            DamagedPoint = 10
        }

        public GameContext context { get; private set; }
        public GameEntity entity { get; private set; }

        public int Point => entity.character_SpecialAttackCurrentPoint.value;
        
        public CharacterEntity_SpecialAttack(GameContext context, GameEntity entity)
        {
            this.context = context;
            this.entity = entity;
        }

        public void SetupSpecialAttack(int point = 0)
        {
            entity.AddCharacter_SpecialAttackCurrentPoint(point);
        }

        public void AddPoint(int pointToAdd)
        {
            var updatedPoint = Math.Max(0, Math.Min(MaxPoint, Point + pointToAdd));
            entity.ReplaceCharacter_SpecialAttackCurrentPoint(updatedPoint);
        }

        public void AddPoint(SpecialAttackPointType type) => AddPoint((int)type);

    }
}