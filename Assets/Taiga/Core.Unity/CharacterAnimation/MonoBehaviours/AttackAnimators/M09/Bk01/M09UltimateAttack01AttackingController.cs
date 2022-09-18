using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation
{
    public class M09UltimateAttack01AttackingController : BaseM09UltimateAttack01
    {
        private const float FreezeChargeEffectPositionX = -0.32f;
        private const float FreezeChargeEffectPositionY = 0.8f;
        private const float FreezeChargeEffectPositionZ = -0.32f;
        private const float ChargeEffectDuration = 1.7f;
        private const float EffectLargeScale = 1.5f;
        private const float MoveTowardAmount = 0.4f;
        const string AnimatorTrigger = "attack_4";

        public override CharacterAnimator CharacterAnimator { get; set; }
        public MapDirection Direction { get; set; }
        public AudioClip NormalPunchClip { get; set; }
        public AudioClip HardPunchClip { get; set; }
        public AudioClip ChargeClip { get; set; }
        private GameObject ChargeEffect => CharacterAnimator.chargeEffect.gameObject;

        private GameObject Effect
        {
            get
            {
                switch (Direction)
                {
                    case MapDirection.East:
                        return CharacterAnimator.effect3.gameObject;
                    case MapDirection.West:
                        return CharacterAnimator.effect4.gameObject;
                    case MapDirection.North:
                        return CharacterAnimator.effect2.gameObject;
                    case MapDirection.South:
                        return CharacterAnimator.effect1.gameObject;
                    default:
                        return CharacterAnimator.effect1.gameObject;
                }
            }
        }

        private Sequence _sequence;
        private Animation _attackingAnimation;
        private AudioSource _audioSource;
        private Vector3 _originalPosition;

        internal void ShowPunchEffect()
        {
            DOTween
                .Sequence()
                .AppendCallback(() => { Effect.SetActive(true); })
                .AppendInterval(0.2f)
                .AppendCallback(() => { Effect.SetActive(false); })
                .Play();
        }

        internal void ShowPunchEffectLarge()
        {
            DOTween
                .Sequence()
                .AppendCallback(() =>
                {
                    Effect.SetActive(true);
                    Effect.GetComponent<Animator>().StopPlayback();
                    Effect.transform.localScale = new Vector3(EffectLargeScale, EffectLargeScale, EffectLargeScale);
                })
                .AppendInterval(0.6f)
                .AppendCallback(() => { Effect.GetComponent<Animator>().StartPlayback(); })
                .AppendInterval(0.2f)
                .AppendCallback(() => { Effect.gameObject.SetActive(false); })
                .Play();
        }

        private void ShowChargeEffect(float x = 0, float y = -1, float z = 0)
        {
            var originPos = Effect.transform.localPosition;
            // if (y != -1)
            // {
            //     ChargeEffect.transform.localPosition = new Vector3(Effect.transform.localPosition.x+x, y, Effect.transform.localPosition.z+z);
            // }

            if (!CharacterAnimator.bodyRenderer.flipX)
                ChargeEffect.transform.localPosition = new Vector3(0.255f, 0.26f, 0);
            else
                ChargeEffect.transform.localPosition = new Vector3(-0.254f, 0.249f, 0);

            DOTween
                .Sequence()
                .AppendCallback(() =>
                {
                    Debug.Log("SHHHHHHHHHHH");
                    _audioSource.clip = ChargeClip;
                    _audioSource.Play();
                    ChargeEffect.SetActive(true);
                })
                .AppendInterval(ChargeEffectDuration)
                .AppendCallback(() =>
                {
                    ChargeEffect.SetActive(false);
                    ChargeEffect.transform.localPosition = originPos;
                })
                .Play();
        }

        internal override Sequence Initialize()
        {
            var animator = CharacterAnimator.animator;
            var root = CharacterAnimator.root;

            CharacterAnimator.Facing = Direction;
            var moveX = 0f;
            var moveZ = 0f;
            GameObject effect;
            switch (Direction)
            {
                case MapDirection.East:
                    effect = CharacterAnimator.effect3.gameObject;
                    moveX = MoveTowardAmount;
                    break;
                case MapDirection.West:
                    effect = CharacterAnimator.effect4.gameObject;
                    moveX = -MoveTowardAmount;
                    break;
                case MapDirection.North:
                    effect = CharacterAnimator.effect2.gameObject;
                    moveZ = MoveTowardAmount;
                    break;
                case MapDirection.South:
                    effect = CharacterAnimator.effect1.gameObject;
                    moveZ = -MoveTowardAmount;
                    break;
                default:
                    effect = CharacterAnimator.effect1.gameObject;
                    break;
            }


            _originalPosition = root.localPosition;

            _sequence = DOTween.Sequence();
            _sequence.Append(root.DOLocalMove(_originalPosition + new Vector3(moveX, 0, moveZ), 0.15f));
            _sequence.AppendCallback(() => { animator.SetTrigger(AnimatorTrigger); });

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
            _sequence.Kill(true);
            Destroy(_audioSource);
            Destroy(_attackingAnimation);
            Destroy(this);
        }

        internal override void OnStepToward()
        {
        }

        internal override void OnAttack1()
        {
            FocusLineController.Instance.ShowFocusLine(false, 1.5f);
            ShowPunchEffect();
            _audioSource.clip = NormalPunchClip;
            _audioSource.Play();
        }

        internal override void OnAttack2()
        {
            ShowPunchEffect();
            _audioSource.Play();
        }

        internal override void OnAttack3()
        {
            ShowPunchEffect();
            _audioSource.Play();
        }

        internal override void OnAttack4()
        {
            ShowPunchEffect();
            _audioSource.Play();
        }

        internal override void OnAttack5()
        {
            FocusLineController.Instance.ShowFocusLine(false, 0.6f);

            ShowPunchEffectLarge();
            _audioSource.clip = HardPunchClip;
            _audioSource.Play();

            var root = CharacterAnimator.root;
            root.DOLocalMove(_originalPosition, 0.15f);
        }

        internal override void OnDown()
        {
        }

        // internal override void OnFreeze()
        // {
        //     Debug.Log("FREEZE");
        //     ShowChargeEffect(FreezeChargeEffectPositionX, FreezeChargeEffectPositionY, FreezeChargeEffectPositionZ);
        // }
    }
}