using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Taiga.Core.Character;
using Taiga.Core.Character.Attack;
using Taiga.Core.CharacterAction;
using Taiga.Core.CharacterFactory;
using Taiga.Core.CharacterTurn;
using Taiga.Core.Map;
using UnityEngine;

namespace Taiga.Core.Unity.Preset
{
    [CreateAssetMenu(fileName = "TaigaGameGlobalPreset", menuName = "Taiga Preset/Game Global", order = 1)]
    public class GameGlobalPreset : SerializedScriptableObject,
        ICharacterArchitypePreset,
        ICharacterTurnPreset,
        ICharacterAttackPreset
    {
        [Header("Character Architype")] [OdinSerialize]
        Dictionary<string, CharacterArchitypeProperty> architypePropertyById;
        public CharacterArchitypeProperty GetCharacterArchitypeProperty(string architypeId)
        {
            return architypePropertyById[architypeId];
        }

        [Header("Turn Affinity")] public int maxTurnAffinity;
        public int MaxTurnAffinity => maxTurnAffinity;

        public float criticalAttackFactor;
        public float CriticalAttackFactor => criticalAttackFactor;

        public int moveAttackAffinityUsage;
        public int attackAffinityUsage;
        public int specialAttackAffinityUsage;
        public int endTurnAffinityUsage;

        public int GetAffinityUsage(CharacterActionType actionType)
        {
            switch (actionType)
            {
                case CharacterActionType.Attack:
                    return attackAffinityUsage;
                case CharacterActionType.SpecialAttack:
                    return specialAttackAffinityUsage;
                case CharacterActionType.Move:
                    return moveAttackAffinityUsage;
                case CharacterActionType.EndTurn:
                default:
                    return endTurnAffinityUsage;
            }
        }

        [Header("Attack")] public int bumpDamage;
        public int BumpDamage => bumpDamage;

        public Dictionary<(CharacterGroup attacker, CharacterGroup damaged), float> attackRateByCharacterGroupPair;
        public bool GetGroupRelativeAttackRate(CharacterGroup attacker, CharacterGroup damaged, out float rate)
        {
            return attackRateByCharacterGroupPair
                .TryGetValue(
                    (attacker, damaged),
                    out rate
                );
        }

        [Header("Special Attack")] public int specialAttackPointPerUnit;
        public int SpecialAttackPointPerUnit => specialAttackPointPerUnit;

        public int moveSpecialAttackPointGain;
        public int MoveSpecialAttackPointGain => moveSpecialAttackPointGain;

        public int attackSpecialAttackPointGain;
        public int AttackSpecialAttackPointGain => attackSpecialAttackPointGain;

        public int damagedSpecialAttackPointGain;
        public int DamagedSpecialAttackPointGain => damagedSpecialAttackPointGain;

        public int killSpecialAttackPointGain;
        public int KillSpecialAttackPointGain => killSpecialAttackPointGain;

    }

}
