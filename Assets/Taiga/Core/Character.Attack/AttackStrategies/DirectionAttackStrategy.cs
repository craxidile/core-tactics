using System;
using System.Collections.Generic;
using System.Linq;
using Taiga.Core.Character.Placement;
using Taiga.Core.Map;
using UnityEngine;

namespace Taiga.Core.Character.Attack
{
    public abstract class DirectionAttackStrategy : IAttackStrategy
    {
        DirectionSelection selection;
        protected CharacterEntity character;
        protected bool throughable;

        public DirectionAttackStrategy(CharacterEntity character, bool throughable = true)
        {
            this.character = character;
            this.throughable = throughable;
            this.selection = new DirectionSelection(this);
        }

        class DirectionSelection : IAttackStrategy_DirectionSelection, IAttackStrategy_FinalSelection
        {
            DirectionAttackStrategy strategy;

            public DirectionSelection(DirectionAttackStrategy strategy)
            {
                this.strategy = strategy;
            }

            static ICollection<MapDirection> SelectableDirections => new HashSet<MapDirection>()
            {
                MapDirection.East,
                MapDirection.North,
                MapDirection.West,
                MapDirection.South
            };

            ICollection<MapDirection> eligibleDirections = new HashSet<MapDirection>();

            CharacterEntity character => strategy.character;

            GameContext game => character.context;

            GameEntity entity => character.entity;

            MapDirection? selectedDirection;

            public ICollection<Vector2Int> SelectablePositions
            {
                get
                {
                    var placementContext = game
                        .AsCharacterPlacementContext();

                    var mapContext = game
                        .AsMapContext();

                    var characterPosition = character
                        .AsCharacter_Placement()
                        .Position;

                    var highlightPositions = new HashSet<Vector2Int>();
                    // var allDirections = SelectableDirections;
                    var allDirections = SelectableDirections;
                    eligibleDirections.Clear();

                    foreach (var direction in allDirections)
                    {
                        foreach (var selectablePositionOffset in strategy.DamagePositionOffsetsPerDirection)
                        {
                            var position = characterPosition + selectablePositionOffset
                                .TransformToMapDirection(direction);

                            if (!mapContext.IsMapCellExist(position))
                            {
                                continue;
                            }

                            // TODO: Fix this for every strategy
                            highlightPositions.Add(position);

                            var targetCharacter = placementContext.GetCharacter(position);
                            if (targetCharacter == null)
                            {
                                continue;
                            }
                            if (targetCharacter.AsCharacter().OwnerPlayerId == character.OwnerPlayerId)
                            {
                                continue;
                            }

                            eligibleDirections.Add(direction);
                        }
                    }

                    return highlightPositions;
                }
            }

            public ICollection<Vector2Int> FocusablePositions
            {
                get
                {
                    var placementContext = game
                        .AsCharacterPlacementContext();

                    var mapContext = game
                        .AsMapContext();

                    var characterPosition = character
                        .AsCharacter_Placement()
                        .Position;

                    var highlightPositions = new HashSet<Vector2Int>();
                    // var allDirections = SelectableDirections;
                    var allDirections = SelectableDirections;
                    eligibleDirections.Clear();

                    foreach (var direction in allDirections)
                    {
                        foreach (var selectablePositionOffset in strategy.DamagePositionOffsetsPerDirection)
                        {
                            var position = characterPosition + selectablePositionOffset
                                .TransformToMapDirection(direction);

                            if (!mapContext.IsMapCellExist(position))
                            {
                                continue;
                            }

                            // TODO: Fix this for every strategy
                            highlightPositions.Add(position);

                            // var targetCharacter = placementContext.GetCharacter(position);
                            // if (targetCharacter == null)
                            // {
                            //     continue;
                            // }
                            // if (targetCharacter.AsCharacter().OwnerPlayerId == character.OwnerPlayerId)
                            // {
                            //     continue;
                            // }

                            eligibleDirections.Add(direction);
                        }
                    }

                    return highlightPositions;
                }
            }

            public ICollection<Vector2Int> SelectedPositions
            {
                get
                {
                    if (selectedDirection == null)
                    {
                        return new HashSet<Vector2Int>();
                    }

                    var mapContext = game
                        .AsMapContext();

                    var characterPosition = character
                        .AsCharacter_Placement()
                        .Position;

                    var selectedPosition = strategy
                        .DamagePositionOffsetsPerDirection
                        .Select(positionOffset =>
                        {
                            return characterPosition + positionOffset
                                .TransformToMapDirection(selectedDirection.Value);
                        })
                        .Where(position => mapContext.IsMapCellExist(position));

                    return strategy.throughable ? new HashSet<Vector2Int>(selectedPosition) : new HashSet<Vector2Int>() { selectedPosition.First() };
                }
            }

            public IEnumerable<CharacterDamageInfo> CharacterDamageInfos
            {
                get
                {
                    if (selectedDirection == null)
                    {
                        return Array.Empty<CharacterDamageInfo>();
                    }

                    var mapContext = game
                        .AsMapContext();

                    var characterPosition = character
                        .AsCharacter_Placement()
                        .Position;

                    var damageInfos = strategy
                        .DamagePositionOffsetsPerDirection
                        .Select(positionOffset =>
                        {
                            var position = characterPosition + positionOffset
                                .TransformToMapDirection(selectedDirection.Value);
                            return (positionOffset, position);
                        })
                        .Select(pair =>
                        {
                            var targetCharacter = character
                                .AsCharacter_Attack()
                                .GetDamagableCharacter(pair.position);
                            return (pair.positionOffset, pair.position, targetCharacter);
                        })
                        .Where(pair => pair.targetCharacter != null)
                        .Select(pair =>
                        {
                            var targetCharacter = pair.targetCharacter.Value;
                            return (pair.positionOffset, pair.position, targetCharacter);
                        })
                        .Select(pair =>
                        {
                            strategy.GetCharacterDamageInfo(
                                selectedDirection: selectedDirection.Value,
                                targetCharacter: pair.targetCharacter,
                                out var damageInfo
                            );
                            return damageInfo;
                        });

                    return strategy.throughable ? damageInfos : new HashSet<CharacterDamageInfo>() { damageInfos.First() };
                }
            }

            public bool HasSelectedPositions => selectedDirection != null;

            public bool IsSelectedPositionsValid => selectedDirection != null && eligibleDirections.Contains(selectedDirection.Value);

            public MapDirection AttackDirection => selectedDirection.Value;

            public void Select(MapDirection direction)
            {
                this.selectedDirection = direction;
            }

        }

        public IAttackStrategy_Selection Selection => selection;

        protected abstract ICollection<Vector2Int> DamagePositionOffsetsPerDirection { get; }

        public abstract AttackType AttackType { get; }

        protected abstract bool GetCharacterDamageInfo(
            MapDirection selectedDirection,
            CharacterEntity targetCharacter,
            out CharacterDamageInfo info
        );

    }
}
