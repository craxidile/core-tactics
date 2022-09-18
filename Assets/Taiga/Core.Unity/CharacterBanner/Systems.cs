using Taiga.Core.Character;

namespace Taiga.Core.Unity.CharacterBanner
{

    public class CharacterBannerSystems : Feature
    {
        public CharacterBannerSystems(Contexts contexts)
        {
            Add(new UpdateBanner_WhenCharacterTurnChanged(contexts));
            Add(new AnimateBanner_WhenCurrentTurnCharacterPostAnimationStarted(contexts));
        }
    }

}
