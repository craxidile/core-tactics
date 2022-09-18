using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation
{
    public class M03UltimateAttack00AttackingController : BaseM03UltimateAttack00
    {
        private const float EffectLargeScale = 1.5f;
        private const float MoveTowardAmount = 0.4f;
        const string AnimatorTrigger = "attack_1";

        public override CharacterAnimator CharacterAnimator { get; set; }
        public MapDirection Direction { get; set; }
        public AudioClip NormalPunchClip { get; set; }
        public AudioClip HardPunchClip { get; set; }

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
                .AppendCallback(() =>
                {
                    Effect.SetActive(true);
                })
                .AppendInterval(0.2f)
                .AppendCallback(() =>
                {
                    Effect.SetActive(false);
                })
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
                .AppendCallback(() =>
                {
                    Effect.GetComponent<Animator>().StartPlayback();
                })
                .AppendInterval(0.2f)
                .AppendCallback(() =>
                {
                    Effect.gameObject.SetActive(false);
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

            Destroy(_audioSource);
            Destroy(_attackingAnimation);
            Destroy(this);
        }

        internal override void OnPunch1()
        {
            Debug.Log(">>punch<<");
            
            _audioSource.clip = NormalPunchClip;
            _audioSource.Play();

            var direction = Direction;

            GameObject effect;
            switch (direction)
            {
                case MapDirection.East:
                    effect = CharacterAnimator.effect3.gameObject;
                    break;
                case MapDirection.West:
                    effect = CharacterAnimator.effect4.gameObject;
                    break;
                case MapDirection.North:
                    effect = CharacterAnimator.effect2.gameObject;
                    break;
                case MapDirection.South:
                    effect = CharacterAnimator.effect1.gameObject;
                    break;
                default:
                    effect = CharacterAnimator.effect1.gameObject;
                    break;
            }
            
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() =>
            {
                effect.SetActive(true);
            });
            sequence.AppendInterval(0.2f);
            sequence.AppendCallback(() =>
            {
                effect.SetActive(false);
            });

            sequence.Play();
        }

        /*internal override void OnPunch2()
        {
            OnPunch1();
        }*/
    }
}
