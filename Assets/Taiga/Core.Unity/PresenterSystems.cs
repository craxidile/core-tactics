using Entitas;
using Taiga.Core.Unity.Camera;
using Taiga.Core.Unity.Character;
using Taiga.Core.Unity.CharacterActions;
using Taiga.Core.Unity.CharacterAnimation;
using Taiga.Core.Unity.CharacterBanner;
using Taiga.Core.Unity.CharacterSequenceAnimation;
using Taiga.Core.Unity.MapCell;
using Taiga.Core.Unity.MapCellHighlight;
using Taiga.Core.Unity.MapInput;
using Taiga.Core.Unity.CharacterCooldownRank;

namespace Taiga.Core.Unity
{
    public class PresenterSystems : Systems
    {
        public PresenterSystems(Contexts contexts)
        {
            Add(new GameObjectLinkSystems(contexts));
            Add(new MapCellSystems(contexts));
            Add(new MapCellHighlightSystems(contexts));
            Add(new MapInputSystems(contexts));
            Add(new CharacterSystems(contexts));
            Add(new CharacterAnimationSystems(contexts));
            Add(new CharacterActionsSystems(contexts));
            Add(new CharacterBannerSystems(contexts));
            Add(new CharacterSequenceAnimationSystems(contexts));
            Add(new CharacterCooldownRankSystems(contexts));
            Add(new CameraSystems(contexts));
        }
    }
}
