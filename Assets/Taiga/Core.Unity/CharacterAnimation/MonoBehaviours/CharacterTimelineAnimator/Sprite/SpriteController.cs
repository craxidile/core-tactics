using DG.Tweening;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.Base.Sprite
{
    public class SpriteController
    {
        private BaseCharacterTimelineController _characterTimelineController;

        public SpriteController(BaseCharacterTimelineController characterTimelineController)
        {
            _characterTimelineController = characterTimelineController;
        }

        internal void ShakeCharacter()
        {
            _characterTimelineController.CharacterAnimator.body.DOShakePosition(0.15f, new Vector3(0.08f, 0, 0.08f), 28,
                20, false, false);
        }

        internal void MaskCharacter(bool shortDelay = true)
        {
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(shortDelay ? 0f : 0.18f);
            sequence.AppendCallback(() =>
            {
                _characterTimelineController.CharacterAnimator.bodyRenderer.color = new Color(1f, 0.5f, 0.5f, 1f);
            });
            sequence.AppendInterval(shortDelay ? 0.15f : 0.25f);
            sequence.AppendCallback(() =>
            {
                _characterTimelineController.CharacterAnimator.bodyRenderer.color = Color.white;
            });
            sequence.Play();
        }

        internal void ShakeAndMaskCharacter(bool shortDelay = true)
        {
            ShakeCharacter();
            MaskCharacter(shortDelay);
        }

        internal void SetAnimatorTrigger(string trigger)
        {
            _characterTimelineController.CharacterAnimator.animator.SetTrigger(trigger);
        }
    }
}