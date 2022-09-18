using DG.Tweening;
using Taiga.Core.Unity.CharacterAnimation.Base;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.M05._01
{
    public abstract class BaseM05SpecialAttack01 : BaseCharacterTimelineController
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
                case "down":
                    OnDown();
                    break;
            }
        }

        internal abstract void OnPrepare();
        internal abstract void OnReset();
        internal abstract void OnAttack1();
        internal abstract void OnDown();
    }
}