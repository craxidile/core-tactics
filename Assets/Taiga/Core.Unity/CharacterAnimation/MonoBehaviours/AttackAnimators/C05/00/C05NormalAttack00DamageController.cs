using DG.Tweening;
using Taiga.Core.Unity.CharacterAnimation.Base;

namespace Taiga.Core.Unity.CharacterAnimation.C05._00
{
    public class C05NormalAttack00DamageController: BaseC05NormalAttack00
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
            ShakeCameraLight();
        }

        protected override void OnStrike2()
        {
            ShowRightTextEffect();
            ShakeAndMaskCharacter();
            ShakeCameraLight();
        }

        private void ShakeCameraLight()
        {
            UnityEngine.Camera.current.gameObject.transform.DOShakeRotation(0.5f, 0.5f, 15);
        }


        protected override void OnStrike3()
        {
            ShowLeftTextEffect();
            ShakeAndMaskCharacter();
            ShakeCameraLight();
        }
        
    }
}