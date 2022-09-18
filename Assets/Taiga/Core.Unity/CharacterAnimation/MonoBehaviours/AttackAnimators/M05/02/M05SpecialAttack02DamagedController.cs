using System;
using DG.Tweening;
using Taiga.Core.Unity.Camera;
using Taiga.Core.Unity.CharacterAnimation.Base;
using Taiga.Core.Unity.CharacterAnimation.Effect;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.M05._02
{
    public class M05SpecialAttack02DamagedController : BaseM05SpecialAttack02
    {
        private const string HardPunchClip = "HardPunchClip";
        private const float DistanceScale = 0.3f;

        private Vector3 _originalPosition;
        private Vector2Int _attackerPositionOffset;

        public override bool AutoMoveToward => false;
        public override CharacterTimelineAnimatorRole Role => CharacterTimelineAnimatorRole.Damaged;
        public override float MoveTowardAmount => 0f;
        public override string AttackAnimatorTrigger => null;

        private bool IsFlipped => Math.Abs(GameCamera.Instance.Angle - 135) < 0.1f && Direction == MapDirection.East;


        private bool IsStandInDirectionFromTarget(Vector2Int _targetOffsetPosition, MapDirection _mapDirection)
        {
            var xDir = _targetOffsetPosition.x == 1 ? MapDirection.East :
                _targetOffsetPosition.x == -1 ? MapDirection.West : 0b0000;
            var yDir = _targetOffsetPosition.y == 1 ? MapDirection.North :
                _targetOffsetPosition.y == -1 ? MapDirection.South : 0b0000;
            return _mapDirection == ((byte)xDir) + yDir;
        }

        private MapDirection GetStandDirectionFromTarget(Vector2Int _targetOffsetPosition)
        {
            var xDir = _targetOffsetPosition.x == 1 ? MapDirection.East :
                _targetOffsetPosition.x == -1 ? MapDirection.West : 0b0000;
            var yDir = _targetOffsetPosition.y == 1 ? MapDirection.North :
                _targetOffsetPosition.y == -1 ? MapDirection.South : 0b0000;
            return ((byte)xDir) + yDir;
        }

        private void ShowTextEffect(MapDirection mapDirection)
        {
            switch (mapDirection)
            {
                case MapDirection.North:
                case MapDirection.East:
                case ((byte)MapDirection.North) + MapDirection.East:
                case ((byte)MapDirection.South) + MapDirection.East:
                    ShowLeftTextEffect();
                    break;
                case MapDirection.South:
                case MapDirection.West:
                case ((byte)MapDirection.North) + MapDirection.West:
                case ((byte)MapDirection.South) + MapDirection.West:
                    ShowRightTextEffect();
                    break;
            }

            DOTween.Sequence().AppendInterval(1f).AppendCallback(HideTextEffects);
        }

        internal void ShakeCharacter()
        {
            CharacterAnimator.body.DOShakePosition(1f, new Vector3(0.07f, 0, 0.07f), 50, 20, false, false);
        }

        private void Damage()
        {
            PlayAudioClip(HardPunchClip);
            CharacterAnimator.animator.SetTrigger("damaged");
            MaskCharacter(false);
            ShakeCharacter();
            ShowDirectedEffect(AttackEffectItem.AttackEffectItem1, 1f);
            ShowTextEffect(GetStandDirectionFromTarget(_attackerPositionOffset));
        }

        public override Sequence OnStart()
        {
            var sequence = base.OnStart();

            _originalPosition = CharacterAnimator.root.localPosition;

            var direction = (AttackerPosition.Value.GameToUnityPosition() - _originalPosition) * DistanceScale;
            var movePos = _originalPosition + direction;
            sequence.PrependCallback(() => CharacterAnimator.root.DOLocalMove(movePos, 0.4f));

            _attackerPositionOffset = (_originalPosition.UnityToRoundGamePosition() - AttackerPosition.Value)
                .NormalizeByDirection(Direction.GetNormalizeDirection());

            AddAudioClip(HardPunchClip, "m09_punch_hard");

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

        internal override void OnAttackNW()
        {
            switch (IsFlipped)
            {
                case false when IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.NorthWest):
                    Debug.Log("OnAttackNW");
                    Damage();
                    break;
                case true when IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.NorthEast):
                    Debug.Log("Flip OnAttackNE");
                    Damage();
                    break;
            }
        }

        internal override void OnAttackN()
        {
            if (!IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.North)) return;
            Debug.Log("OnAttackN");
            Damage();
        }

        internal override void OnAttackNE()
        {
            switch (IsFlipped)
            {
                case false when IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.NorthEast):
                    Debug.Log("OnAttackNE");
                    Damage();
                    break;
                case true when IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.NorthWest):
                    Debug.Log("Flip OnAttackNW");
                    Damage();
                    break;
            }
        }

        internal override void OnAttackE()
        {
            switch (IsFlipped)
            {
                case false when IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.East):
                    Debug.Log("OnAttackE");
                    Damage();
                    break;
                case true when IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.West):
                    Debug.Log("Flip OnAttackW");
                    Damage();
                    break;
            }
        }

        internal override void OnAttackSE()
        {
            switch (IsFlipped)
            {
                case false when IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.SouthEast):
                    Debug.Log("OnAttackSE");
                    Damage();
                    break;
                case true when IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.SouthWest):
                    Debug.Log("Flip OnAttackSW");
                    Damage();
                    break;
            }
        }

        internal override void OnAttackS()
        {
            if (!IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.South)) return;
            Debug.Log("OnAttackS");
            Damage();
        }

        internal override void OnAttackSW()
        {
            switch (IsFlipped)
            {
                case false when IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.SouthWest):
                    Debug.Log("OnAttackSW");
                    Damage();
                    break;
                case true when IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.SouthEast):
                    Debug.Log("Flip OnAttackSE");
                    Damage();
                    break;
            }
        }

        internal override void OnAttackW()
        {
            switch (IsFlipped)
            {
                case false when IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.West):
                    Debug.Log("OnAttackW");
                    Damage();
                    break;
                case true when IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.East):
                    Debug.Log("Flip OnAttackE");
                    Damage();
                    break;
            }
        }

        internal override void OnDown()
        {
            //
        }
    }
}