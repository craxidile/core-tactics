using Taiga.Core.Unity.CharacterAnimation.Base;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.S04._00
{
    public abstract class BaseS04NormalAttack00 : BaseCharacterTimelineController
    {
        private const string NormalPunchClip = "NormalPunchClip";
        private const string HardPunchClip = "HardPunchClip";
        private const string ChargeClip = "Charge";
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