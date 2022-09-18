using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Taiga.Core.Character;
using Taiga.Core.Character.Attack;
using Taiga.Core.Character.Placement;
using Taiga.Core.CharacterSequence;
using Taiga.Core.Unity.Audio.Providers;
using Taiga.Core.Unity.CharacterAnimation.Base;
using Taiga.Core.Unity.CharacterAnimation.Bk01;
using Taiga.Core.Unity.Effect.Providers;
using Taiga.Core.Unity.Preset;
using Taiga.Core.Unity.UltimateAttack.Providers;
using Taiga.Utils;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation
{
    [Serializable]
    public class AudioKeyPair
    {
        public string name;
        public AudioClip audio;
    }

    public class CharacterAnimator : MonoBehaviour
    {
        public IGameAudioPreset AudioPreset { get; set; }
        public IGameUltimateAttackPreset UltimateAttackPreset { get; set; }

        static readonly Dictionary<AttackType, ICharacterAttackAnimator> attackAnimators =
            new Dictionary<AttackType, ICharacterAttackAnimator>
            {
                { AttackType.Attack1, new BasicAttackAnimator() },
                { AttackType.Attack2, new BasicAttackAnimator() },
                { AttackType.SpecialAttack1, new BasicAttackAnimator() }
            };

        [SerializeField] internal Transform directedEffectRoot;
        [SerializeField] internal Transform directedEffectSprite;
        [SerializeField] internal Transform effect1;
        [SerializeField] internal Transform effect2;
        [SerializeField] internal Transform effect3;
        [SerializeField] internal Transform effect4;
        [SerializeField] internal Transform effect5;
        [SerializeField] internal Transform effect6;
        [SerializeField] internal Transform chargeEffect;
        [SerializeField] internal Transform ultimateAttackController;
        [SerializeField] internal Transform throwableDock;

        [SerializeField] internal Transform root;

        [SerializeField] internal Transform body;

        [SerializeField] internal SpriteRenderer bodyRenderer;

        [SerializeField] internal Transform shadow;
        [SerializeField] private HitTextEffectController textEffectController;

        public Animator animator;
        public GameAssetsPreset.CharacterAnimationConfig animationConfig;

        private bool _walking;

        GameContext context;

        Tween mainTween;

        Tween MainTween
        {
            get => mainTween;
            set
            {
                if (value == null)
                {
                    if (MainTween != null && MainTween.IsActive())
                    {
                        MainTween.Kill();
                    }

                    mainTween = null;
                    return;
                }

                mainTween = DOTween
                    .Sequence()
                    .Append(value);
            }
        }

        public MapDirection Facing { get; set; }

        private void Awake()
        {
            context = Contexts.sharedInstance.game;
        }

        private void Update()
        {
            if (!_walking) return;
            SpriteRearranger.Rearrange();
        }

        internal void ShowTextEffect(HitTextEffectType type, float duration,
            Vector3 position = default(Vector3),
            Vector3 rotation = default(Vector3),
            Vector3 scale = default(Vector3))
        {
            textEffectController.ShowTextEffect(type, duration, position, rotation, scale);
        }

        public void SetFinishCallbackOnce(Action onFinish)
        {
            MainTween.OnComplete(() => onFinish());
        }

        public void SetPlacement(MapDirection mapDirection, Vector2Int position)
        {
            MainTween = null;
            Facing = mapDirection;
            root.position = position.GameToUnityPosition();
        }

        public void Stand(float seconds = 0)
        {
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() => animator.SetTrigger("stand"));
            sequence.AppendInterval(seconds);
            MainTween = sequence;
        }

        public void Walk(IEnumerable<Movement> movements)
        {
            _walking = true;
            var sequence = DOTween.Sequence();
            var animationPreset = context.GetProvider<ICharacterAnimationPreset>();
            var fromPosition = root.position.UnityToRoundGamePosition();

            foreach (var movement in movements)
            {
                var toPosition = movement.toPosition;
                var toUnityPosition = toPosition.GameToUnityPosition();
                var facing = movement.direction;
                var tween = root
                    .DOMove(
                        toUnityPosition,
                        duration: (toPosition - fromPosition).magnitude / animationPreset.WalkSpeed
                    )
                    .SetEase(Ease.Linear)
                    .OnStart(() => Facing = facing);
                fromPosition = toPosition;
                sequence.Append(tween);
            }

            sequence.OnStart(() => animator.SetTrigger("walk"));
            sequence.OnComplete(() =>
            {
                //animator.SetTrigger("stand");
                animator.SetTrigger("guard");
                _walking = false;
            });
            MainTween = sequence;
        }

        public void Attack(
            AttackType attackType,
            MapDirection direction,
            string attackerName = null,
            Core.Character.CharacterEntity character = default
        )
        {
            var characterAttack = character.AsCharacter_Attack();
            var attackingControllerType = characterAttack.GetAttackingControllerType(attackType);
            if (attackingControllerType != null)
            {
                Debug.Log($">>found_controllers<< {character.ArchitypeId} {attackType} {attackingControllerType}");
                var specialAttackController = ultimateAttackController.gameObject;
                var attackingController =
                    (BaseCharacterTimelineController)specialAttackController.AddComponent(attackingControllerType);
                attackingController.CharacterAnimator = this;
                attackingController.CharacterEntity = character;
                attackingController.ArchitypeId = character.ArchitypeId;
                attackingController.AttackType = attackType;
                attackingController.Direction = direction;
                attackingController.AttackAnimationClip = characterAttack.GetAttackTimeline(attackType);
                MainTween = attackingController.OnStart();
                return;
            }

            ICharacterAttackAnimator attackAnimator = null;
            Debug.Log($">>attacker_name<< {attackerName}");
            // TODO FIX THIS
            if (attackType == AttackType.SpecialAttack1 && attackerName == "Inoue Masaru")
            {
                Debug.Log($">>attacker_name<< matched");
                attackAnimator = new M09UltimateAttackAnimator()
                {
                    AudioPreset = AudioPreset,
                    UltimateAttackPreset = UltimateAttackPreset,
                };
            }
            else if (attackType.ToString().Contains("Special") && attackerName == "Aragaki")
            {
                Debug.Log($">>attacker_name<< matched");
                attackAnimator = new M05UltimateAttackAnimator()
                {
                    AttackType = attackType,
                    AudioPreset = AudioPreset,
                    UltimateAttackPreset = UltimateAttackPreset,
                };
            }
            else if (attackType.ToString().Contains("Attack1") && attackerName == "Aragaki")
            {
                Debug.Log($">>attacker_name<< matched");
                attackAnimator = new M05UltimateAttack00Animator()
                {
                    AudioPreset = AudioPreset,
                    UltimateAttackPreset = UltimateAttackPreset,
                };
            }
            else if (attackType.ToString().Contains("Special") && attackerName == "Arashi Taiga")
            {
                Debug.Log($">>attacker_name<< matched");
                attackAnimator = new M01UltimateAttackAnimator()
                {
                    AttackType = attackType,
                    AudioPreset = AudioPreset,
                    UltimateAttackPreset = UltimateAttackPreset,
                    CharcterPlacement = character.AsCharacter_Placement()
                };
            }
            else if (attackType.ToString().Contains("Attack1") && attackerName == "Yasuda Kento")
            {
                Debug.Log($">>attacker_name<< matched");
                attackAnimator = new S17UltimateAttackAnimator()
                {
                    AudioPreset = AudioPreset,
                    UltimateAttackPreset = UltimateAttackPreset,
                };
            }
            else if (attackType.ToString().Contains("Attack1") && attackerName == "Goto Minato")
            {
                Debug.Log($">>attacker_name<< matched");
                attackAnimator = new S22UltimateAttack00Animator()
                {
                    AudioPreset = AudioPreset,
                    UltimateAttackPreset = UltimateAttackPreset,
                };
            }
            else if (attackType.ToString().Contains("Attack1") && attackerName == "Ueno Yuji")
            {
                Debug.Log($">>attacker_name<< matched");
                attackAnimator = new M03UltimateAttack00Animator()
                {
                    AudioPreset = AudioPreset,
                    UltimateAttackPreset = UltimateAttackPreset,
                };
            }
            else
            {
                attackAnimator = attackAnimators[attackType];
            }

            if (attackAnimator is BasicAttackAnimator)
            {
                ((BasicAttackAnimator)attackAnimator).AudioPreset = AudioPreset;
            }

            Debug.Log(">>test<< " + attackType.ToString() + ", " + attackerName);

            MainTween = attackAnimator.Attack(this, direction);
        }

        public void Damaged(
            AttackType attackType,
            MapDirection bumpDirection,
            Vector2Int offsetRelativeToAttacker,
            Vector2Int? endPosition,
            Vector2Int? bumpPosition,
            Action onBump,
            string attackerName = null,
            Vector2Int? attackerPosition = null,
            Core.Character.CharacterEntity attackerCharacter = default
        )
        {
            var attackerCharacterAttack = attackerCharacter.AsCharacter_Attack();
            var damagedControllerType = attackerCharacterAttack.GetDamagedControllerType(attackType);
            if (damagedControllerType != null)
            {
                // Debug.Log($">>found_controllers<< {attackerCharacter.ArchitypeId} {attackType}");
                var specialAttackController = ultimateAttackController.gameObject;
                var damagedController =
                    (BaseCharacterTimelineController)specialAttackController.AddComponent(damagedControllerType);
                damagedController.CharacterAnimator = this;
                damagedController.CharacterEntity = attackerCharacter;
                damagedController.ArchitypeId = null;
                damagedController.AttackType = attackType;
                damagedController.Direction = bumpDirection;
                damagedController.AttackerPosition = attackerPosition;
                damagedController.AttackAnimationClip = attackerCharacterAttack.GetAttackTimeline(attackType);
                damagedController.OnBump = (bool bumpPositionExists) =>
                {
                    CreateBumpDamagedTween(
                        bumpDirection: bumpDirection,
                        endPosition: endPosition,
                        bumpPosition: !bumpPositionExists ? null : bumpPosition,
                        onBump: onBump,
                        isOverrideAnimation: true
                    );
                };
                MainTween = damagedController.OnStart();
                return;
            }

            ICharacterAttackAnimator attackAnimator = null;
            // TODO FIX THIS
            if (attackType == AttackType.SpecialAttack1 && attackerName == "Inoue Masaru")
            {
                attackAnimator = new M09UltimateAttackAnimator()
                {
                    AudioPreset = AudioPreset,
                    UltimateAttackPreset = UltimateAttackPreset,
                };
            }
            else if (attackType.ToString().Contains("Special") && attackerName == "Aragaki")
            {
                attackAnimator = new M05UltimateAttackAnimator()
                {
                    AttackType = attackType,
                    AudioPreset = AudioPreset,
                    UltimateAttackPreset = UltimateAttackPreset,
                };
            }
            else if (attackType.ToString().Contains("Attack1") && attackerName == "Aragaki")
            {
                Debug.Log($">>attacker_name<< matched");
                attackAnimator = new M05UltimateAttack00Animator()
                {
                    AudioPreset = AudioPreset,
                    UltimateAttackPreset = UltimateAttackPreset,
                };
            }
            else if (attackType.ToString().Contains("Special") && attackerName == "Arashi Taiga")
            {
                Debug.Log($">>attacker_name<< matched");
                attackAnimator = new M01UltimateAttackAnimator()
                {
                    AttackType = attackType,
                    AudioPreset = AudioPreset,
                    UltimateAttackPreset = UltimateAttackPreset,
                };
            }
            else if (attackType.ToString().Contains("Attack1") && attackerName == "Yasuda Kento")
            {
                Debug.Log($">>attacker_name<< matched");
                attackAnimator = new S17UltimateAttackAnimator()
                {
                    AudioPreset = AudioPreset,
                    UltimateAttackPreset = UltimateAttackPreset,
                };
            }
            else if (attackType.ToString().Contains("Attack1") && attackerName == "Goto Minato")
            {
                Debug.Log($">>attacker_name<< matched");
                attackAnimator = new S22UltimateAttack00Animator()
                {
                    AudioPreset = AudioPreset,
                    UltimateAttackPreset = UltimateAttackPreset,
                };
            }

            else if (attackType.ToString().Contains("Attack1") && attackerName == "Ueno Yuji")
            {
                Debug.Log($">>attacker_name<< matched");
                attackAnimator = new M03UltimateAttack00Animator()
                {
                    AudioPreset = AudioPreset,
                    UltimateAttackPreset = UltimateAttackPreset,
                };
            }
            else
            {
                attackAnimator = attackAnimators[attackType];
            }

            if (attackAnimator is BasicAttackAnimator)
            {
                ((BasicAttackAnimator)attackAnimator).AudioPreset = AudioPreset;
            }

            var tween = attackAnimator.Damaged(
                characterAnimator: this,
                offsetRelativeToAttacker: offsetRelativeToAttacker,
                fromDirection: bumpDirection,
                endPosition: endPosition,
                bumpPosition: bumpPosition,
                onBump: onBump,
                attackerPosition: attackerPosition
            );
            MainTween = tween;
        }

        public void Block(
            AttackType attackType,
            MapDirection bumpDirection
        )
        {
            var attackAnimator = attackAnimators[attackType];
            MainTween = attackAnimator.Blocked(this, bumpDirection);
        }

        public void Dead()
        {
            var tween = DOVirtual.DelayedCall(1f, () => { });
            tween.OnStart(
                () => animator.SetTrigger("die")
            );
            MainTween = tween;
        }

        public void BumpDamaged(
            MapDirection bumpDirection,
            Vector2Int? endPosition,
            Vector2Int? bumpPosition,
            Action onBump
        )
        {
            var sequence = DOTween.Sequence();
            var animationPreset = context.GetProvider<ICharacterAnimationPreset>();
            var currentPosition = root.position.UnityToRoundGamePosition();

            Tween tween;
            if (endPosition == null)
            {
                sequence
                    .AppendInterval(2)
                    .OnStart(() =>
                    {
                        Facing = bumpDirection.GetOppsite();
                        animator.SetTrigger("damaged");
                    });
            }
            else if (bumpPosition == null)
            {
                tween = root.DOMove(
                        endPosition.Value.GameToUnityPosition(),
                        duration: (currentPosition - endPosition.Value).magnitude / animationPreset.DamagedFlySpeed
                    )
                    .SetEase(Ease.Linear)
                    .OnStart(() =>
                    {
                        Facing = bumpDirection.GetOppsite();
                        animator.SetTrigger("fly");
                    })
                    .OnComplete(() => { animator.SetTrigger("up"); });

                sequence.Append(tween);
            }
            else if (endPosition != bumpPosition)
            {
                tween = root.DOMove(
                        bumpPosition.Value.GameToUnityPosition(),
                        duration: (currentPosition - endPosition.Value).magnitude / animationPreset.DamagedFlySpeed
                    )
                    .SetEase(Ease.Linear)
                    .OnStart(() =>
                    {
                        Facing = bumpDirection.GetOppsite();
                        animator.SetTrigger("fly");
                    })
                    .OnComplete(() => { onBump?.Invoke(); });

                sequence.Append(tween);

                tween = root.DOMove(
                        endPosition.Value.GameToUnityPosition(),
                        duration: (endPosition.Value - bumpPosition.Value).magnitude / animationPreset.DamagedFlySpeed
                    )
                    .SetEase(Ease.Linear)
                    .OnComplete(() => { animator.SetTrigger("up"); });

                sequence.Append(tween);
            }
            else
            {
                tween = root.DOMove(
                        bumpPosition.Value.GameToUnityPosition(),
                        duration: (currentPosition - endPosition.Value).magnitude / animationPreset.DamagedFlySpeed
                    )
                    .SetEase(Ease.Linear)
                    .OnStart(() =>
                    {
                        Facing = bumpDirection.GetOppsite();
                        animator.SetTrigger("fly");
                    })
                    .OnComplete(() =>
                    {
                        onBump?.Invoke();
                        animator.SetTrigger("up");
                    });

                sequence.Append(tween);
            }

            MainTween = sequence;
        }


        public Tween CreateBumpDamagedTween(
            MapDirection bumpDirection,
            Vector2Int? endPosition,
            Vector2Int? bumpPosition,
            Action onBump,
            bool isOverrideAnimation = false
        )
        {
            var sequence = DOTween.Sequence();
            var animationPreset = context.GetProvider<ICharacterAnimationPreset>();
            var currentPosition = root.position.UnityToRoundGamePosition();

            Tween tween;
            if (endPosition == null)
            {
                sequence
                    .AppendInterval(2)
                    .OnStart(() =>
                    {
                        Facing = bumpDirection.GetOppsite();
                        if (!isOverrideAnimation)
                            animator.SetTrigger("damaged");
                    });
            }
            else if (bumpPosition == null)
            {
                tween = root.DOMove(
                        endPosition.Value.GameToUnityPosition(),
                        duration: (currentPosition - endPosition.Value).magnitude / animationPreset.DamagedFlySpeed
                    )
                    .SetEase(Ease.Linear)
                    .OnStart(() =>
                    {
                        Facing = bumpDirection.GetOppsite();
                        if (!isOverrideAnimation)
                            animator.SetTrigger("fly");
                    })
                    .OnComplete(() =>
                    {
                        if (!isOverrideAnimation)
                            animator.SetTrigger("up");
                    });

                sequence.Append(tween);
            }
            else if (endPosition != bumpPosition)
            {
                tween = root.DOMove(
                        bumpPosition.Value.GameToUnityPosition(),
                        duration: (currentPosition - endPosition.Value).magnitude / animationPreset.DamagedFlySpeed
                    )
                    .SetEase(Ease.Linear)
                    .OnStart(() =>
                    {
                        Facing = bumpDirection.GetOppsite();
                        if (!isOverrideAnimation)
                            animator.SetTrigger("fly");
                    })
                    .OnComplete(() => { onBump?.Invoke(); });

                sequence.Append(tween);

                tween = root.DOMove(
                        endPosition.Value.GameToUnityPosition(),
                        duration: (endPosition.Value - bumpPosition.Value).magnitude / animationPreset.DamagedFlySpeed
                    )
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        if (!isOverrideAnimation)
                            animator.SetTrigger("up");
                    });

                sequence.Append(tween);
            }
            else
            {
                tween = root.DOMove(
                        bumpPosition.Value.GameToUnityPosition(),
                        duration: (currentPosition - endPosition.Value).magnitude / animationPreset.DamagedFlySpeed
                    )
                    .SetEase(Ease.Linear)
                    .OnStart(() =>
                    {
                        Facing = bumpDirection.GetOppsite();
                        if (!isOverrideAnimation)
                            animator.SetTrigger("fly");
                    })
                    .OnComplete(() =>
                    {
                        onBump?.Invoke();
                        if (!isOverrideAnimation)
                            animator.SetTrigger("up");
                    });

                sequence.Append(tween);
            }

            return sequence;
        }
    }
}