using DG.Tweening;
using Taiga.Core.Unity.CharacterAnimation.Base;
using Taiga.Core.Unity.CharacterAnimation.Effect;

namespace Taiga.Core.Unity.CharacterAnimation.M05._00
{
    public class M05NormalAttack01AttackingController : BaseM05NormalAttack01
    {
        private const string strikeSoundLight = "strikeSoundLight";
        public override bool AutoMoveToward => true;
        public override CharacterTimelineAnimatorRole Role => CharacterTimelineAnimatorRole.Attacking;
        public override float MoveTowardAmount => 0.15f;
        public override string AttackAnimatorTrigger => "attack_1";

        public override Sequence OnStart()
        {
            var sequence = base.OnStart();
            AddAudioClip(strikeSoundLight, "m09_punch_normal");
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
            //ShowFocusLine(false, 0.3f);
            PlayAudioClip("strikeSoundLight");
            ShowDirectedEffect(AttackEffectItem.AttackEffectItem1, 0.8f);
        }
        protected override void OnStrike2()
        {
            //ShowFocusLine(false, 0.3f);
            PlayAudioClip("strikeSound");
            ShowDirectedEffect(AttackEffectItem.AttackEffectItem2, 0.8f);
        }
        protected override void OnStrike3()
        {
            ShowFocusLine(false, 1f);
            PlayAudioClip("strikeSound");
            ShowDirectedEffect(AttackEffectItem.AttackEffectItem3, 0.8f);
        }
        protected override void OnReset()
        {
            MoveBack();
            OnEnd();
        }
    }
}