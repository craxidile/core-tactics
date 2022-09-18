using System;
using DG.Tweening;
using Taiga.Core.Unity.Demo.Providers;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation
{
    public abstract class BaseM09UltimateAttack00 : MonoBehaviour
    {
        public AnimationClip Animation { get; set; }
        public abstract CharacterAnimator CharacterAnimator { get; set; }

        public void SetAttackAction(string action)
        {
            switch (action)
            {
                case "punch1":
                    OnPunch1();
                    break;
                /*case "punch2":
                    OnPunch2();
                    break;*/
                case "reset":
                    OnReset();
                    break;

            }
        }

        internal abstract Sequence Initialize();
        internal abstract void OnReset();
        internal abstract void OnPunch1();
        //internal abstract void OnPunch2();
    }
}