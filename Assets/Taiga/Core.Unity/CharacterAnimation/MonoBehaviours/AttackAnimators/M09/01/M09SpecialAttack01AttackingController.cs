using System;
using DG.Tweening;
using Taiga.Core.Character.Attack;
using Taiga.Core.Unity.CharacterAnimation.Base;
using Taiga.Core.Unity.CharacterAnimation.Effect;
using UnityEditor;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation
{
    public class M09SpecialAttack01AttackingController : BaseM09SpecialAttack01
    {
        private const string NormalPunchClip = "NormalPunchClip";
        private const string HardPunchClip = "HardPunchClip";
        private const string ChargeClip = "Charge";
        private const float ChargeEffectDuration = 1.7f;
        private const float SmallPunchDuration = 0.2f;

        public override bool AutoMoveToward => true;
        public override CharacterTimelineAnimatorRole Role => CharacterTimelineAnimatorRole.Attacking;
        public override float MoveTowardAmount => 0.4f;
        public override string AttackAnimatorTrigger => "attack_4";

        public override Sequence OnStart()
        {
            var sequence = base.OnStart();
            AddAudioClip(NormalPunchClip, "m09_punch_normal");
            AddAudioClip(HardPunchClip, "m09_punch_hard");
            AddAudioClip(ChargeClip, "charge");
            return sequence;
        }

        protected override void OnReset()
        {
            OnEnd();
        }

        protected override void OnStepToward()
        {
            // Do Nothing
        }

        protected override void OnAttack1()
        {
            ShowFocusLine(false, 1.5f);
            PlayAudioClip(NormalPunchClip);
            ShowDirectedEffect(AttackEffectItem.AttackEffectItem1, SmallPunchDuration);
        }

        protected override void OnAttack2()
        {
            PlayAudioClip(NormalPunchClip);
            ShowDirectedEffect(AttackEffectItem.AttackEffectItem1, SmallPunchDuration);
        }

        protected override void OnAttack3()
        {
            PlayAudioClip(NormalPunchClip);
            ShowDirectedEffect(AttackEffectItem.AttackEffectItem1, SmallPunchDuration);
        }

        protected override void OnAttack4()
        {
            PlayAudioClip(NormalPunchClip);
            ShowDirectedEffect(AttackEffectItem.AttackEffectItem1, SmallPunchDuration);
        }

        protected override void OnAttack5()
        {
            ShowFocusLine(false, 0.6f);
            ShowDirectedEffect(AttackEffectItem.AttackEffectItem2, SmallPunchDuration);
            PlayAudioClip(HardPunchClip);
            MoveBack();
        }

        protected override void OnDown()
        {
            // Do Nothing
        }

        protected override void OnFreeze()
        {
            PlayAudioClip(ChargeClip);
            ShowBodyEffect(AttackEffectItem.SpecialAttackEffectItem1, ChargeEffectDuration);
        }
    }
}
