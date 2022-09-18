using Taiga.Core.Unity.CharacterAnimation.Base;

namespace Taiga.Core.Unity.CharacterAnimation.S09._00
{
    public class S09NormalAttack00DamageController: BaseS09NormalAttack00
    {
        public override bool AutoMoveToward => false;
        public override CharacterTimelineAnimatorRole Role => CharacterTimelineAnimatorRole.Damaged;
        public override float MoveTowardAmount => 0;
        public override string AttackAnimatorTrigger => null;
        
        protected override void OnReset()
        {
            SetAnimatorTrigger("guard");
            HideTextEffects();
            OnEnd();
        }

        protected override void OnStrike()
        {
            SetAnimatorTrigger("fly");
            ShowLeftTextEffect();
            ShakeAndMaskCharacter();
            ShakeCameraLight();
        }
    }
}