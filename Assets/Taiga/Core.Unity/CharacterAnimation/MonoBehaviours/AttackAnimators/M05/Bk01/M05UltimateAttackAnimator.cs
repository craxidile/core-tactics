using System;
using DG.Tweening;
using Taiga.Core.Character.Attack;
using Taiga.Core.Unity.Audio.Providers;
using Taiga.Core.Unity.Demo.Providers;
using Taiga.Core.Unity.UltimateAttack.Providers;
using UnityEditor;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.Bk01
{

    public class M05UltimateAttackAnimator : ICharacterAttackAnimator
    {
        public AttackType AttackType { get; set; }
        public IGameAudioPreset AudioPreset { get; set; }
        public IGameUltimateAttackPreset UltimateAttackPreset { get; set; }

        public Tween Attack(
            CharacterAnimator characterAnimator,
            MapDirection direction
        )
        {
            var ultimateAttackController = characterAnimator.ultimateAttackController.gameObject;
            // TODO Will fix this later
            switch (AttackType)
            {
                case AttackType.SpecialAttack2:
                    var attackingController1 = ultimateAttackController.AddComponent<M05UltimateAttack01AttackingController>();
                    attackingController1.CharacterAnimator = characterAnimator;
                    attackingController1.Direction = direction;
                    attackingController1.Animation = UltimateAttackPreset.GetAnimationByName("m05_001");
                    attackingController1.AttackClip = AudioPreset.GetAudioSourceByName("m09_punch_hard");
                    attackingController1.ChargeClip = AudioPreset.GetAudioSourceByName("charge");
                    return attackingController1.Initialize();
                case AttackType.SpecialAttack3:
                    var attackingController2 = ultimateAttackController.AddComponent<M05UltimateAttack02AttackingController>();
                    attackingController2.CharacterAnimator = characterAnimator;
                    attackingController2.Direction = direction;
                    attackingController2.Animation = UltimateAttackPreset.GetAnimationByName("m05_002");
                    attackingController2.ChargeClip = AudioPreset.GetAudioSourceByName("charge");
                    return attackingController2.Initialize();
                case AttackType.SpecialAttack4:
                    var attackingController3 = ultimateAttackController.AddComponent<M05UltimateAttack03AttackingController>();
                    attackingController3.CharacterAnimator = characterAnimator;
                    attackingController3.Direction = direction;
                    attackingController3.Animation = UltimateAttackPreset.GetAnimationByName("m05_003");
                    attackingController3.LastKickClip = AudioPreset.GetAudioSourceByName("m09_punch_hard");
                    attackingController3.ChargeClip = AudioPreset.GetAudioSourceByName("charge");
                    return attackingController3.Initialize();
                default:
                    throw new ArgumentOutOfRangeException();
            }

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

            var ultimateAttackController = characterAnimator.ultimateAttackController.gameObject;
            // TODO Will fix this later
            switch (AttackType)
            {
                case AttackType.SpecialAttack2:
                    var attackedController = ultimateAttackController.AddComponent<M05UltimateAttack01AttackedController>();
                    attackedController.Animation = UltimateAttackPreset.GetAnimationByName("m05_001");
                    attackedController.MapDirection = bumpDirection;
                    attackedController.CharacterAnimator = characterAnimator;
                    attackedController.AttackerPosition = attackerPosition;
                    return attackedController.Initialize();
                case AttackType.SpecialAttack3:
                    var attackedController2 = ultimateAttackController.AddComponent<M05UltimateAttack02AttackedController>();
                    attackedController2.Animation = UltimateAttackPreset.GetAnimationByName("m05_002");
                    attackedController2.HitClip = AudioPreset.GetAudioSourceByName("m09_punch_hard");
                    attackedController2.MapDirection = bumpDirection;
                    attackedController2.CharacterAnimator = characterAnimator;
                    attackedController2.AttackerPosition = attackerPosition;
                    return attackedController2.Initialize();
                case AttackType.SpecialAttack4:
                    var attackedController3 = ultimateAttackController.AddComponent<M05UltimateAttack03AttackedController>();
                    attackedController3.Animation = UltimateAttackPreset.GetAnimationByName("m05_003");
                    attackedController3.HitClip = AudioPreset.GetAudioSourceByName("m09_punch_hard");
                    attackedController3.MapDirection = bumpDirection;
                    attackedController3.CharacterAnimator = characterAnimator;
                    attackedController3.AttackerPosition = attackerPosition;
                    attackedController3.BumpEndPosition = endPosition.HasValue ? endPosition.Value : default;
                    attackedController3.OnBump = onBump;
                    return attackedController3.Initialize();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Tween Blocked(
            CharacterAnimator characterAnimator,
            MapDirection bumpDirection
        )
        {
            var sequence = DOTween.Sequence();
            // sequence.AppendCallback(() =>
            // {
            //     characterAnimator.Facing = bumpDirection;
            //     characterAnimator.animator.SetTrigger("guard");
            // });
            //
            // sequence.AppendInterval(1);
            return sequence;
        }
    }
}
