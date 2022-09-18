using System;
using DG.Tweening;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.Bk01
{
    public class M01UltimateAttack01AttackedController : BaseM01UltimateAttack01
    {
        private const float _DistanceScale = 0.2f;
        private const float _EffectMoveX = -0.2f;

        public override CharacterAnimator CharacterAnimator { get; set; }
        public Vector2Int? AttackerPosition { get; set; }
        public MapDirection MapDirection { get; set; }
        public AudioClip HitClip { get; set; }

        private Vector3 _originalBodyPosition;
        private Sequence _sequence;
        private GameObject _hitEffect;
        private Vector3 _originalEffect5Position;
        private Vector3 _originalEffect6Position;
        private Action _onDamage;
        private AudioSource _audioSource;

        private Vector3 Effect6Position
        {
            get
            {
                return new Vector3(
                    _originalEffect6Position.x + _EffectMoveX,
                    _originalEffect6Position.y,
                    _originalEffect6Position.z
                );
            }
        }

        internal override Sequence Initialize()
        {
            _originalEffect5Position = CharacterAnimator.effect5.localPosition;
            _originalEffect6Position = CharacterAnimator.effect6.localPosition;

            CharacterAnimator.Facing = MapDirection.GetOppsite();
            _originalBodyPosition = CharacterAnimator.body.localPosition;

            SetupHitEffect(MapDirection);

            _sequence = DOTween
                .Sequence()
                .AppendInterval(6f)
                .OnKill(() =>
                {
                    Debug.Log(">>killed<<");
                });

            var ultimateAttackController = CharacterAnimator.ultimateAttackController.gameObject;

            _audioSource = ultimateAttackController.AddComponent<AudioSource>();

            AttackingAnimation = ultimateAttackController.AddComponent<Animation>();
            var animationClip = Instantiate(Animation);

            AttackingAnimation.AddClip(animationClip, StateName);
            AttackingAnimation.Play(StateName);

            return _sequence;
        }

        internal void SetOnDamangeEvent(Action action) => _onDamage = action;

        internal override void OnReset()
        {
            _sequence.Kill();
            HideEffects();

            var animator = CharacterAnimator.animator;

            DOTween
                .Sequence()
                .AppendCallback(() =>
                {
                    CharacterAnimator.animator.SetTrigger($"stand");
                    CharacterAnimator.body.DOLocalMove(_originalBodyPosition, 0.2f);
                })
                .Play();

            Destroy(_audioSource);
            Destroy(AttackingAnimation);
            Destroy(this);
        }

        internal override void OnPrepare()
        {
        }

        internal override void OnRun()
        {
        }

        internal override void OnAttack1()
        {
            CharacterAnimator.animator.SetTrigger($"damaged");
            MaskCharacter();
            ShakeCharacter();
            ShowHitEffect(1, true);
            _audioSource.clip = HitClip;
            _audioSource.Play();
        }

        internal override void OnDamaged()
        {
            CharacterAnimator.animator.Play("LayTrigger", -1, 0);
            CharacterAnimator.body.DOShakePosition(0.5f, new Vector3(0, 0.15f, 0), 10, 0);
            _onDamage?.Invoke();
        }

        internal override void OnGetUp()
        {
            CharacterAnimator.animator.Play("SitTrigger", -1, 0);
        }

        private void ShakeCharacter()
        {
            CharacterAnimator.body.DOShakePosition(0.5f, new Vector3(0.15f, 0, 0.15f), 50, 20, false, false);
        }

        private void ShowHitEffect(float duration, bool detach = false)
        {
            var originLocalPos = _hitEffect.transform.localPosition;
            var parent = _hitEffect.transform.parent;
            if (detach)
            {
                _hitEffect.transform.SetParent(null);
            }

            _hitEffect.SetActive(true);
            DOTween.Sequence()
                .AppendInterval(duration)
                .AppendCallback(() =>
                {
                    _hitEffect.SetActive(false);
                    if (detach)
                    {
                        _hitEffect.transform.SetParent(parent);
                        _hitEffect.transform.localPosition = originLocalPos;
                    }
                });
        }

        private void HideEffects()
        {
            _hitEffect.SetActive(false);

            CharacterAnimator.effect5.localPosition = _originalEffect5Position;
            CharacterAnimator.effect6.localPosition = _originalEffect6Position;
            CharacterAnimator.effect5.gameObject.SetActive(false);
            CharacterAnimator.effect6.gameObject.SetActive(false);
        }

        private void MaskCharacter(bool shortDelay = true)
        {
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(shortDelay ? 0f : 0.18f);
            sequence.AppendCallback(() =>
            {
                CharacterAnimator.bodyRenderer.color = new Color(1f, 0.5f, 0.5f, 1f);
            });
            sequence.AppendInterval(shortDelay ? 0.15f : 0.25f);
            sequence.AppendCallback(() =>
            {
                CharacterAnimator.bodyRenderer.color = Color.white;
            });
            sequence.Play();
        }

        private void SetupHitEffect(MapDirection direction)
        {
            switch (direction)
            {
                case MapDirection.East:
                    _hitEffect = CharacterAnimator.effect4.gameObject;
                    break;
                case MapDirection.West:
                    _hitEffect = CharacterAnimator.effect3.gameObject;
                    break;
                case MapDirection.North:
                    _hitEffect = CharacterAnimator.effect1.gameObject;
                    break;
                case MapDirection.South:
                    _hitEffect = CharacterAnimator.effect2.gameObject;
                    break;
                default:
                    _hitEffect = CharacterAnimator.effect2.gameObject;
                    break;
            }
        }
    }
}
