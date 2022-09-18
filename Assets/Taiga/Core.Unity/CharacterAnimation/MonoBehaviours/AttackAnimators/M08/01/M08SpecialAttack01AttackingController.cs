using DG.Tweening;
using Taiga.Core.Unity.Camera;
using Taiga.Core.Unity.CharacterAnimation.Base;
using Taiga.Utils;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.M08._01
{
    public class M08SpecialAttack01AttackingController : BaseM08SpecialAttack01
    {
        private float _throwDelay = 0f;
        private const string GreenTeaBottleKey = "green_tea";

        public override bool AutoMoveToward => false;
        public override CharacterTimelineAnimatorRole Role => CharacterTimelineAnimatorRole.Attacking;
        public override float MoveTowardAmount => 0f;
        public override string AttackAnimatorTrigger => "attack_special_1";

        public override Sequence OnStart()
        {
            var sequence = base.OnStart();
            
            CharacterAnimator.throwableDock.gameObject.SetActive(true);

            GreenTeaBottle.OriginPosition = CharacterAnimator.throwableDock.transform.position;
            GreenTeaBottle.OnHitFinish = ResumeAnimation;

            AddThrowableSprite(GreenTeaBottleKey, "green_tea");

            return sequence;
        }

        protected override void OnReset()
        {
            Debug.Log($">>on_attacker_reset<<");
            ResetThrowableSprite();
            CharacterAnimator.throwableDock.gameObject.SetActive(false);
            OnEnd();
        }

        protected override void OnZoomOut()
        {
            ZoomOutCamera();
        }

        protected override void OnThrow()
        {
            SpriteRearranger.Rearrange();

            ThrowSprite(GreenTeaBottleKey, GreenTeaBottle.ThrowDelay, GreenTeaBottle.OriginPosition,
                GreenTeaBottle.DestinationPosition);
            
            Debug.Log($">>throw<< {GreenTeaBottle.OriginPosition} {GreenTeaBottle.DestinationPosition}");

            DOTween.Sequence()
                .AppendInterval(GreenTeaBottle.ThrowDelay)
                .AppendCallback(() => { GreenTeaBottle.Hit(_throwDelay); });
        }

        protected override void OnWait()
        {
            PauseAnimation();
        }
    }
}