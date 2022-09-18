using DG.Tweening;
using Taiga.Core.Unity.CharacterAnimation.Base;
using Taiga.Core.Unity.CharacterAnimation.Effect;

namespace Taiga.Core.Unity.CharacterAnimation.M05._01
{
    public class M05SpecialAttack01AttackingController : BaseM05SpecialAttack01
    {
        private const string HardPunchClip = "HardPunchClip";
        private const string ChargeClip = "Charge";
        
        public override bool AutoMoveToward => true;
        public override CharacterTimelineAnimatorRole Role => CharacterTimelineAnimatorRole.Attacking;
        public override float MoveTowardAmount => 0.3f;
        public override string AttackAnimatorTrigger => "attack_special_1";

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
            PlayAudioClip(ChargeClip);
            ShowBodyEffect(AttackEffectItem.SpecialAttackEffectItem1, 1.7f);
        }
        
        internal override void OnAttack1()
        {
            PlayAudioClip(HardPunchClip);
            ShowFocusLine(false, 1f);
            ShakeCamera();
        }

        internal override void OnDown()
        {
            //
        }
    }
}