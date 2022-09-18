using Taiga.Core.Unity.CharacterAnimation.Base;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.M08._01
{
    public abstract class BaseM08SpecialAttack01 : BaseCharacterTimelineController
    {
        private float _originalAnimationSpeed;
        public override CharacterAnimator CharacterAnimator { get; set; }
        public override MapDirection Direction { get; set; }
        public override AnimationClip AttackAnimationClip { get; set; }

        protected ThrowableGreenTeaBottle GreenTeaBottle => ThrowableGreenTeaBottle.Instance;

        public void SetAttackAction(string action)
        {
            switch (action)
            {
                case "throw":
                    OnThrow();
                    break;
                case "zoom_out":
                    OnZoomOut();
                    break;
                case "wait":
                    OnWait();
                    break;
                case "reset":
                    OnReset();
                    break;
            }
        }

        protected abstract void OnReset();
        protected abstract void OnZoomOut();
        protected abstract void OnWait();
        protected abstract void OnThrow();

        protected void PauseAnimation()
        {
            _originalAnimationSpeed = AttackAnimation[AnimationClipName].speed;
            AttackAnimation[AnimationClipName].speed = 0;
        }

        protected void ResumeAnimation()
        {
            AttackAnimation[AnimationClipName].speed = _originalAnimationSpeed;
        }
    }
}