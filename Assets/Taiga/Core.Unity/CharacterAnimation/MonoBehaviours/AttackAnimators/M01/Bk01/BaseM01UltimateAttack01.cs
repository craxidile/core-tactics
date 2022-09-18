using DG.Tweening;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.Bk01
{
    public abstract class BaseM01UltimateAttack01 : MonoBehaviour
    {
        protected const string StateName = "Attack_Special_1";
        protected Animation AttackingAnimation;
        public AnimationClip Animation { get; set; }
        public abstract CharacterAnimator CharacterAnimator { get; set; }

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
        internal void SetEventNormallizedTime(float value) => AttackingAnimation[StateName].normalizedTime = value;

        internal abstract void OnPrepare();
        internal abstract Sequence Initialize();
        internal abstract void OnReset();
        internal abstract void OnRun();
        internal abstract void OnAttack1();
        internal abstract void OnDamaged();
        internal abstract void OnGetUp();
    }
}
