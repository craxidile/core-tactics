using System;
using DG.Tweening;
using Taiga.Core.Unity.Demo.Providers;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation
{
    public abstract class BaseM09UltimateAttack01 : MonoBehaviour
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

            }
        }

        internal abstract Sequence Initialize();
        internal abstract void OnReset();
        internal abstract void OnStepToward();
        internal abstract void OnAttack1();
        internal abstract void OnAttack2();
        internal abstract void OnAttack3();
        internal abstract void OnAttack4();
        internal abstract void OnAttack5();
        internal abstract void OnDown();
    }
}
