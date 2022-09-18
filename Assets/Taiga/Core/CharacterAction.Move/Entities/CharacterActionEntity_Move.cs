using System;
using System.Collections.Generic;
using Taiga.Core.Character.Placement;
using Taiga.Core.Map;
using UnityEngine;
using UnityEngine.Assertions;

namespace Taiga.Core.CharacterAction.Move
{

    public static class CharacterActionEntity_MoveExtensions
    {
        public static CharacterActionEntity_Move AsCharacterAction_Move(this IGameScopedEntity entity)
        {
            return new CharacterActionEntity_Move(entity.context, entity.entity);
        }

        public static CharacterActionEntity_Move AsCharacterAction_Move(this GameEntity entity, GameContext context)
        {
            return new CharacterActionEntity_Move(context, entity);
        }
    }

    public struct CharacterActionEntity_Move : IGameScopedEntity
    {
        public CharacterActionEntity_Move(GameContext context, GameEntity entity)
        {
            this.context = context;
            this.entity = entity;
        }

        public GameContext context { get; private set; }

        public GameEntity entity { get; private set; }

        public bool CanExecute => entity.hasCharacterAction_MovePrediction;

        public void Focus(Vector2Int position)
        {
            Assert.IsTrue(
                entity.characterAction_MovePossibilities.positions.Contains(position)
            );
            entity.ReplaceCharacterAction_MoveFocus(
                newPosition: position
            );
        }

        public void Unfocus()
        {
            if (entity.hasCharacterAction_MoveFocus)
            {
                entity.RemoveCharacterAction_MoveFocus();
            }

            if (entity.hasCharacterAction_MovePrediction)
            {
                entity.RemoveCharacterAction_MovePrediction();

            }

            var action = entity.AsCharacterAction(context);
            action.CanExecute = false;
        }

        public ICollection<Vector2Int> PossiblePositions => entity.characterAction_MovePossibilities?.positions ?? Array.Empty<Vector2Int>();
        public ICollection<Vector2Int> CharacterBlockages => entity.characterAction_MoveCharacterBlockages?.positions ?? Array.Empty<Vector2Int>();

        public IEnumerable<Vector2Int> PredictedPathways => entity.characterAction_MovePrediction?.pathway ?? Array.Empty<Vector2Int>();

        public Vector2Int? FocusPosition => entity.characterAction_MoveFocus?.position;
    }
}
