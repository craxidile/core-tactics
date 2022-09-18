using Taiga.Core.Unity.CharacterAnimation.Base;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.C06._00
{
    public abstract class BaseC06NormalAttack00 : BaseCharacterTimelineController
    {
        public override CharacterAnimator CharacterAnimator { get; set; }
        public override MapDirection Direction { get; set; }
        public override AnimationClip AttackAnimationClip { get; set; }

        public void SetAttackAction(string action)
        {
            switch (action)
            {
                case "strike" :
                    OnStrike();
                    break;
                case "reset" :
                    OnReset();
                    break;
            }
        }

        protected abstract void OnReset();

        protected abstract void OnStrike();
    }
}