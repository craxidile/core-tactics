using DG.Tweening;
using Taiga.Core.Unity.CharacterAnimation.Base;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.M01._01
{
    public abstract class BaseM01SpecialAttack01 : BaseCharacterTimelineController
    {
        public override CharacterAnimator CharacterAnimator { get; set; }
        public override MapDirection Direction { get; set; }
        public override AnimationClip AttackAnimationClip { get; set; }


        protected const string StateName = "Attack_Special_1";

        public void SetAttackAction(string action)
        {
            switch (action)
            {
                case "reset":
                    OnReset();
                    break;
                case "prepare":
                    OnPrepare();
                    break;
                case "run":
                    OnRun();
                    break;
                case "attack_01":
                    OnAttack1();
                    break;
                case "damage":
                    OnDamaged();
                    break;
                case "getup":
                    OnGetUp();
                    break;
            }
        }

        internal void SetEventNormalizedTime(float value) => AttackAnimation["animation"].normalizedTime = value;

        internal abstract void OnPrepare();
        internal abstract void OnReset();
        internal abstract void OnRun();
        internal abstract void OnAttack1();
        internal abstract void OnDamaged();
        internal abstract void OnGetUp();
    }
}