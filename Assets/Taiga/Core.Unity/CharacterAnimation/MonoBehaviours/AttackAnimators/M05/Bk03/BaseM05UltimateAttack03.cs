using System;
using DG.Tweening;
using Taiga.Core.Unity.Demo.Providers;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation
{
    public abstract class BaseM05UltimateAttack03 : MonoBehaviour
    {
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
        internal abstract Sequence Initialize();
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
