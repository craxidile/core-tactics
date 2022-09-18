using System;
using DG.Tweening;
using Taiga.Core.Character.Attack;
using Taiga.Core.Unity.Audio.Providers;
using Taiga.Core.Unity.Demo.Providers;
using Taiga.Core.Unity.UltimateAttack.Providers;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Taiga.Core.Unity.CharacterAnimation
{

    public class BkM09UltimateAttackAnimator : ICharacterAttackAnimator
    {

        public IGameAudioPreset AudioPreset { get; set; }
        public IGameUltimateAttackPreset UltimateAttackPreset { get; set; }

        public Tween Attack(
            CharacterAnimator characterAnimator,
            MapDirection direction
        )
        {
            var ultimateAttackController = characterAnimator.ultimateAttackController.gameObject;
            var attackingController = ultimateAttackController.AddComponent<M09UltimateAttack01AttackingController>();
            attackingController.CharacterAnimator = characterAnimator;
            attackingController.Direction = direction;
            attackingController.Animation = UltimateAttackPreset.GetAnimationByName("m09_001");
            attackingController.NormalPunchClip = AudioPreset.GetAudioSourceByName("m09_punch_normal");
            attackingController.HardPunchClip = AudioPreset.GetAudioSourceByName("m09_punch_hard");
            attackingController.ChargeClip = AudioPreset.GetAudioSourceByName("charge");

            return attackingController.Initialize();
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
            var attackedController = ultimateAttackController.AddComponent<M09UltimateAttack01AttackedController>();
            attackedController.Animation = UltimateAttackPreset.GetAnimationByName("m09_001");
            attackedController.MapDirection = bumpDirection;
            attackedController.CharacterAnimator = characterAnimator;

            return attackedController.Initialize();
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
