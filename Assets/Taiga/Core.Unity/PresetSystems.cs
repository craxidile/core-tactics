using Entitas;
using Taiga.Core.Unity.Preset;

namespace Taiga.Core.Unity
{
    public class PresetSystems : Feature
    {
        public PresetSystems(Contexts contexts)
        {
            Add(new ScriptablePresetSystems<GameGlobalPreset>(contexts));
            Add(new ScriptablePresetSystems<GamePreset>(contexts));
            Add(new ScriptablePresetSystems<GameAudioPreset>(contexts));
            Add(new ScriptablePresetSystems<GameThrowableSpritePreset>(contexts));
            Add(new ScriptablePresetSystems<GameAssetsPreset>(contexts));
            Add(new ScriptablePresetSystems<GameViewPreset>(contexts));
            Add(new ScriptablePresetSystems<GameDemoPreset>(contexts));
            Add(new ScriptablePresetSystems<GameUltimateAttackPreset>(contexts));
            Add(new CreateAttackStrategyPreset(contexts));
        }
    }
}
