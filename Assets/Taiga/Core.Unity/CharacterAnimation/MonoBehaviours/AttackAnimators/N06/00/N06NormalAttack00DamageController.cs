using Taiga.Core.Unity.CharacterAnimation.Base;

namespace Taiga.Core.Unity.CharacterAnimation.N06._00
{
    public class N06NormalAttack00DamageController: BaseN06NormalAttack00
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