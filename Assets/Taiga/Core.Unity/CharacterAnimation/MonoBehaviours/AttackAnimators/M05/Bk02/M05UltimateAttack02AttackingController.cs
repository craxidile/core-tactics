using System.Linq;
using DG.Tweening;
using Sirenix.Utilities;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation
{
    public class M05UltimateAttack02AttackingController : BaseM05UltimateAttack02
    {
        private const float _CHARGE_EFFECT_DURATION = 1.7f;
        private const float EffectLargeScale = 1.5f;
        private const float MoveTowardAmount = 0.3f;
        private const string AnimatorTrigger = "attack_special_2";

        public override CharacterAnimator CharacterAnimator { get; set; }
        public MapDirection Direction { get; set; }
        public AudioClip ChargeClip { get; set; }

        private GameObject Effect => CharacterAnimator.chargeEffect.gameObject;

        private Sequence _sequence;
        private Animation _attackingAnimation;
        private AudioSource _audioSource;
        private Vector3 _originalPosition;

        internal void ShowChargeEffect()
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

        internal override Sequence Initialize()
        {
            var animator = CharacterAnimator.animator;
            var root = CharacterAnimator.root;

            CharacterAnimator.Facing = Direction;
            _originalPosition = root.localPosition;

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

        private void FreezeTime(float duration)
        {
            var characterAnimators = FindObjectsOfType<CharacterAnimator>().Where(x => x != CharacterAnimator);
            characterAnimators.ForEach(x => x.animator.speed = 0);
            DOTween.Sequence()
                .SetUpdate(true)
                .AppendInterval(duration)
                .AppendCallback(() =>
                {
                    characterAnimators.ForEach(x => x.animator.speed = 1);
                });
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

        internal override void OnAttackNW()
        {
            FocusLineController.Instance.ShowFocusLine(false, 1);
        }

        internal override void OnAttackN()
        {
        }

        internal override void OnAttackNE()
        {
        }

        internal override void OnAttackE()
        {
        }

        internal override void OnAttackSE()
        {
        }

        internal override void OnAttackS()
        {
        }

        internal override void OnAttackSW()
        {
        }

        internal override void OnAttackW()
        {
        }

        internal override void OnDown()
        {
        }
    }
}
