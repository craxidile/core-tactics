using System;
using DG.Tweening;
using Taiga.Core.Character.Attack;
using Taiga.Core.Unity.UltimateAttack.Providers;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

namespace Taiga.Core.Unity.CharacterAnimation
{

    public class BasicAttackAnimator : ICharacterAttackAnimator
    {

        const string animatorTrigger = "attack_1";

        public IGameAudioPreset AudioPreset { get; set; }

        public Tween Attack(
            CharacterAnimator characterAnimator,
            MapDirection direction
        )
        {
            var animator = characterAnimator.animator;
            var root = characterAnimator.root;

            characterAnimator.Facing = direction;
            var ultimateAttackController = characterAnimator.ultimateAttackController.gameObject;
            var audioSource = ultimateAttackController.AddComponent<AudioSource>();


            var moveX = 0f;
            var moveZ = 0f;
            var originalPosition = root.localPosition;
            var moveAmount = 0.2f;
            GameObject effect;
            switch (direction)
            {
                case MapDirection.East:
                    effect = characterAnimator.effect3.gameObject;
                    moveX = moveAmount;
                    break;
                case MapDirection.West:
                    effect = characterAnimator.effect4.gameObject;
                    moveX = -moveAmount;
                    break;
                case MapDirection.North:
                    effect = characterAnimator.effect2.gameObject;
                    moveZ = moveAmount;
                    break;
                case MapDirection.South:
                    effect = characterAnimator.effect1.gameObject;
                    moveZ = -moveAmount;
                    break;
                default:
                    effect = characterAnimator.effect1.gameObject;
                    break;
            }

            Debug.LogFormat(">>effect_delay<< {0}", characterAnimator.animationConfig.attackEffectDelay);
            var attackEffectDelay = characterAnimator.animationConfig.attackEffectDelay / 1000f;
            var attackEndDelay = 1.4f - attackEffectDelay;

            var sequence = DOTween.Sequence();

            sequence.Append(root.DOLocalMove(originalPosition + new Vector3(moveX, 0, moveZ), 0.15f));
            sequence.AppendInterval(0.1f);
            sequence.AppendCallback(() =>
            {
                animator.SetTrigger(animatorTrigger);
            });
            sequence.AppendInterval(attackEffectDelay);
            sequence.AppendCallback(() =>
            {
                effect.SetActive(true);
                audioSource.clip = AudioPreset.GetAudioSourceByName("m09_punch_normal");
                audioSource.Play();
            });
            sequence.AppendInterval(attackEndDelay);
            sequence.AppendCallback(() =>
            {
                effect.SetActive(false);
            });
            // sequence.AppendInterval(0.6f);
            sequence.Append(root.DOLocalMove(originalPosition, 0.15f));
            sequence.AppendCallback(() =>
            {
                AudioSource.Destroy(audioSource);
            });

            // sequence.AppendInterval(2);

            return sequence;
        }


        private Vector3 _originalEffect5Position;
        private Vector3 _originalEffect6Position;
        private const float EffectMoveX = -0.2f;

        private float CameraAngle
        {
            get
            {

                var cameraRotation = UnityEngine.Camera.current.gameObject.transform.rotation;
                return cameraRotation.eulerAngles.y;
            }
        }

        private Vector3 Effect5Position
        {
            get
            {
                return new Vector3(
                    _originalEffect5Position.x + (CameraAngle == 315 || CameraAngle == 225 ? EffectMoveX : 0f),
                    _originalEffect5Position.y,
                    _originalEffect5Position.z
                );
            }
        }

        private Vector3 Effect6Position
        {
            get
            {
                return new Vector3(
                    _originalEffect6Position.x + (CameraAngle == 315 || CameraAngle == 225 ? EffectMoveX : 0f),
                    _originalEffect6Position.y,
                    _originalEffect6Position.z
                );
            }
        }

