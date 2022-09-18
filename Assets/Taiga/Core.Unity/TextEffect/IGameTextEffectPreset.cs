using UnityEngine;

namespace Taiga.Core.Unity.Effect.Providers
{
    public enum HitTextEffectType
    {
        JP01_Black,
        JP01_White,
        JP02_01,
        JP02_02,
        JP02_03,
        JP03,
        JP04,
        JP05,
        JP06_Black,
        JP06_White
    }

    public interface IGameTextEffectPreset : IProvider
    {
        public Sprite GetTextEffectByType(HitTextEffectType type);
    }
}
