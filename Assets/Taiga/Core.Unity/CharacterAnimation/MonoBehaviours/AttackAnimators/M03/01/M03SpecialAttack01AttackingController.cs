using DG.Tweening;
using Taiga.Core.Unity.CharacterAnimation.Base;
using Taiga.Core.Unity.CharacterAnimation.Effect;
using Taiga.Core.Unity.CharacterAnimation.M03._00;

namespace Taiga.Core.Unity.CharacterAnimation.M03._00
{
    public class M03SpecialAttack01AttackingController : BaseM03SpecialAttack01
    {
        public override bool AutoMoveToward => true;
        public override CharacterTimelineAnimatorRole Role => CharacterTimelineAnimatorRole.Attacking;
        public override float MoveTowardAmount => 0.4f;
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
            ShowBodyEffect(AttackEffectItem.SpecialAttackEffectItem2, 1.2f);
        }

        protected override void OnStrike1()
        {
            ShowFocusLine(false, 1f);
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