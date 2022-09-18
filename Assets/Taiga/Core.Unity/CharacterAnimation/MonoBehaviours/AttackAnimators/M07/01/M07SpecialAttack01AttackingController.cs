using DG.Tweening;
using Taiga.Core.Unity.CharacterAnimation.Base;
using Taiga.Core.Unity.CharacterAnimation.Effect;

namespace Taiga.Core.Unity.CharacterAnimation
{
    public class M07SpecialAttack01AttackingController : BaseM07SpecialAttack01
    {
        public override bool AutoMoveToward => true;
        public override CharacterTimelineAnimatorRole Role => CharacterTimelineAnimatorRole.Attacking;
        public override float MoveTowardAmount => 0.4f;
        public override string AttackAnimatorTrigger => "attack_special_1";

        public override Sequence OnStart()
        {
            var sequence = base.OnStart();
            AddAudioClip("punch", "m09_punch_normal");
            return sequence;
        }
        
        protected override void OnAttack01()
        {
            PlayAudioClip("punch");
            ShowDirectedEffect(AttackEffectItem.AttackEffectItem1, 0.3f);
        }

        protected override void OnReset()
        {
            OnEnd();
        }
    }
}