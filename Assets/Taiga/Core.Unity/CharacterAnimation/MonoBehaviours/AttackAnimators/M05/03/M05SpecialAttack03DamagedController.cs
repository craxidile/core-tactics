using DG.Tweening;
using Taiga.Core.Unity.Camera;
using Taiga.Core.Unity.CharacterAnimation.Base;
using Taiga.Core.Unity.CharacterAnimation.Effect;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.M05._03
{
    public class M05SpecialAttack03DamagedController : BaseM05SpecialAttack03
    {
        private const string HardPunchClip = "HardPunchClip";
        private const float DistanceScale = 0.1f;

        public override bool AutoMoveToward => false;
        public override CharacterTimelineAnimatorRole Role => CharacterTimelineAnimatorRole.Damaged;
        public override float MoveTowardAmount => 0f;
        public override string AttackAnimatorTrigger => null;

        private bool IsFlipped => GameCamera.Instance.Angle == 135 && Direction == MapDirection.East;

        private Vector3 _originalPosition;
        private Sequence _currentDamageTween;

        private void DamageFly()
        {
            PlayAudioClip(HardPunchClip);
            CharacterAnimator.animator.SetTrigger($"fly");
            ShakeAndMaskCharacter();
            ShowDirectedEffect(AttackEffectItem.AttackEffectItem1, 1f);
            ShowLeftTextEffect();
            DOTween.Sequence().AppendInterval(1f).AppendCallback(HideTextEffects);

            var faceCam = CharacterAnimator.bodyRenderer.GetComponent<AlwaysFaceCamera>();
            var originalY = CharacterAnimator.bodyRenderer.transform.localPosition.y;
            faceCam.enabled = false;

            _currentDamageTween = DOTween.Sequence()
                .Append(CharacterAnimator.bodyRenderer.transform.DOLocalMoveY(1f, 0.5f))
                .Join(CharacterAnimator.bodyRenderer.transform.DOLocalRotate(new Vector3(0, 0, -180), 0.2f)
                    .SetLoops(3, LoopType.Incremental))
                .Append(CharacterAnimator.bodyRenderer.transform.DOLocalMoveY(originalY, 0.5f))
                .Join(CharacterAnimator.bodyRenderer.transform.DOLocalRotate(Vector3.zero, 0.5f))
                .AppendCallback(() => faceCam.enabled = true);
        }

        public override Sequence OnStart()
        {
            var sequence = base.OnStart();

            _originalPosition = CharacterAnimator.root.localPosition;
            var direction = (AttackerPosition.Value.GameToUnityPosition() - _originalPosition) * DistanceScale;
            var movePos = _originalPosition + direction;

            sequence.PrependCallback(() => CharacterAnimator.root.DOLocalMove(movePos, 0f));

            AddAudioClip(HardPunchClip, "m09_punch_hard");

            return sequence;
        }

        internal override void OnReset()
        {
            HideTextEffects();
            CharacterAnimator.animator.SetTrigger($"stand");
            OnEnd();
        }

        internal override void OnPrepare()
        {
            // Do Nothing
        }

        internal override void OnAttack1()
        {
            if (!IsFlipped &&
                _originalPosition.UnityToRoundGamePosition()
                    .GetMapDirectionFromTarget(AttackerPosition.Value, Direction) == MapDirection.NorthWest)
            {
                DamageFly();
            }
            else if (IsFlipped && _originalPosition.UnityToRoundGamePosition()
                         .GetMapDirectionFromTarget(AttackerPosition.Value, Direction) == MapDirection.NorthEast)
            {
                DamageFly();
            }
        }

        internal override void OnAttack2()
        {
            var isInPosition =
                _originalPosition.UnityToRoundGamePosition()
                    .GetMapDirectionFromTarget(AttackerPosition.Value, Direction) == MapDirection.North;
            if (isInPosition)
                DamageFly();
        }

        internal override void OnAttack3()
        {
            if (!IsFlipped &&
                _originalPosition.UnityToRoundGamePosition()
                    .GetMapDirectionFromTarget(AttackerPosition.Value, Direction) == MapDirection.NorthEast)
            {
                DamageFly();
            }
            else if (IsFlipped && _originalPosition.UnityToRoundGamePosition()
                         .GetMapDirectionFromTarget(AttackerPosition.Value, Direction) == MapDirection.NorthWest)
            {
                DamageFly();
            }
        }

        internal override void OnFreeze()
        {
            _currentDamageTween.timeScale = 0;
        }

        internal override void OnAttack4()
        {
            MaskCharacter();
            ShowDirectedEffect(AttackEffectItem.AttackEffectItem1, 1f);
            ShowLeftTextEffect();
            DOTween.Sequence().AppendInterval(0.5f).AppendCallback(HideTextEffects);
        }

        internal override void OnUnfreeze()
        {
            _currentDamageTween.timeScale = 1;
            CharacterAnimator.animator.Play("LayTrigger", -1, 0);
            OnBump?.Invoke(true);
        }

        internal override void OnDown()
        {
            CharacterAnimator.animator.Play("SitTrigger", -1, 0);
        }
    }
}