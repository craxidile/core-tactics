using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Taiga.Core.Character.Attack;
using Taiga.Core.Unity.Audio.Providers;
using Taiga.Core.Unity.Character;
using Taiga.Core.Unity.CharacterAnimation;
using Taiga.Core.Unity.CharacterBanner;
using Taiga.Core.Unity.Demo.Providers;
using Taiga.Core.Unity.MapCell;
using Taiga.Core.Unity.UltimateAttack.Providers;
using UnityEngine;

namespace Taiga.Core.Unity.Preset
{

    [CreateAssetMenu(fileName = "TaigaAssetsPreset", menuName = "Taiga Preset/Throwable Sprite", order = 1)]
    public class GameThrowableSpritePreset : SerializedScriptableObject, IThrowableSpritePreset
    {
        [Serializable]
        public struct ThrowableSpriteKeyPair
        {
            public string name;
            public Sprite sprite;
        }

        public List<ThrowableSpriteKeyPair> throwableSprites;

        public Sprite GetThrowableSpriteSourceByName(string name)
        {
            return throwableSprites.FirstOrDefault(ts => ts.name == name).sprite;
        }
        
    }

}
