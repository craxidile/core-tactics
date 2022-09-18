using DG.Tweening;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.Base.CharacterTranslation
{
    public class CharacterTranslationController
    {
        private BaseCharacterTimelineController _characterTimelineController;

        public CharacterTranslationController(BaseCharacterTimelineController characterTimelineController)
        {
            _characterTimelineController = characterTimelineController;
        }

        internal Vector3 CreateMoveTowardDelta()
        {
            var moveX = 0f;
            var moveZ = 0f;
            var moveTowardAmount = _characterTimelineController.MoveTowardAmount;
            switch (_characterTimelineController.Direction)
            {
                case MapDirection.East:
                    moveX = moveTowardAmount;
                    break;
                case MapDirection.West:
                    moveX = -moveTowardAmount;
                    break;
                case MapDirection.North:
                    moveZ = moveTowardAmount;
                    break;
                case MapDirection.South:
                    moveZ = -moveTowardAmount;
                    break;
            }

            return _characterTimelineController.Role == CharacterTimelineAnimatorRole.Attacking
                ? new Vector3(moveX, 0, moveZ)
                : new Vector3(-moveX, 0, -moveZ);
        }

        internal void MoveToward(Sequence sequence = null)
        {
            var root = _characterTimelineController.CharacterAnimator.root;
            _characterTimelineController.OriginalPosition = root.localPosition;

            _characterTimelineController.MoveTowardDelta = CreateMoveTowardDelta();

            var initSequenceEmpty = sequence == null;
            if (initSequenceEmpty) sequence = DOTween.Sequence();
            sequence.Append(root.DOLocalMove(
                _characterTimelineController.OriginalPosition + _characterTimelineController.MoveTowardDelta,
                _characterTimelineController.MoveTowardDelay
            ));
            if (initSequenceEmpty) sequence.Play();
        }

        internal void MoveBack(float? delay)
        {
            var root = _characterTimelineController.CharacterAnimator.root;
            root.DOLocalMove(
                _characterTimelineController.OriginalPosition,
                delay ?? _characterTimelineController.MoveTowardDelay
            );
        }
    }
}