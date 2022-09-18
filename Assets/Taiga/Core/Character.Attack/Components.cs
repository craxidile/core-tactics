using System;
using System.Collections.Generic;
using Entitas;
using Taiga.Core.CharacterFactory;

namespace Taiga.Core.Character.Attack
{
    public enum DamageLevel
    {
        Normal,
        Miss,
        Critical
    }

    public enum AttackSelectType
    {
        AttackSelect1,
        AttackSelect2,
        AttackSelect3,
        AttackSelect4,
        AttackSelect5,
        AttackSelect6,
        SpecialAttackSelect1,
        SpecialAttackSelect2,
        SpecialAttackSelect3,
        SpecialAttackSelect4,
        SpecialAttackSelect5,
        SpecialAttackSelect6
    }

    public enum AttackType
    {
        Attack1,
        Attack2,
        Attack3,
        Attack4,
        Attack5,
        Attack6,
        SpecialAttack1,
        SpecialAttack2,
        SpecialAttack3,
        SpecialAttack4,
        SpecialAttack5,
        SpecialAttack6
    }

    public sealed class Character_AttackProperty : IComponent
    {
        public AttackType attackType;
        public int attack;
        public int defend;
        public int accuracy;
        public int evasion;
        public int critical;
    }

    public sealed class Character_SpecialAttackProperty : IComponent
    {
        public AttackType attackType;
        public int attack;
        public int specialAttackUnitUsage;
        public int maxSpecialAttackUnit;
        public Dictionary<AttackType, SpecialAttackControllers> attackControllers;
        public List<SpecialAttackUnitBound?> specialAttackUnitBounds;
    }

    public sealed class Character_SpecialAttackPoint : IComponent
    {
        public int value;
    }

    public sealed class CharacterAction_SpecialAttackReady : IComponent
    {
        public AttackType specialAttackType;
    }
}