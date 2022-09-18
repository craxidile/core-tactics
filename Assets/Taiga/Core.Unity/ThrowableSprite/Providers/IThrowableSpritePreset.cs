using UnityEngine;

namespace Taiga.Core.Unity.Audio.Providers
{
    public interface IThrowableSpritePreset : IProvider
    {
        public Sprite GetThrowableSpriteSourceByName(string name);
    }
}
