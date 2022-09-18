using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using Taiga.Core.Character.Attack;
using UnityEngine;

namespace Taiga.Core.CharacterActionPrediction.Attack
{
    public sealed class CharacterAction_AttackType : IComponent
    {
        public AttackType value;
    }

    public sealed class CharacterAction_AttackStrategy : IComponent
    {
        public IAttackStrategy value;
    }

    public sealed class CharacterAction_AttackStrategySelection : IComponent
    {
        public IAttackStrategy_Selection value;
    }

    public sealed class CharacterAction_AttackStrategyPreviousSelectionSelectedPosition : IComponent
    {
        public List<IEnumerable<Vector2Int>> value;
    }

    public sealed class CharacterAction_AttackStrategySelection_PossiblePositions : IComponent
    {
        public IEnumerable<Vector2Int> positions;
    }

    public sealed class CharacterAction_AttackStrategySelection_Focus : IComponent
    {
        public Vector2Int? position;
        public MapDirection? direction;
    }

    public sealed class CharacterAction_AttackStrategySelection_FocusPredictedPositions : IComponent
    {
        public IEnumerable<Vector2Int> positions;
    }

    public sealed class CharacterAction_AttackStrategySelection_FocusValid : IComponent
    {
    }

    public sealed class CharacterAction_AttackPrediction : IComponent
    {
        public MapDirection attackDirection;
        public IEnumerable<CharacterDamageInfo> damageInfos;
    }

    public sealed class CharacterAction_AttackCombo : IComponent
    {
        public IEnumerable<CharacterDamageInfo> damageInfos;
    }
}
