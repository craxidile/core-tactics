using DG.Tweening;
using Taiga.Core.Unity.CharacterAnimation.Base;
using Taiga.Core.Unity.CharacterAnimation.Effect;

namespace Taiga.Core.Unity.CharacterAnimation.M06._00
{
    public class M06SpecialAttack01AttackingController : BaseM06SpecialAttack01
    {
        public override bool AutoMoveToward => true;
        public override CharacterTimelineAnimatorRole Role => CharacterTimelineAnimatorRole.Attacking;
        public override float MoveTowardAmount => 0.25f;
        public override string AttackAnimatorTrigger => "attack_special_1";

        public override Sequence OnStart()
        {
            var sequence = base.OnStart();
            AddAudioClip("strikeSound", "m09_punch_hard");
            AddAudioClip("chargeSound", "charge");
            return sequence;
        }

        protected override void OnCharge()
        {
            PlayAudioClip("chargeSound");
            ShowBodyEffect(AttackEffectItem.SpecialAttackEffectItem1, 1.2f);
        }

        protected override void OnStrike1()
        {
            ShowFocusLine(false, 0.8f);
            PlayAudioClip("strikeSound");
            ShowDirectedEffect(AttackEffectItem.AttackEffectItem1, 0.8f);
        }

        /*protected override void OnStrike2()
        {
            PlayAudioClip("strikeSound");
            ShowDirectedEffect(AttackEffectItem.AttackEffectItem2, 0.8f);
        }*/

        protected override void OnReset()
        {
            MoveBack();
            OnEnd();
        }
    }
}