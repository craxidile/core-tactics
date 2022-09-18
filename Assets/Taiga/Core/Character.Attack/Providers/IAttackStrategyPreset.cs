using System.Collections.Generic;
using UnityEngine;

namespace Taiga.Core.Character.Attack
{
    public interface IAttackStrategyPreset : IProvider
    {
        IAttackStrategy CreateStrategy(
            AttackType attackType,
            int attackPoint,
            CharacterEntity character
        );
    }

    public struct CharacterDamageInfo
    {
        public int characterId;
        public DamageLevel level;
        public int damage;
        public int hitCount;
        public MapDirection fromDirection;
        public CharacterBumpInfo? bumpInfo;
    }

    public struct CharacterBumpInfo
    {
        public Vector2Int toPosition;
        public BumpType type;
    }

    public enum BumpType
    {
        Drag,
        Fly
    }

    public interface IAttackStrategy
    {
        //AttackType AttackType { get; }

        IAttackStrategy_Selection Selection { get; }

    }

    public interface IAttackStrategy_Selection
    {
        ICollection<Vector2Int> SelectablePositions { get; }
        ICollection<Vector2Int> FocusablePositions { get; }

        bool HasSelectedPositions { get; }

        bool IsSelectedPositionsValid { get; }

        ICollection<Vector2Int> SelectedPositions { get; }

    }

    public interface IAttackStrategy_InProgressSelection
    {
        IAttackStrategy_Selection NextSelection { get; }
    }

    public interface IAttackStrategy_FinalSelection
    {
        MapDirection AttackDirection { get; }

        IEnumerable<CharacterDamageInfo> CharacterDamageInfos { get; }
    }

    public interface IAttackStrategy_PositionSelection : IAttackStrategy_Selection
    {
        void Select(Vector2Int position);
    }

    public interface IAttackStrategy_DirectionSelection : IAttackStrategy_Selection
    {
        void Select(MapDirection direction);
    }

}
