using System;
using System.Collections.Generic;
using System.Linq;
using Taiga.Core.Character.Placement;
using Taiga.Core.Map;
using UnityEngine;

namespace Taiga.Core.Character.Attack
{
    public abstract class CharacterSpecificAttackStrategy : IAttackStrategy
    {

        AttackType AttackType;
        CharacterSelection selection;
        protected CharacterEntity character;

        public CharacterSpecificAttackStrategy(CharacterEntity character)
        {
            this.character = character;
            this.selection = new CharacterSelection(this);
        }

        public class PositionSelection : IAttackStrategy_PositionSelection, IAttackStrategy_FinalSelection
        {
            CharacterSpecificAttackStrategy strategy;
            CharacterSelection characterSelection;
            public Vector2Int? selectedPosition;

            CharacterEntity character => strategy.character;

            GameContext game => character.context;

            GameEntity entity => character.entity;

            public PositionSelection(
                CharacterSpecificAttackStrategy strategy,
                CharacterSelection characterSelection
            )
            {
                this.strategy = strategy;
                this.characterSelection = characterSelection;
            }

            public MapDirection AttackDirection => characterSelection.AttackDirection;

            public IEnumerable<CharacterDamageInfo> CharacterDamageInfos
            {
                get
                {

                    var characterPosition = character
                        .AsCharacter_Placement()
                        .Position;

                    var selectedPositionOffset = selectedPosition.Value - characterPosition;

                    selectedPositionOffset = selectedPositionOffset
                        .NormalizeByDirection(
                            characterSelection
                                .AttackDirection
                                .GetNormalizeDirection()
                        );

                    strategy.GetCharacterDamageInfo(
                        attackDirection: characterSelection.AttackDirection,
                        targetCharacter: characterSelection.SelectedCharacter,
                        targetPosititonOffset: selectedPositionOffset,
                        out var info
                    );
                    return new CharacterDamageInfo[] { info };
                }
            }

            public ICollection<Vector2Int> FocusablePositions => SelectablePositions;

            public ICollection<Vector2Int> SelectablePositions
            {
                get
                {
                    Debug.Log(">>selectable_positions<<");
                    var mapContext = game
                        .AsMapContext();

                    var placementContext = game
                        .AsCharacterPlacementContext();

                    var characterPosition = character
                        .AsCharacter_Placement()
                        .Position;

                    var highlightPositions = new HashSet<Vector2Int>();

                    foreach (var selectablePositionOffset in strategy.TargetSelectionPositionOffsets)
                    {
                        var position = characterPosition + selectablePositionOffset
                            .TransformToMapDirection(characterSelection.AttackDirection);

                        if (!mapContext.IsMapCellExist(position))
                        {
                            continue;
                        }

                        if (placementContext.GetCharacter(position) != null)
                        {
                            continue;
                        }

                        highlightPositions.Add(position);
                    }

                    return highlightPositions;
                }
            }

            public bool HasSelectedPositions => selectedPosition != null;

            public bool IsSelectedPositionsValid
            {
                get
                {
                    return SelectablePositions.Contains(selectedPosition.Value);
                }
            }

            public ICollection<Vector2Int> SelectedPositions
            {
                get
                {
                    if (selectedPosition.Value == characterSelection.selectedPosition.Value)
                    {
                        return new HashSet<Vector2Int>();
                    }

                    if (!SelectablePositions.Contains(selectedPosition.Value))
                    {
                        return new HashSet<Vector2Int>();
                    }

                    return new HashSet<Vector2Int>
                    {
                        selectedPosition.Value
                    };
                }
            }

            public void Select(Vector2Int position)
            {
                selectedPosition = position;
            }
        }

        public class CharacterSelection : IAttackStrategy_PositionSelection, IAttackStrategy_InProgressSelection
        {

            static ICollection<MapDirection> SelectableDirections => new HashSet<MapDirection>()
            {
                MapDirection.East,
                MapDirection.North,
                MapDirection.West,
                MapDirection.South
            };

            CharacterSpecificAttackStrategy strategy;
            public Vector2Int? selectedPosition;

            CharacterEntity character => strategy.character;

            GameContext game => character.context;

            GameEntity entity => character.entity;

            public CharacterSelection(CharacterSpecificAttackStrategy strategy)
            {
                this.strategy = strategy;
            }

            public ICollection<Vector2Int> SelectablePositions
            {
                get
                {
                    var mapContext = game
                        .AsMapContext();

                    var characterPosition = character
                        .AsCharacter_Placement()
                        .Position;

                    var highlightPositions = new HashSet<Vector2Int>();
                    var allDirections = SelectableDirections;

                    foreach (var direction in allDirections)
                    {
                        foreach (var selectablePositionOffset in strategy.CharacterSelectionPositionOffsetsPerDirection)
                        {
                            var position = characterPosition + selectablePositionOffset
                                .TransformToMapDirection(direction);

                            if (!mapContext.IsMapCellExist(position))
                            {
                                continue;
                            }

                            highlightPositions.Add(position);
                        }
                    }

                    return highlightPositions;
                }
            }

            public ICollection<Vector2Int> FocusablePositions => SelectablePositions;

            public bool HasSelectedPositions => selectedPosition != null;

            public bool IsSelectedPositionsValid
            {
                get
                {
                    var targetCharacter = game
                        .AsCharacterPlacementContext()
                        .GetCharacter(selectedPosition.Value);

                    return targetCharacter != null && targetCharacter.Value.OwnerPlayerId != character.OwnerPlayerId;
                }
            }

            public ICollection<Vector2Int> SelectedPositions
            {
                get
                {
                    if (!SelectablePositions.Contains(selectedPosition.Value))
                    {
                        return new HashSet<Vector2Int>();
                    }

                    return new HashSet<Vector2Int> { selectedPosition.Value };
                }
            }

            public void Select(Vector2Int position)
            {
                this.selectedPosition = position;
            }

            public CharacterEntity SelectedCharacter
            {
                get
                {
                    var placementContext = game
                        .AsCharacterPlacementContext();

                    return placementContext
                        .GetCharacter(selectedPosition.Value)
                        .Value;
                }
            }

            public IAttackStrategy_Selection NextSelection => new PositionSelection(strategy, this);

            public MapDirection AttackDirection
            {
                get
                {
                    var selectedCharacterPosition = SelectedCharacter
                        .AsCharacter_Placement()
                        .Position;

                    var characterPosition = character
                        .AsCharacter_Placement()
                        .Position;

                    return (selectedCharacterPosition - characterPosition)
                        .ToVector2()
                        .GetAngle()
                        .GetNearestMapDirection();
                }
            }
        }

        public abstract ICollection<Vector2Int> CharacterSelectionPositionOffsetsPerDirection { get; }

        public abstract ICollection<Vector2Int> TargetSelectionPositionOffsets { get; }

        public IAttackStrategy_Selection Selection => new CharacterSelection(this);

        protected abstract bool GetCharacterDamageInfo(
            MapDirection attackDirection,
            CharacterEntity targetCharacter,
            Vector2Int targetPosititonOffset,
            out CharacterDamageInfo info
        );

    }
}
