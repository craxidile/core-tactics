using DG.Tweening;
using Taiga.Core.Unity.CharacterAnimation.Base;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation
{
    public abstract class BaseM09SpecialAttack01 : BaseCharacterTimelineController
    {
        public override CharacterAnimator CharacterAnimator { get; set; }
        public override MapDirection Direction { get; set; }
        public override AnimationClip AttackAnimationClip { get; set; }

        public void SetAttackAction(string action)
        {
            switch (action)
            {
                case "reset":
                    OnReset();
                    break;
                case "step_forward":
                    OnStepToward();
                    break;
                case "attack_01":
                    OnAttack1();
                    break;
                case "attack_02":
                    OnAttack2();
                    break;
                case "attack_03":
                    OnAttack3();
                    break;
                case "attack_04":
                    OnAttack4();
                    break;
                case "attack_05":
                    OnAttack5();
                    break;
                case "down":
                    OnDown();
                    break;
                case "freeze":
                    OnFreeze();
                    break;

            }
        }

        protected abstract void OnReset();
        protected abstract void OnStepToward();
        protected abstract void OnAttack1();
        protected abstract void OnAttack2();
        protected abstract void OnAttack3();
        protected abstract void OnAttack4();
        protected abstract void OnAttack5();
        protected abstract void OnDown();
        protected abstract void OnFreeze();
    }
}
