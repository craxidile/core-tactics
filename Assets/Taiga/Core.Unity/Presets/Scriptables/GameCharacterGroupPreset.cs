using System.Collections.Generic;
using Sirenix.OdinInspector;
using Taiga.Core.Character;
using UnityEngine;

namespace Taiga.Core.Unity.Preset
{
    [CreateAssetMenu(fileName = "TaigaAssetsPreset", menuName = "Taiga Preset/Character Group Sprite", order = 1)]
    public class GameCharacterGroupPreset : SerializedScriptableObject
    {
        [SerializeField] private Dictionary<CharacterGroup, Sprite> bloodTypeDisplayDict;

        internal Sprite GetBloodSpriteByGroup(CharacterGroup type) => bloodTypeDisplayDict[type];
    }
}
