using UnityEngine;

namespace Taiga.Core.Unity.Audio.Providers
{
    public interface IGameUltimateAttackPreset : IProvider
    {
        public AnimationClip GetAnimationByName(string name);
    }
}
