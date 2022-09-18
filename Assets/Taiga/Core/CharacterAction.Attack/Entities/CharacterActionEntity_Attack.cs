using System;
using System.Collections.Generic;
using Taiga.Core.Character;
using Taiga.Core.Character.Attack;
using Taiga.Core.Character.Placement;
using UnityEngine;
using UnityEngine.Assertions;

namespace Taiga.Core.CharacterAction.Attack
{

    public static class CharacterActionEntity_AttackExtensions
    {
        public static CharacterActionEntity_Attack AsCharacterAction_Attack(this IGameScopedEntity entity)
        {
            return new CharacterActionEntity_Attack(entity.context, entity.entity);
        }

        public static CharacterActionEntity_Attack AsCharacterAction_Attack(this GameEntity entity, GameContext context)
        {
            return new CharacterActionEntity_Attack(context, entity);
        }
    }

    public struct CharacterActionEntity_Attack : IGameScopedEntity
    {
        public CharacterActionEntity_Attack(GameContext context, GameEntity entity)
        {
            this.context = context;
            this.entity = entity;
        }

        public GameContext context { get; private set; }

        public GameEntity entity { get; private set; }

        public IAttackStrategy Strategy => entity
            .characterAction_AttackStrategy
            .value;

        public IEnumerable<Vector2Int> PreviousSelectionSelectedPositions
        {
            get
            {
                if (!entity.hasCharacterAction_AttackStrategyPreviousSelectionSelectedPosition)
                {
                    return Array.Empty<Vector2Int>();
                }

                var result = new HashSet<Vector2Int>();
                var previousPositionsList = entity
                    .characterAction_AttackStrategyPreviousSelectionSelectedPosition
                    .value;

                foreach (var previousPositions in previousPositionsList)
                {
                    result.UnionWith(previousPositions);
                }

                return result;
            }
        }

        public IEnumerable<Vector2Int> SelectionPossiblePositions => entity
            .characterAction_AttackStrategySelection_PossiblePositions
            .positions;

        public bool HasSelectionFocusPredictedPositions => entity
            .hasCharacterAction_AttackStrategySelection_FocusPredictedPositions;

        public IEnumerable<Vector2Int> SelectionFocusPredictedPositions => entity
            .characterAction_AttackStrategySelection_FocusPredictedPositions
            .positions;

        public bool IsFocus => entity.hasCharacterAction_AttackStrategySelection_Focus;

        public void Focus(MapDirection direction, Vector2Int position)
        {
            if (entity.hasCharacterAction_AttackStrategySelection_Focus)
            {
                var currentFocus = entity.characterAction_AttackStrategySelection_Focus;
                if (currentFocus.direction == direction && currentFocus.position == position)
                {
                    return;
                }
            }

            entity.ReplaceCharacterAction_AttackStrategySelection_Focus(
                newDirection: direction,
                newPosition: position
            );
        }

        public bool IsCurretSelectedValid => entity.isCharacterAction_AttackStrategySelection_FocusValid;

        public bool HasNextSelection => entity.characterAction_AttackStrategySelection.value is IAttackStrategy_InProgressSelection;

        public void NextSelection()
        {
            Assert.IsTrue(HasNextSelection);
            Assert.IsTrue(entity.isCharacterAction_AttackStrategySelection_FocusValid);
            var selection = entity
                .characterAction_AttackStrategySelection
                .value as IAttackStrategy_InProgressSelection;


            var nextSelection = selection.NextSelection;
            entity.ReplaceCharacterAction_AttackStrategySelection(nextSelection);

            if (!entity.hasCharacterAction_AttackStrategyPreviousSelectionSelectedPosition)
            {
                entity.AddCharacterAction_AttackStrategyPreviousSelectionSelectedPosition(
                    new List<IEnumerable<Vector2Int>>()
                );
            }

            var previousSelectedPositions = entity
                .characterAction_AttackStrategyPreviousSelectionSelectedPosition
                .value;

            previousSelectedPositions.Add(
                entity
                    .characterAction_AttackStrategySelection_FocusPredictedPositions
                    .positions
            );

            entity.ReplaceCharacterAction_AttackStrategyPreviousSelectionSelectedPosition(
                previousSelectedPositions
            );

            if (entity.hasCharacterAction_AttackStrategySelection_Focus)
            {
                entity.RemoveCharacterAction_AttackStrategySelection_Focus();
            }

            if (entity.hasCharacterAction_AttackStrategySelection_FocusPredictedPositions)
            {
                entity.RemoveCharacterAction_AttackStrategySelection_FocusPredictedPositions();
            }

            entity.isCharacterAction_AttackStrategySelection_FocusValid = false;
        }

    }
}
