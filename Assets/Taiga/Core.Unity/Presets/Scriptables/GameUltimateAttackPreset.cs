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
using UnityEngine;

namespace Taiga.Core.Unity.Preset
{

    [CreateAssetMenu(fileName = "TaigaAssetsPreset", menuName = "Taiga Preset/Game Ultimate Attack", order = 1)]
    public class GameUltimateAttackPreset : SerializedScriptableObject, IGameUltimateAttackPreset
    {
        [Serializable]
        public struct AnimationKeyPair
        {
            public string name;
            public AnimationClip animation;
        }

        public List<AnimationKeyPair> animations;

        public AnimationClip GetAnimationByName(string name)
        {
            return animations.FirstOrDefault(kp => kp.name == name).animation;
        }
    }

}
