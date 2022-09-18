using Taiga.Core.Unity.CharacterAnimation.Base;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.M05._02
{
    public abstract class BaseM05SpecialAttack02 : BaseCharacterTimelineController
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
                case "attack_nw":
                    OnAttackNW();
                    break;
                case "attack_n":
                    OnAttackN();
                    break;
                case "attack_ne":
                    OnAttackNE();
                    break;
                case "attack_e":
                    OnAttackE();
                    break;
                case "attack_se":
                    OnAttackSE();
                    break;
                case "attack_s":
                    OnAttackS();
                    break;
                case "attack_sw":
                    OnAttackSW();
                    break;
                case "attack_w":
                    OnAttackW();
                    break;
                case "down":
                    OnDown();
                    break;
            }
        }

        internal abstract void OnAttackNW();
        internal abstract void OnAttackN();
        internal abstract void OnAttackNE();
        internal abstract void OnAttackE();
        internal abstract void OnAttackSE();
        internal abstract void OnAttackS();
        internal abstract void OnAttackSW();
        internal abstract void OnAttackW();
        internal abstract void OnPrepare();
        internal abstract void OnReset();
        internal abstract void OnDown();
    }
}