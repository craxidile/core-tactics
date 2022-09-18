namespace Taiga.Core.Unity.CharacterAnimation.Base.FocusLine
{
    public class FocusAnimationController
    {
        private BaseCharacterTimelineController _characterTimelineController;
        
        public FocusAnimationController(BaseCharacterTimelineController characterTimelineController)
        {
            _characterTimelineController = characterTimelineController;
        }

        internal void ShowFocusLine(bool isLightFocus, float duration)
        {
            FocusLineController.Instance.ShowFocusLine(isLightFocus, duration);
        }
    }
}