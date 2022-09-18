using Taiga.Core.Unity.CharacterAnimation.Base;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation
{
    public abstract class BaseM07SpecialAttack01 : BaseCharacterTimelineController
    {
        public override CharacterAnimator CharacterAnimator { get; set; }
        public override MapDirection Direction { get; set; }
        public override AnimationClip AttackAnimationClip { get; set; }

        public void SetAttackAction(string action)
        {
            switch (action)
            {
                case "attack_01":
                    OnAttack01();
                    break;
                case "reset":
                    OnReset();
                    break;
            }
        }

        protected abstract void OnAttack01();
        protected abstract void OnReset();
    }
}