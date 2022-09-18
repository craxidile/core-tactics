using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Taiga.Core.Character.Attack;
using Taiga.Core.Unity.Character;
using Taiga.Core.Unity.CharacterAnimation;
using Taiga.Core.Unity.CharacterBanner;
using Taiga.Core.Unity.Demo.Providers;
using Taiga.Core.Unity.MapCell;
using UnityEngine;

namespace Taiga.Core.Unity.Preset
{
    [CreateAssetMenu(fileName = "TaigaAssetsPreset", menuName = "Taiga Preset/Game Demo", order = 1)]
    public class GameDemoPreset : SerializedScriptableObject, IGameDemoPreset
    {
        public int mode;
        public int Mode => mode;
    }

}
