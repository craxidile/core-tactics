using System;
using System.Collections.Generic;
using System.Linq;
using Taiga.Core.Character.Placement;
using Taiga.Core.Map;
using UnityEngine;
using UnityEngine.Assertions;

namespace Taiga.Core.Character.Attack
{
    public class Area_Attack_Strategy : IAttackStrategy
    {
        readonly int attackPoint;

        private int cellCount;
        PositionSelection selection;
        protected CharacterEntity character;

        public IAttackStrategy_Selection Selection => selection;

        public Area_Attack_Strategy(CharacterEntity character, int attackPoint, int cellCount)
        {
            this.character = character;
            this.attackPoint = attackPoint;
            this.cellCount = cellCount;
            
            selection = new PositionSelection(this);
        }

        class PositionSelection : IAttackStrategy_PositionSelection, IAttackStrategy_FinalSelection
        {
            Area_Attack_Strategy strategy;

            public PositionSelection(Area_Attack_Strategy strategy)
            {
                this.strategy = strategy;
            }

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
                    var cellCount = strategy.cellCount;

                    for (var i = -cellCount; i <= cellCount; i++)
                    for (var j = -cellCount; j <= cellCount; j++)
                    {
                        if (Math.Abs(i) + Math.Abs(j) > cellCount) continue;
                        var position = characterPosition + new Vector2Int(i, j);
                        if (!mapContext.IsMapCellExist(position)) continue;
                        highlightPositions.Add(position);
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

                    if (!SelectablePositions.Any(position => position.Equals(selectedPosition.Value)))
                        return new HashSet<Vector2Int>();

                    return new HashSet<Vector2Int>(new[] {selectedPosition.Value});
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

                    var damageInfos = SelectablePositions
                        .Select(position =>
                        {
                            var targetCharacter = character
                                .AsCharacter_Attack()
                                .GetDamagableCharacter(position);
                            return (position, targetCharacter);
                        })
                        .Where(pair => pair.targetCharacter != null)
                        .Select(pair =>
                        {
                            var targetCharacter = pair.targetCharacter.Value;
                            return (pair.position, targetCharacter);
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
                    var characterPosition = strategy
                        .character
                        .AsCharacter_Placement()
                        .Position;

                    var angle = (selectedPosition.Value - characterPosition)
                        .ToVector2()
                        .GetAngle();
                    Debug.Log($">>angle_angle<< {angle}");

                    return (selectedPosition.Value - characterPosition)
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

        protected bool GetCharacterDamageInfo(
            MapDirection selectedDirection,
            CharacterEntity targetCharacter,
            out CharacterDamageInfo info
        )
        {
            var damage = character
                .AsCharacter_Attack()
                .CalculateDamage(
                    targetCharacter.Id,
                    attackPoint: attackPoint,
                    out var level
                );

            var targetCharacterPosition = targetCharacter
                .AsCharacter_Placement()
                .Position;

            info = new CharacterDamageInfo
            {
                characterId = targetCharacter.Id,
                level = level,
                damage = damage,
                hitCount = 1,
                fromDirection = selectedDirection,
                bumpInfo = null,
            };

            return true;
        }
    }
}
