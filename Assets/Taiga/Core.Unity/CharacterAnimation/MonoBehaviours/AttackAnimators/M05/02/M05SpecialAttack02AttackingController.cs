using DG.Tweening;
using Taiga.Core.Unity.CharacterAnimation.Base;
using Taiga.Core.Unity.CharacterAnimation.Effect;

namespace Taiga.Core.Unity.CharacterAnimation.M05._02
{
    public class M05SpecialAttack02AttackingController : BaseM05SpecialAttack02
    {
        private const string ChargeClip = "Charge";
        
        public override bool AutoMoveToward => false;
        public override CharacterTimelineAnimatorRole Role => CharacterTimelineAnimatorRole.Attacking;
        public override float MoveTowardAmount => 0f;
        public override string AttackAnimatorTrigger => "attack_special_2";

        public override Sequence OnStart()
        {
            var sequence = base.OnStart();
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
        
        internal override void OnAttackNW()
        {
            ShowFocusLine(false, 1f);
        }

        internal override void OnAttackN()
        {
        }

        internal override void OnAttackNE()
        {
        }

        internal override void OnAttackE()
        {
        }

        internal override void OnAttackSE()
        {
        }

        internal override void OnAttackS()
        {
        }

        internal override void OnAttackSW()
        {
        }

        internal override void OnAttackW()
        {
        }
        
        internal override void OnDown()
        {
        }
    }
}