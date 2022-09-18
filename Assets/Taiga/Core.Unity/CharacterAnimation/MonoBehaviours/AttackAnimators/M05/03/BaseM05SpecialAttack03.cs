using Taiga.Core.Unity.CharacterAnimation.Base;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.M05._03
{
    public abstract class BaseM05SpecialAttack03 : BaseCharacterTimelineController
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
                case "prepare":
                    OnPrepare();
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
                case "freeze":
                    OnFreeze();
                    break;
                case "attack_04":
                    OnAttack4();
                    break;
                case "unfreeze":
                    OnUnfreeze();
                    break;
                case "down":
                    OnDown();
                    break;
            }
        }

        internal abstract void OnPrepare();
        internal abstract void OnReset();
        internal abstract void OnAttack1();
        internal abstract void OnAttack2();
        internal abstract void OnAttack3();
        internal abstract void OnFreeze();
        internal abstract void OnAttack4();
        internal abstract void OnUnfreeze();
        internal abstract void OnDown();
    }
}