using Taiga.Core.Unity.CharacterAnimation.Base;

namespace Taiga.Core.Unity.CharacterAnimation.M03._00
{
    public class M03SpecialAttack01DamageController: BaseM03SpecialAttack01
    {
        public override bool AutoMoveToward => false;
        public override CharacterTimelineAnimatorRole Role => CharacterTimelineAnimatorRole.Damaged;
        public override float MoveTowardAmount => 0;
        public override string AttackAnimatorTrigger => null;

        protected override void OnCharge()
        {
        }

        protected override void OnReset()
        {
            SetAnimatorTrigger("guard");
            HideTextEffects();
            OnEnd();
        }

        protected override void OnStrike1()
        {
            SetAnimatorTrigger("fly");
            ShowLeftTextEffect();
            ShakeAndMaskCharacter();
            ShakeCamera();
        }

        /*protected override void OnStrike2()
        {
            ShowRightTextEffect();
            ShakeAndMaskCharacter();
        }*/
        
    }
}