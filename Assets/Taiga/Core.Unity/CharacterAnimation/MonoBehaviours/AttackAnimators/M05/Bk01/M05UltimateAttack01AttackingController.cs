using System;
using System.Linq;
using DG.Tweening;
using Sirenix.Utilities;
using Taiga.Core.Unity.Camera;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.Bk01
{
    public class M05UltimateAttack01AttackingController : BaseM05UltimateAttack01
    {
        private const float _CHARGE_EFFECT_DURATION = 1.7f;
        private const float EffectLargeScale = 1.5f;
        private const float MoveTowardAmount = 0.3f;
        private const string AnimatorTrigger = "attack_special_1";

        public override CharacterAnimator CharacterAnimator { get; set; }
        public MapDirection Direction { get; set; }
        public AudioClip AttackClip { get; set; }
        public AudioClip ChargeClip { get; set; }

        private GameObject Effect => CharacterAnimator.chargeEffect.gameObject;


        private Sequence _sequence;
        private Animation _attackingAnimation;
        private AudioSource _audioSource;
        private Vector3 _originalPosition;

        internal override Sequence Initialize()
        {
            var animator = CharacterAnimator.animator;
            var root = CharacterAnimator.root;

            CharacterAnimator.Facing = Direction;
            var moveX = 0f;
            var moveZ = 0f;
            switch (Direction)
            {
                case MapDirection.East:
                    moveX = MoveTowardAmount;
                    break;
                case MapDirection.West:
                    moveX = -MoveTowardAmount;
                    break;
                case MapDirection.North:
                    moveZ = MoveTowardAmount;
                    break;
                case MapDirection.South:
                    moveZ = -MoveTowardAmount;
                    break;
                default:
                    break;
            }

            _originalPosition = root.localPosition;
            DOTween.Sequence()
                .AppendInterval(0.6f)
                .Append(root.DOLocalMove(_originalPosition + new Vector3(moveX, 0, moveZ), 0.15f));

            _sequence = DOTween.Sequence();
            _sequence.AppendInterval(0.15f);
            _sequence.AppendCallback(() =>
            {
                animator.SetTrigger(AnimatorTrigger);
            });

            var ultimateAttackController = CharacterAnimator.ultimateAttackController.gameObject;

            _audioSource = ultimateAttackController.AddComponent<AudioSource>();

            _attackingAnimation = ultimateAttackController.AddComponent<Animation>();
            var animationClip = Instantiate(Animation);
            _attackingAnimation.AddClip(animationClip, "attacking");
            _attackingAnimation.Play("attacking");

            return _sequence;
        }

        internal override void OnReset()
        {
            var root = CharacterAnimator.root;
            root.DOLocalMove(_originalPosition, 0.15f);

            _sequence.Kill(true);
            Destroy(_audioSource);
            Destroy(_attackingAnimation);
            Destroy(this);

        }

        internal override void OnPrepare()
        {
            ShowChargeEffect();
        }

        internal override void OnAttack1()
        {
            FocusLineController.Instance.ShowFocusLine(false, 1);
            _audioSource.clip = AttackClip;
            _audioSource.Play();
            GameCamera.Instance.Camera.transform.DOShakeRotation(0.6f, 1.5f, 40);
        }

        internal override void OnDown()
        {
        }

        private void ShowChargeEffect()
        {
            DOTween
                .Sequence()
                .AppendCallback(() =>
                {
                    _audioSource.clip = ChargeClip;
                    _audioSource.Play();
                    Effect.SetActive(true);
                })
                .AppendInterval(_CHARGE_EFFECT_DURATION)
                .AppendCallback(() => Effect.SetActive(false))
                .Play();
        }
    }
}
