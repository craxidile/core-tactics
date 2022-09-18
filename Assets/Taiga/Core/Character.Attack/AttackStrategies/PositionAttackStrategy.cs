using System;
using System.Collections.Generic;
using System.Linq;
using Taiga.Core.Character.Health;
using Taiga.Core.Character.Placement;
using Taiga.Core.Map;
using UnityEngine;
using UnityEngine.Assertions;

namespace Taiga.Core.Character.Attack
{

    public abstract class PositionAttackStrategy : IAttackStrategy
    {
        PositionSelection selection;
        protected CharacterEntity character;

        public PositionAttackStrategy(CharacterEntity character)
        {
            this.character = character;
            this.selection = new PositionSelection(this);
        }

        class PositionSelection : IAttackStrategy_PositionSelection, IAttackStrategy_FinalSelection
        {
            PositionAttackStrategy strategy;

            public PositionSelection(PositionAttackStrategy strategy)
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

            CharacterEntity character => strategy.character;

            GameContext game => character.context;

            GameEntity entity => character.entity;

            Vector2Int? selectedPosition;

            public ICollection<Vector2Int> FocusablePositions => SelectablePositions;

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
                        foreach (var selectablePositionOffset in strategy.SelectablePositionOffsetsPerDirection)
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

            public ICollection<Vector2Int> SelectedPositions
            {
                get
                {
                    if (this.selectedPosition == null)
                    {
                        return new HashSet<Vector2Int>();
                    }

                    if (!SelectablePositions.Contains(selectedPosition.Value))
                    {
                        return new HashSet<Vector2Int>();
                    }

                    var mapContext = game
                        .AsMapContext();

                    var characterPosition = character
                        .AsCharacter_Placement()
                        .Position;

                    var selectedPositions = strategy
                        .DamagePositionOffsetsFromSelectedPosition
                        .Select(positionOffset => positionOffset.TransformToMapDirection(SelectedPositionDirection))
                        .Select(positionOffset => this.selectedPosition.Value + positionOffset)
                        .Where(position => mapContext.IsMapCellExist(position));

                    return new HashSet<Vector2Int>(selectedPositions);
                }
            }

            public IEnumerable<CharacterDamageInfo> CharacterDamageInfos
            {
                get
                {
                    if (selectedPosition == null)
                    {
                        return Array.Empty<CharacterDamageInfo>();
                    }

                    var mapContext = game
                        .AsMapContext();

                    var characterPosition = character
                        .AsCharacter_Placement()
                        .Position;

                    var damageInfos = strategy
                        .DamagePositionOffsetsFromSelectedPosition
                        .Select(positionOffset =>
                        {
                            var position = this.selectedPosition.Value + positionOffset;
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
                                selectedDirection: AttackDirection,
                                targetCharacter: pair.targetCharacter,
                                out var damageInfo
                            );
                            return damageInfo;
                        });

                    return damageInfos;
                }
            }

            public bool HasSelectedPositions => selectedPosition != null;

            public bool IsSelectedPositionsValid => selectedPosition != null;

            MapDirection SelectedPositionDirection
            {
                get
                {
                    var characterPosititon = strategy
                        .character
                        .AsCharacter_Placement()
                        .Position;

                    return (selectedPosition.Value - characterPosititon)
                        .ToVector2()
                        .GetAngle()
                        .GetNearestMapDirection();
                }
            }

            public MapDirection AttackDirection => SelectedPositionDirection;

            public void Select(Vector2Int position)
            {
                this.selectedPosition = position;
            }
        }

        public IAttackStrategy_Selection Selection => selection;

        protected abstract ICollection<Vector2Int> SelectablePositionOffsetsPerDirection { get; }

        protected abstract ICollection<Vector2Int> DamagePositionOffsetsFromSelectedPosition { get; }

        public abstract AttackType AttackType { get; }

        protected abstract bool GetCharacterDamageInfo(
            MapDirection selectedDirection,
            CharacterEntity targetCharacter,
            out CharacterDamageInfo info
        );

    }
}
