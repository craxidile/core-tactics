using System.Transactions;
using DG.Tweening;
using Taiga.Core.Unity.CharacterAnimation.Base;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.M01._01
{
    public class M01SpecialAttack01DamagedController : BaseM01SpecialAttack01
    {
        public override bool AutoMoveToward => true;
        public override CharacterTimelineAnimatorRole Role => CharacterTimelineAnimatorRole.Damaged;
        public override float MoveTowardAmount => 0f;
        public override string AttackAnimatorTrigger => null;

        private Vector3 _originalBodyPosition;

        public override Sequence OnStart()
        {
            var sequence = base.OnStart();
            _originalBodyPosition = CharacterAnimator.body.localPosition;
            return sequence;
        }

        internal override void OnReset()
        {
            DOTween
                .Sequence()
                .AppendCallback(() =>
                {
                    CharacterAnimator.animator.SetTrigger($"stand");
                    CharacterAnimator.body.DOLocalMove(_originalBodyPosition, 0.2f);
                })
                .Play();
            OnEnd();
        }

        internal override void OnPrepare()
        {
            //
        }

        internal override void OnRun()
        {
            //
        }

        internal override void OnAttack1()
        {
            ShakeAndMaskCharacter();
        }

        internal override void OnDamaged()
        {
            CharacterAnimator.animator.Play("LayTrigger", -1, 0);
            CharacterAnimator.body.DOShakePosition(0.5f, new Vector3(0, 0.15f, 0), 10, 0);
            OnBump?.Invoke(false);
        }

        internal override void OnGetUp()
        {
            CharacterAnimator.animator.Play("SitTrigger", -1, 0);
        }
    }
}