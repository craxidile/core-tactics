using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Taiga.Core.Unity.Effect.Providers;
using UnityEngine;

namespace Taiga.Core.Unity.Preset
{
    [CreateAssetMenu(fileName = "TaigaAssetsPreset", menuName = "Taiga Preset/Game Text Effect", order = 1)]
    public class GameTextEffectPreset : SerializedScriptableObject, IGameTextEffectPreset
    {
        [Serializable]
        public struct SpriteKeyPair
        {
            public HitTextEffectType type;
            public Sprite sprite;
        }

        public List<SpriteKeyPair> textEffects;

        public Sprite GetTextEffectByType(HitTextEffectType type)
        {
            return textEffects.FirstOrDefault(kp => kp.type == type).sprite;
        }
    }

}
