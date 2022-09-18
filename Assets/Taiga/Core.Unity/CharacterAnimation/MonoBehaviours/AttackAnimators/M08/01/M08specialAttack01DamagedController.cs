using DG.Tweening;
using Taiga.Core.Unity.CharacterAnimation.Base;
using Taiga.Core.Unity.CharacterAnimation.Effect;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.M08._01
{
    public class M08specialAttack01DamagedController : BaseM08SpecialAttack01
    {
        private const float UpDelay = 1f;
        private const float DamageDelay = 1f;
        private const float DistanceFactor = 0.1f;

        public override bool AutoMoveToward => false;
        public override CharacterTimelineAnimatorRole Role => CharacterTimelineAnimatorRole.Damaged;
        public override float MoveTowardAmount => 0f;
        public override string AttackAnimatorTrigger => null;

        public override Sequence OnStart()
        {
            
            GreenTeaBottle.DestinationPosition = CharacterAnimator.throwableDock.transform.position;
            
            // Calculation
            var enemy = FindObjectOfType<M08SpecialAttack01AttackingController>();
            var distance = Vector3.Distance(enemy.CharacterAnimator.body.position, CharacterAnimator.body.position);
            var throwingDelay = distance * DistanceFactor;
            Debug.Log($">>throwing_delay<< {distance} {throwingDelay}");
            
            AdditionalTimelineDelay = throwingDelay + DamageDelay + UpDelay;

            var sequence = base.OnStart();
            GreenTeaBottle.ThrowDelay = throwingDelay;
            GreenTeaBottle.OnHit = OnHit;
            return sequence;
        }

        protected override void OnReset()
        {
            OnEnd();
        }

        protected override void OnZoomOut()
        {
            //
        }

        protected override void OnThrow()
        {
            //
        }

        protected override void OnWait()
        {
            PauseAnimation();
        }

        private void OnHit()
        {
            DOTween.Sequence()
                .AppendCallback(() =>
                {
                    ShowDirectedEffect(AttackEffectItem.AttackEffectItem1, .2f);
                    SetAnimatorTrigger("fly");
                    ShakeAndMaskCharacter();
                    ShakeCamera();
                })
                .AppendInterval(UpDelay)
                .AppendCallback(() => SetAnimatorTrigger("stand"))
                .AppendInterval(DamageDelay)
                .AppendCallback(() =>
                {
                    GreenTeaBottle.FinishHit();
                    ResumeAnimation();
                })
                .Play();
        }
    }
}