        internal void ShowEffect5(CharacterAnimator characterAnimator)
        {
            characterAnimator.effect5.localPosition = Effect5Position;
            characterAnimator.effect5.gameObject.SetActive(true);
            characterAnimator.effect6.gameObject.SetActive(false);
            characterAnimator.effect5.DOShakePosition(0.3f, new Vector3(0.04f, 0.04f, 0.04f), 20, 20, false, false);
        }

        internal void ShowEffect6(CharacterAnimator characterAnimator)
        {
            characterAnimator.effect6.localPosition = Effect6Position;
            characterAnimator.effect5.gameObject.SetActive(false);
            characterAnimator.effect6.gameObject.SetActive(true);
            characterAnimator.effect6.DOShakePosition(0.3f, new Vector3(0.04f, 0.04f, 0.04f), 20, 20, false, false);
        }

        internal void HideEffects(CharacterAnimator characterAnimator)
        {
            characterAnimator.effect5.localPosition = _originalEffect5Position;
            characterAnimator.effect6.localPosition = _originalEffect6Position;
            characterAnimator.effect5.gameObject.SetActive(false);
            characterAnimator.effect6.gameObject.SetActive(false);
        }

        public Tween Damaged(
            CharacterAnimator characterAnimator,
            MapDirection bumpDirection,
            Vector2Int offsetRelativeToAttacker,
            Vector2Int? endPosition,
            Vector2Int? bumpPosition,
            Action onBump,
            Vector2Int? attackerPosition
        )
        {
            Tween tween;
            var root = characterAnimator.root;
            var animator = characterAnimator.animator;

            if (endPosition == null)
            {
                // tween = DOVirtual.DelayedCall(0.5f, () => { });
                // tween.OnComplete(() => {
                //     characterAnimator.Facing = bumpDirection.GetOppsite();
                //     animator.SetTrigger("damaged");
                //     
                // });

                characterAnimator.Facing = bumpDirection.GetOppsite();
                var moveX = 0f;
                var moveZ = 0f;
                var originalPosition = root.localPosition;

                var moveAmount = 0.2f;
                switch (characterAnimator.Facing)
                {
                    case MapDirection.East:
                        moveX = moveAmount;
                        break;
                    case MapDirection.West:
                        moveX = -moveAmount;
                        break;
                    case MapDirection.North:
                        moveZ = moveAmount;
                        break;
                    case MapDirection.South:
                        moveZ = -moveAmount;
                        break;
                }

                _originalEffect5Position = characterAnimator.effect5.localPosition;
                _originalEffect6Position = characterAnimator.effect6.localPosition;

                tween = DOTween.Sequence()
                    .Append(root.DOLocalMove(originalPosition + new Vector3(moveX, 0, moveZ), 0.15f))
                    .AppendInterval(0.7f)
                    .AppendCallback(() =>
                    {
                        ShowEffect5(characterAnimator);
                        animator.SetTrigger("damaged");
                    })
                    .Append(root.DOShakePosition(0.37f, new Vector3(0.04f, 0, 0.04f), 28, 20, false, false))
                    .AppendInterval(0.4f)
                    .AppendCallback(() =>
                    {
                        HideEffects(characterAnimator);
                    })
                    // .AppendInterval(1.0f)
                    .Append(root.DOLocalMove(originalPosition, 0.15f));
            }
            else
            {
                tween = characterAnimator.CreateBumpDamagedTween(
                    bumpDirection: bumpDirection,
                    endPosition: endPosition,
                    bumpPosition: bumpPosition,
                    onBump: onBump
                );
            }

            return tween;
        }

        public Tween Blocked(
            CharacterAnimator characterAnimator,
            MapDirection bumpDirection
        )
        {
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() =>
            {
                characterAnimator.Facing = bumpDirection;
                characterAnimator.animator.SetTrigger("guard");
            });

            sequence.AppendInterval(1);
            return sequence;
        }
    }
}
