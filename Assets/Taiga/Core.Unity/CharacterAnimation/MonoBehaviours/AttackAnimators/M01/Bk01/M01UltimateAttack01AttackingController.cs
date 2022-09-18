using DG.Tweening;
using Taiga.Core.Character.Placement;
using Taiga.Core.Unity.Camera;
using Taiga.Core.Unity.Effect.Providers;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.Bk01
{
    public class M01UltimateAttack01AttackingController : BaseM01UltimateAttack01
    {
        private const float ChargeEffectDuration = 1.7f;
        private const string AnimatorTrigger = "attack_special_1";
        private const float RunningTime = 0.75f;
        private const float StartRunningNormalizedTime = 0.56f;
        private const float EndRunningNormalizedTime = 0.62f;
        private const float BeforeAttackNormalizedTime = 0.65f;
        private const float ChargeEffectPositionY = 0.45f;
        private const float BeforeEventAttackNormalizedTime = 0.398f;

        public override CharacterAnimator CharacterAnimator { get; set; }
        public MapDirection Direction { get; set; }
        public AudioClip ChargeClip { get; set; }
        public CharacterEntity_Placement CharacterPlacement { get; set; }

        private GameObject Effect => CharacterAnimator.chargeEffect.gameObject;

        private Sequence _sequence;
        private AudioSource _audioSource;
        private Vector3 _originalPosition;

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

            AttackingAnimation = ultimateAttackController.AddComponent<Animation>();
            var animationClip = Instantiate(Animation);
            AttackingAnimation.AddClip(animationClip, StateName);
            AttackingAnimation.Play(StateName);
            return _sequence;
        }

        internal override void OnReset()
        {
            CharacterPlacement.SetPlacement(CharacterAnimator.root.localPosition.UnityToRoundGamePosition(), Direction);

            _sequence.Kill(true);
            Destroy(_audioSource);
            Destroy(AttackingAnimation);
            Destroy(this);
        }

        internal override void OnPrepare()
        {
            float moveX = 0, moveZ = 0;
            const float moveValue = 0.105f;
            switch (Direction)
            {
                case MapDirection.East:
                    moveX = moveValue;
                    break;
                case MapDirection.West:
                    moveX = -moveValue;
                    break;
                case MapDirection.North:
                    moveZ = moveValue;
                    break;
                case MapDirection.South:
                    moveZ = -moveValue;
                    break;
                default:
                    break;
            }

            ShowChargeEffect(new Vector3(moveX, ChargeEffectPositionY, moveZ));
        }

        internal override void OnRun()
        {
            var victim = FindObjectOfType<M01UltimateAttack01AttackedController>();
            var targetPos = (CharacterAnimator.root.localPosition - victim.CharacterAnimator.root.localPosition)
                .UnityToRoundGamePosition()
                .TransformByDirection(Core.Direction.Backward);
            var x = targetPos.x > 0 ? -1 : 1;
            var y = targetPos.y > 0 ? -1 : 1;
            targetPos = new Vector2Int(targetPos.x != 0 ? targetPos.x + x : targetPos.x, targetPos.y != 0 ? targetPos.y + y : targetPos.y);

            var animator = CharacterAnimator.animator;
            if (targetPos.magnitude >= 1)
            {
                CharacterAnimator.root
                    .DOLocalMove(CharacterAnimator.root.localPosition + targetPos.GameToUnityPosition(), RunningTime * targetPos.magnitude / 3)
                    .SetEase(Ease.InOutSine)
                    .OnUpdate(() =>
                    {
                        var distance = (CharacterAnimator.root.localPosition - victim.CharacterAnimator.root.localPosition)
                            .UnityToRoundGamePosition()
                            .TransformByDirection(Core.Direction.Backward).magnitude;

                        if (distance > 1 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= EndRunningNormalizedTime)
                        {
                            animator.Play(StateName, -1, StartRunningNormalizedTime);
                        }
                        else if (distance <= 1 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < EndRunningNormalizedTime)
                        {
                            FinishRunning();
                        }
                    });
            }
            else
            {
                FinishRunning();
            }

            void FinishRunning()
            {
                float moveX = 0, moveZ = 0;
                const float moveValue = 0.3f;
                const float chargeSpeed = 2;
                switch (Direction)
                {
                    case MapDirection.East:
                        moveX = -moveValue;
                        break;
                    case MapDirection.West:
                        moveX = moveValue;
                        break;
                    case MapDirection.North:
                        moveZ = -moveValue;
                        break;
                    case MapDirection.South:
                        moveZ = moveValue;
                        break;
                    default:
                        break;
                }

                ShowChargeEffect(new Vector3(moveX, ChargeEffectPositionY, moveZ), chargeSpeed);
                animator.Play(StateName, -1, BeforeAttackNormalizedTime);
                SetEventNormallizedTime(BeforeEventAttackNormalizedTime);
                victim.SetEventNormallizedTime(BeforeEventAttackNormalizedTime);
            }
        }

        internal override void OnAttack1()
        {
            FocusLineController.Instance.ShowFocusLine(false, 1);
            GameCamera.Instance.Camera.transform.DOShakeRotation(1f, 1.5f, 40);
            CharacterAnimator.ShowTextEffect(HitTextEffectType.JP05, 1);
        }

        internal override void OnDamaged()
        {
        }

        internal override void OnGetUp()
        {
        }

        private Animator _chargeEffectAnimator;

        private void ShowChargeEffect(Vector3 position = default, float speed = 1)
        {
            _chargeEffectAnimator = _chargeEffectAnimator ?? Effect.GetComponent<Animator>();
            _chargeEffectAnimator.speed = speed;

            var originPos = Effect.transform.localPosition;
            if (position != default)
            {
                Effect.transform.localPosition = position;
            }

            DOTween
                .Sequence()
                .AppendCallback(() =>
                {
                    _audioSource.clip = ChargeClip;
                    _audioSource.Play();
                    Effect.SetActive(true);
                })
                .AppendInterval(ChargeEffectDuration / speed)
                .AppendCallback(() =>
                {
                    Effect.SetActive(false);
                    Effect.transform.localPosition = originPos;
                })
                .Play();
        }
    }
}
