using System.Collections.Generic;
using UnityEngine;

namespace Taiga.Core.Unity.Character
{
    public interface ICharacterPrefabPreset : IProvider
    {
        GameObject CharacterPrefab { get; }
    }
}
