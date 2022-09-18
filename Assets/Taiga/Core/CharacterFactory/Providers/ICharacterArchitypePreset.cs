using System;
using System.Collections.Generic;
using Sirenix.Serialization;
using Taiga.Core.Character;
using Taiga.Core.Character.Attack;
using Taiga.Core.Unity.CharacterAnimation.Base;
using UnityEngine;

namespace Taiga.Core.CharacterFactory
{
    [Serializable]
    public struct SpecialAttackUnitBound
    {
        public AttackType attackType;
        public int minUnit;
        public int unitUsage;
        // public int? maxSpecialAttackUnit;
    }

    [Serializable]
    public struct SpecialAttackControllers
    {
        public AnimationClip timeline;
        public Type attackStrategy;
        public Type attackingController;
        public Type damagedController;
    }

    [Serializable]
    public struct CharacterArchitypeProperty
    {

        public string name;
        public CharacterGroup group;
        public int health;
        public int moveLength;
        public int turnAffinityRagainRate;

        public AttackType attackType;
        public int attack;
        public int defend;
        public int accuracy;
        public int evasion;
        public int critical;

        public AttackType? specialAttackType;
        public Dictionary<AttackType, SpecialAttackControllers> attackControllers;
        public List<SpecialAttackUnitBound?> specialAttackUnitBounds;
        public int specialAttack;
        public int maxSpecialAttackUnit;
        public int specialAttackUnitUsage;
    }

    public interface ICharacterArchitypePreset : IProvider
    {
        CharacterArchitypeProperty GetCharacterArchitypeProperty(string architypeId);
    }
}
