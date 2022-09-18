using DG.Tweening;
using Taiga.Core.Unity.CharacterAnimation.Base;
using Taiga.Core.Unity.CharacterAnimation.Effect;

namespace Taiga.Core.Unity.CharacterAnimation.M05._03
{
    public class M05SpecialAttack03AttackingController : BaseM05SpecialAttack03
    {
        private const string HardPunchClip = "HardPunchClip";
        private const string ChargeClip = "Charge";
        public override bool AutoMoveToward => true;
        public override CharacterTimelineAnimatorRole Role => CharacterTimelineAnimatorRole.Attacking;
        public override float MoveTowardAmount => 0.1f;

        public override string AttackAnimatorTrigger => "attack_special_3";

        public override Sequence OnStart()
        {
            var sequence = base.OnStart();
            AddAudioClip(HardPunchClip, "m09_punch_hard");
            AddAudioClip(ChargeClip, "charge");
            return sequence;
        }

        internal override void OnReset()
        {
            OnEnd();
        }

        internal override void OnPrepare()
        {
            ShowBodyEffect(AttackEffectItem.SpecialAttackEffectItem1, 1.7f);
        }
        
        internal override void OnAttack1()
        {
            ShakeCamera();
        }

        internal override void OnAttack2()
        {
            ShakeCamera();
        }

        internal override void OnAttack3()
        {
            ShakeCamera();
        }

        internal override void OnFreeze()
        {
            ShowBodyEffect(AttackEffectItem.SpecialAttackEffectItem1, 1.7f);
        }

        internal override void OnAttack4()
        {
            PlayAudioClip(HardPunchClip);
            ShakeCamera();
        }

        internal override void OnUnfreeze()
        {
            //
        }

        internal override void OnDown()
        {
            //
        }
    }
}