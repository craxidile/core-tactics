using System;
using DG.Tweening;
using Taiga.Core.Character.Attack;
using Taiga.Core.Character.Placement;
using Taiga.Core.Unity.Audio.Providers;
using Taiga.Core.Unity.UltimateAttack.Providers;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.Bk01
{

    public class M01UltimateAttackAnimator : ICharacterAttackAnimator
    {
        public AttackType AttackType { get; set; }
        public IGameAudioPreset AudioPreset { get; set; }
        public IGameUltimateAttackPreset UltimateAttackPreset { get; set; }
        public CharacterEntity_Placement CharcterPlacement { get; set; }

        public Tween Attack(
            CharacterAnimator characterAnimator,
            MapDirection direction
        )
        {
            var ultimateAttackController = characterAnimator.ultimateAttackController.gameObject;
            // TODO Will fix this later
            switch (AttackType)
            {
                case AttackType.SpecialAttack5:
                    var attackingController1 = ultimateAttackController.AddComponent<M01UltimateAttack01AttackingController>();
                    attackingController1.CharacterAnimator = characterAnimator;
                    attackingController1.Direction = direction;
                    attackingController1.Animation = UltimateAttackPreset.GetAnimationByName("m01_001");
                    attackingController1.ChargeClip = AudioPreset.GetAudioSourceByName("charge");
                    attackingController1.CharacterPlacement = CharcterPlacement;
                    return attackingController1.Initialize();
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
                case AttackType.SpecialAttack5:
                    var attackedController = ultimateAttackController.AddComponent<M01UltimateAttack01AttackedController>();
                    attackedController.Animation = UltimateAttackPreset.GetAnimationByName("m01_001");
                    attackedController.MapDirection = bumpDirection;
                    attackedController.HitClip = AudioPreset.GetAudioSourceByName("m09_punch_hard");
                    attackedController.CharacterAnimator = characterAnimator;
                    attackedController.AttackerPosition = attackerPosition;
                    attackedController.SetOnDamangeEvent(() => Bump());
                    return attackedController.Initialize();
                default:
                    throw new ArgumentOutOfRangeException();
            }

            void Bump()
            {
                characterAnimator.CreateBumpDamagedTween(
                    bumpDirection: bumpDirection,
                    endPosition: endPosition,
                    bumpPosition: bumpPosition,
                    onBump: onBump,
                    isOverrideAnimation: true
                );
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
