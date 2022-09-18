using System.Collections.Generic;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.Effect
{
    public interface ICharacterAttackEffectAssetPreset : IProvider
    {
        public Dictionary<AttackEffectType, RuntimeAnimatorController> GetEffectAssetsMap();
    }
}
