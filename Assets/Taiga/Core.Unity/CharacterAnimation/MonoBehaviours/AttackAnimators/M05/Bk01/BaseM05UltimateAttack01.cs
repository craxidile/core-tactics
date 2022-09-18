using System;
using DG.Tweening;
using Taiga.Core.Unity.Demo.Providers;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.Bk01
{
    public abstract class BaseM05UltimateAttack01 : MonoBehaviour
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
                case "down":
                    OnDown();
                    break;
            }
        }

        internal abstract void OnPrepare();
        internal abstract Sequence Initialize();
        internal abstract void OnReset();
        internal abstract void OnAttack1();
        internal abstract void OnDown();
    }
}
