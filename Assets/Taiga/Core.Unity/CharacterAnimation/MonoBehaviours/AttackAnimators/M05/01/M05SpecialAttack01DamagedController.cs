using DG.Tweening;
using Taiga.Core.Unity.CharacterAnimation.Base;
using Taiga.Core.Unity.CharacterAnimation.Effect;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.M05._01
{
    public class M05SpecialAttack01DamagedController : BaseM05SpecialAttack01
    {
        private const float DistanceScale = 0.2f;
        public override bool AutoMoveToward => false;
        public override CharacterTimelineAnimatorRole Role => CharacterTimelineAnimatorRole.Damaged;
        public override float MoveTowardAmount => 0f;
        public override string AttackAnimatorTrigger => null;

        private Vector3 _originalPosition;

        protected bool IsAttackerOnTheRight
        {
            get
            {
                var attackPositionOffset =
                    (_originalPosition.UnityToRoundGamePosition() - AttackerPosition.Value).NormalizeByDirection(
                        Direction.GetNormalizeDirection());
                return attackPositionOffset.x == 1;
            }
        }

        public override Sequence OnStart()
        {
            var sequence = base.OnStart();

            _originalPosition = CharacterAnimator.root.localPosition;

            var direction = (AttackerPosition.Value.GameToUnityPosition() - _originalPosition) * DistanceScale;
            var movePos = _originalPosition + direction;
            sequence.PrependCallback(() => CharacterAnimator.root.DOLocalMove(movePos, 0.4f));

            return sequence;
        }

        internal override void OnReset()
        {
            CharacterAnimator.animator.SetTrigger($"stand");
            CharacterAnimator.root.DOLocalMove(_originalPosition, 0.2f);

            OnEnd();
        }

        internal override void OnPrepare()
        {
            //
        }

        internal override void OnAttack1()
        {
            ShakeAndMaskCharacter();
            SetAnimatorTrigger("damaged");
            ShowDirectedEffect(AttackEffectItem.AttackEffectItem1, 1f);
            if (IsAttackerOnTheRight)
            {
                ShowLeftTextEffect();
            }
            else
            {
                ShowRightTextEffect();
            }

            DOTween.Sequence()
                .AppendInterval(1f)
                .AppendCallback(HideTextEffects)
                .Play();
        }

        internal override void OnDown()
        {
            CharacterAnimator.animator.Play("SitTrigger", -1, 0);
        }
    }
}