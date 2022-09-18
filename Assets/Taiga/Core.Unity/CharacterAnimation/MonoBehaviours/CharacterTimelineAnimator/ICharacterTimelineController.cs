using DG.Tweening;

namespace Taiga.Core.Unity.CharacterAnimation.Base
{
    public interface ICharacterTimelineController
    {
         Sequence OnStart();
         void OnEnd();
    }
}
