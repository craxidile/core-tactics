using System;
using Taiga.Core.Unity.Effect.Providers;
using UnityEngine;

namespace Taiga.Core.Unity.Preset
{

    public class TextEffectProvider : MonoBehaviour
    {
        [SerializeField] private GameTextEffectPreset textEffectPreset;
        internal static TextEffectProvider Instance { get; private set; }

        void Awake()
        {
            Instance = this;
        }

        internal Sprite GetTextEffectByType(HitTextEffectType type)
        {
            return textEffectPreset.GetTextEffectByType(type);
        }
    }
}
