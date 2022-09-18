using System;
using DG.Tweening;
using Taiga.Core.Character.Attack;
using Taiga.Core.Unity.CharacterAnimation.Base;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation
{
    public class M09SpecialAttack01DamagedController : BaseM09SpecialAttack01
    {
        public override bool AutoMoveToward => false;
        public override CharacterTimelineAnimatorRole Role => CharacterTimelineAnimatorRole.Damaged;
        public override float MoveTowardAmount => 0.25f;
        public override string AttackAnimatorTrigger => null;

        protected override void OnReset()
        {
            HideTextEffects();
            DOTween
                .Sequence()
                .AppendInterval(1f)
                .AppendCallback(() =>
                {
                    SetAnimatorTrigger($"up");
                    OnEnd();
                })
                .Play();
        }

        protected override void OnStepToward()
        {
            SetAnimatorTrigger($"fly");
            MoveToward();
        }

        protected override void OnAttack1()
        {
            ShakeAndMaskCharacter();
            ShowLeftTextEffect();
        }

        protected override void OnAttack2()
        {
            ShakeAndMaskCharacter();
            ShowRightTextEffect();
        }

        protected override void OnAttack3()
        {
            ShakeAndMaskCharacter();
            ShowLeftTextEffect();
        }

        protected override void OnAttack4()
        {
            ShakeAndMaskCharacter();
            ShowRightTextEffect();
            DOTween.Sequence().AppendInterval(0.4f).AppendCallback(HideTextEffects).Play();
        }

        protected override void OnAttack5()
        {
            ShakeCamera();
            MaskCharacter();
            ShowLeftTextEffect();
            DOTween.Sequence().AppendInterval(0.7f).AppendCallback(HideTextEffects).Play();
        }

        protected override void OnDown()
        {
            SetAnimatorTrigger($"die");
            MoveBack(0.2f);
        }

        protected override void OnFreeze()
        {
            // Do Nothing
        }
    }
}