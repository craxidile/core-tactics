using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Move;
using Taiga.Core.Character.Placement;
using Taiga.Core.CharacterAction_Move.Move;
using Taiga.Core.Map;
using UnityEngine;
using UnityEngine.Assertions;

namespace Taiga.Core.CharacterAction.Move
{
    internal class CalculatePossibilities_WhenActionCreated : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public CalculatePossibilities_WhenActionCreated(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var action = entity
                    .AsCharacterAction(game);

                var characterId = action.CharacterId;

                var character = game
                    .AsCharacterContext()
                    .GetCharacter(characterId)
                    .Value;

                var position = character
                    .AsCharacter_Placement()
                    .Position;

                var moveLength = character
                    .AsCharacter_Movement()
                    .Length;

                var possiblePositions = CalculateMovePossibilities(
                    start: position,
                    maxLength: moveLength,
                    out var pathwayCalculationCache,
                    out var characterBlockages
                );

                characterBlockages.Add(position);

                entity.ReplaceCharacterAction_MoveCharacterBlockages(characterBlockages);

                entity.ReplaceCharacterAction_MovePossibilities(
                    newPositions: possiblePositions,
                    newPathwayCalculationCache: pathwayCalculationCache
                );
            }
        }

        static IEnumerable<MapDirection> AllDirections = new MapDirection[] { MapDirection.North, MapDirection.East, MapDirection.South, MapDirection.West };

        internal ICollection<Vector2Int> CalculateMovePossibilities(
            Vector2Int start,
            int maxLength,
            out Dictionary<Vector2Int, Dictionary<MapDirection, Vector2Int[]>> calculationCache,
            out List<Vector2Int> characterBlockages
        )
        {
            calculationCache = new Dictionary<Vector2Int, Dictionary<MapDirection, Vector2Int[]>>();
            characterBlockages = new List<Vector2Int>();

            CalculateMovePathways(
                calculationCache,
                characterBlockages,
                currentPathway: new Vector2Int[] { start },
                currentTurn: 0,
                maxLength: maxLength
            );
            calculationCache.Remove(start);

            return calculationCache.Keys;
        }

        void CalculateMovePathways(
            Dictionary<Vector2Int, Dictionary<MapDirection, Vector2Int[]>> calculationCache,
            List<Vector2Int> characterBlockages,
            Vector2Int[] currentPathway,
            int currentTurn,
            int maxLength
        )
        {
            Assert.IsTrue(currentPathway.Length > 0);

            var currentLength = currentPathway.Length - 1;
            var currentDirection = MapDirection.North;
            var currentPosition = currentPathway.Last();

            var isInitialPosition = currentLength == 0;
            var nextDirections = AllDirections;
            var previousPosition = currentPosition;

            if (!isInitialPosition)
            {
                previousPosition = currentPathway[currentPathway.Length - 2];
                currentDirection = (currentPosition - previousPosition).GetMapDirection();

                nextDirections = nextDirections
                    .Where(nextDirection => currentDirection != nextDirection.GetOppsite());
            }

            // if (currentTurn >= 2)
            // {
            //     return;
            // }

            if (currentLength > maxLength)
            {
                return;
            }

            if (!isInitialPosition && !CanWalkToPosition(currentPosition))
            {
                if (game.AsCharacterPlacementContext().GetCharacter(currentPosition) != null)
                {
                    characterBlockages.Add(currentPosition);
                }
                return;
            }

            if (!calculationCache.TryGetValue(currentPosition, out var cacheByDirection))
            {
                cacheByDirection = new Dictionary<MapDirection, Vector2Int[]>();
                calculationCache[currentPosition] = cacheByDirection;
            }

            if (cacheByDirection.TryGetValue(currentDirection, out var existingPathway))
            {
                var existingLength = existingPathway.Length - 1;
                if (currentLength >= existingLength)
                {
                    return;
                }
            }

            cacheByDirection[currentDirection] = currentPathway;
            foreach (var nextDirection in nextDirections)
            {
                var nextPosition = currentPosition + nextDirection.GetUnitVector();
                var nextLength = currentLength + 1;
                var nextTurn = currentTurn;

                if (!isInitialPosition && currentDirection != nextDirection)
                {
                    nextTurn += 1;
                }

                var nextPathway = currentPathway
                    .Append(nextPosition)
                    .ToArray();

                CalculateMovePathways(
                    calculationCache,
                    characterBlockages,
                    currentPathway: nextPathway,
                    currentTurn: nextTurn,
                    maxLength: maxLength
                );
            }

        }

        bool CanWalkToPosition(Vector2Int position)
        {
            var hasMapCell = game
                .AsMapContext()
                .IsMapCellExist(position);

            var hasCharacter = game
                .AsCharacterPlacementContext()
                .GetCharacter(position) != null;

            if (!hasMapCell)
            {
                return false;
            }

            return !hasCharacter;
        }

        protected override bool Filter(GameEntity entity) => entity.AsCharacterAction(game).ActionType == CharacterActionType.Move;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(CharacterActionEvents.OnActionCreated);
        }

    }
}
