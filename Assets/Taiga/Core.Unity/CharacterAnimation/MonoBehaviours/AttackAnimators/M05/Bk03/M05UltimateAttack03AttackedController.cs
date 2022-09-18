using System;
using DG.Tweening;
using Taiga.Core.Unity.Camera;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation
{
    public class M05UltimateAttack03AttackedController : BaseM05UltimateAttack03
    {
        private const float _DistanceScale = 0.1f;

        public override CharacterAnimator CharacterAnimator { get; set; }
        public Vector2Int? AttackerPosition { get; internal set; }
        public MapDirection MapDirection { get; set; }
        public Vector2Int BumpEndPosition { get; set; }
        public Action OnBump { get; internal set; }
        public AudioClip HitClip { get; internal set; }

        private Vector3 _originalPosition;
        private Sequence _sequence;
        private Animation _attackingAnimation;
        private AudioSource _audioSource;
        private GameObject _hitEffect;
        private Sequence _currentDamageTween;
        private bool _isFlip => GameCamera.Instance.Angle == 135 && MapDirection == MapDirection.East;


        internal override Sequence Initialize()
        {
            CharacterAnimator.Facing = MapDirection.GetOppsite();

            var root = CharacterAnimator.root;

            _originalPosition = root.localPosition;
            var direction = (AttackerPosition.Value.GameToUnityPosition() - _originalPosition) * _DistanceScale;
            var movePos = _originalPosition + direction;

            DOTween.Sequence()
                .AppendInterval(0f)
                .AppendCallback(() => CharacterAnimator.root.DOLocalMove(movePos, 0f));

            SetupHitEffect(MapDirection);

            _sequence = DOTween
                .Sequence()
                .AppendInterval(5.6667f)
                .OnKill(() =>
                {
                    Debug.Log(">>killed<<");
                });

            var ultimateAttackController = CharacterAnimator.ultimateAttackController.gameObject;
            _attackingAnimation = ultimateAttackController.AddComponent<Animation>();
            _audioSource = ultimateAttackController.AddComponent<AudioSource>();

            var animationClip = Instantiate(Animation);
            _attackingAnimation.AddClip(animationClip, "attacking");
            _attackingAnimation.Play("attacking");

            return _sequence;
        }


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
                })
                .Play();

            Destroy(_audioSource);
            Destroy(_attackingAnimation);
            Destroy(this);
        }

        internal override void OnPrepare()
        {
        }

        internal override void OnAttack1()
        {
            if (!_isFlip && _originalPosition.UnityToRoundGamePosition().GetMapDirectionFromTarget(AttackerPosition.Value, MapDirection) == MapDirection.NorthWest)
            {
                DamageFly();
            }
            else if (_isFlip && _originalPosition.UnityToRoundGamePosition().GetMapDirectionFromTarget(AttackerPosition.Value, MapDirection) == MapDirection.NorthEast)
            {
                DamageFly();
            }
        }

        internal override void OnAttack2()
        {
            var isInPosition = _originalPosition.UnityToRoundGamePosition().GetMapDirectionFromTarget(AttackerPosition.Value, MapDirection) == MapDirection.North;
            if (isInPosition)
                DamageFly();
        }

        internal override void OnAttack3()
        {
            if (!_isFlip && _originalPosition.UnityToRoundGamePosition().GetMapDirectionFromTarget(AttackerPosition.Value, MapDirection) == MapDirection.NorthEast)
            {
                DamageFly();
            }
            else if (_isFlip && _originalPosition.UnityToRoundGamePosition().GetMapDirectionFromTarget(AttackerPosition.Value, MapDirection) == MapDirection.NorthWest)
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
            LastDamage();
        }

        internal override void OnUnfreeze()
        {
            _currentDamageTween.timeScale = 1;
            CharacterAnimator.animator.Play("LayTrigger", -1, 0);
            // ShakeCharacter();

            CharacterAnimator.CreateBumpDamagedTween(
                bumpDirection: MapDirection,
                endPosition: BumpEndPosition,
                bumpPosition: null,
                onBump: OnBump,
                isOverrideAnimation: true
            );
        }

        internal override void OnDown()
        {
            CharacterAnimator.animator.Play("SitTrigger", -1, 0);
        }

        private void LastDamage()
        {
            MaskCharacter();
            ShowHitEffect(1, true);
            ShowTextEffect(0.5f, true);
        }

        private void DamageFly()
        {
            MaskCharacter();
            CharacterAnimator.animator.SetTrigger($"fly");
            ShakeCharacter();
            ShowHitEffect(1);
            ShowTextEffect(1);
            _audioSource.PlayOneShot(HitClip);

            var faceCam = CharacterAnimator.bodyRenderer.GetComponent<AlwaysFaceCamera>();
            var originalY = CharacterAnimator.bodyRenderer.transform.localPosition.y;
            faceCam.enabled = false;

            _currentDamageTween = DOTween.Sequence()
                .Append(CharacterAnimator.bodyRenderer.transform.DOLocalMoveY(1f, 0.5f))
                .Join(CharacterAnimator.bodyRenderer.transform.DOLocalRotate(new Vector3(0, 0, -180), 0.2f).SetLoops(3, LoopType.Incremental))
                .Append(CharacterAnimator.bodyRenderer.transform.DOLocalMoveY(originalY, 0.5f))
                .Join(CharacterAnimator.bodyRenderer.transform.DOLocalRotate(Vector3.zero, 0.5f))
                .AppendCallback(() => faceCam.enabled = true);
        }

        private void SetupHitEffect(MapDirection direction)
        {
            var attackPositionOffset = (_originalPosition.UnityToRoundGamePosition() - AttackerPosition.Value).NormalizeByDirection(direction.GetNormalizeDirection());
            switch (direction)
            {
                case MapDirection.East:
                    _hitEffect = attackPositionOffset.x == 1 ? CharacterAnimator.effect2.gameObject : CharacterAnimator.effect1.gameObject;
                    break;
                case MapDirection.West:
                    _hitEffect = attackPositionOffset.x == 1 ? CharacterAnimator.effect1.gameObject : CharacterAnimator.effect2.gameObject;
                    break;
                case MapDirection.North:
                    _hitEffect = attackPositionOffset.x == 1 ? CharacterAnimator.effect4.gameObject : CharacterAnimator.effect3.gameObject;
                    break;
                case MapDirection.South:
                    _hitEffect = attackPositionOffset.x == 1 ? CharacterAnimator.effect3.gameObject : CharacterAnimator.effect4.gameObject;
                    break;
            }
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
        }

        private void ShowTextEffect(float duration, bool detach = false)
        {
            var effect = CharacterAnimator.effect5;
            var originLocalPos = effect.localPosition;
            var parent = effect.parent;
            if (detach)
            {
                effect.SetParent(null);
            }

            effect.gameObject.SetActive(true);
            effect.DOShakePosition(0.3f, new Vector3(0.04f, 0.04f, 0.04f), 20, 20, false, false);

            DOTween.Sequence()
                .AppendInterval(duration)
                .AppendCallback(() =>
                {
                    effect.gameObject.SetActive(false);
                    if (detach)
                    {
                        effect.SetParent(parent);
                        effect.localPosition = originLocalPos;
                    }
                });
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
    }
}
