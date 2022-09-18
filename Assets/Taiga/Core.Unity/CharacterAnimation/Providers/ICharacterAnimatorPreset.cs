using System;
using System.Collections.Generic;
using DG.Tweening;
using Taiga.Core.Character.Attack;
using Taiga.Core.Unity.Preset;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation
{

    public interface ICharacterAnimatorPreset : IProvider
    {
        RuntimeAnimatorController GetAnimatorController(string architypeId);

        ICharacterAttackAnimator GetAttackAnimator(AttackType attackType);

        GameAssetsPreset.CharacterAnimationConfig GetAnimationConfig(string architypeId);
        
        public Dictionary<AttackType, GameAssetsPreset.SpecialAttackAssets> GetSpecialAttackAssetMap(string architypeId);

    }

    public interface ICharacterAttackAnimator
    {

        Tween Attack(
            CharacterAnimator characterAnimator,
            MapDirection direction
        );

        Tween Damaged(
            CharacterAnimator characterAnimator,
            MapDirection fromDirection,
            Vector2Int offsetRelativeToAttacker,
            Vector2Int? endPosition,
            Vector2Int? bumpPosition,
            Action onBump,
            Vector2Int? attackerPosition
        );

        Tween Blocked(
            CharacterAnimator characterAnimator,
            MapDirection fromDirection
        );
    }
}
