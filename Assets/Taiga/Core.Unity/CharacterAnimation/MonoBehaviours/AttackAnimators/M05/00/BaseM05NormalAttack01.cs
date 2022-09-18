using Taiga.Core.Unity.CharacterAnimation.Base;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.M05._00
{
    public abstract class BaseM05NormalAttack01: BaseCharacterTimelineController
    {
        public override CharacterAnimator CharacterAnimator { get; set; }
        public override MapDirection Direction { get; set; }
        public override AnimationClip AttackAnimationClip { get; set; }

        public void SetAttackAction(string action)
        {
            switch (action)
            {
                case "charge" :
                    OnCharge();
                    break;
                case "strike1" :
                    OnStrike1();
                    break;
                case "strike2" :
                    OnStrike2();
                    break;
                case "strike3" :
                    OnStrike3();
                    break;
                case "reset" :
                    OnReset();
                    break;
            }
        }

        protected abstract void OnCharge();

        protected abstract void OnReset();

        protected abstract void OnStrike1();
        
        protected abstract void OnStrike2();
        protected abstract void OnStrike3();
    }
}