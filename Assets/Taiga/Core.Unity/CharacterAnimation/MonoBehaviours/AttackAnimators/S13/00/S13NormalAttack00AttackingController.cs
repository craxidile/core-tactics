using DG.Tweening;
using Taiga.Core.Unity.CharacterAnimation.Base;
using Taiga.Core.Unity.CharacterAnimation.Effect;

namespace Taiga.Core.Unity.CharacterAnimation.S13._00
{
    public class S13NormalAttack00AttackingController: BaseS13NormalAttack00
    {
        private const string NormalPunchClip = "NormalPunchClip";
        private const string HardPunchClip = "HardPunchClip";
        private const string ChargeClip = "Charge";
        public override bool AutoMoveToward => true;
        public override CharacterTimelineAnimatorRole Role => CharacterTimelineAnimatorRole.Attacking;
        public override float MoveTowardAmount => 0.15f;
        public override string AttackAnimatorTrigger => "attack_1";
        
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
            MoveBack();
            OnEnd();
        }

        protected override void OnStrike()
        {
            PlayAudioClip(NormalPunchClip);
            ShowDirectedEffect(AttackEffectItem.AttackEffectItem1, 0.8f);
        }
    }
}