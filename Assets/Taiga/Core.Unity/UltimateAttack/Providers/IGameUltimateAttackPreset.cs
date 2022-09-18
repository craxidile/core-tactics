using UnityEngine;

namespace Taiga.Core.Unity.UltimateAttack.Providers
{
    public interface IGameAudioPreset : IProvider
    {
        public AudioClip GetAudioSourceByName(string name);
    }
}
