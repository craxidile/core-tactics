using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Taiga.Core.Character.Attack;
using Taiga.Core.Unity.Character;
using Taiga.Core.Unity.CharacterAnimation;
using Taiga.Core.Unity.CharacterBanner;
using Taiga.Core.Unity.Demo.Providers;
using Taiga.Core.Unity.MapCell;
using Taiga.Core.Unity.UltimateAttack.Providers;
using UnityEngine;

namespace Taiga.Core.Unity.Preset
{

    [CreateAssetMenu(fileName = "TaigaAssetsPreset", menuName = "Taiga Preset/Game Audio", order = 1)]
    public class GameAudioPreset : SerializedScriptableObject, IGameAudioPreset
    {
        [Serializable]
        public struct AudioKeyPair
        {
            public string name;
            public AudioClip audio;
        }

        public List<AudioKeyPair> effectAudios;

        public AudioClip GetAudioSourceByName(string name)
        {
            return effectAudios.FirstOrDefault(kp => kp.name == name).audio;
        }
    }

}
