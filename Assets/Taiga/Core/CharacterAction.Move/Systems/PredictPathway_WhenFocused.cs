using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Placement;
using UnityEngine;
using UnityEngine.Assertions;

namespace Taiga.Core.CharacterAction.Move
{
    internal class PredictPathway_WhenFocused : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public PredictPathway_WhenFocused(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var calculationCache = entity
                        .characterAction_MovePossibilities
                        .pathwayCalculationCache
                    as Dictionary<Vector2Int, Dictionary<MapDirection, Vector2Int[]>>;

                var focusPosition = entity
                    .characterAction_MoveFocus
                    .position;

                // Assert.IsTrue(calculationCache.TryGetValue(focusPosition, out cacheByDirection));
                // Assert.IsTrue(cacheByDirection.Count() > 0);

                if (calculationCache == null) return;
                calculationCache.TryGetValue(focusPosition, out var cacheByDirection);
                if (cacheByDirection == null || cacheByDirection.Count() == 0) return;

                var pathway = cacheByDirection
                    .OrderBy(pair => pair.Value.Length)
                    .Select(pair => pair.Value)
                    .First();

                var minimizedPathway = new List<Vector2Int>();

                Vector2Int? lastPosition = null;
                MapDirection? lastDirection = null;
                foreach (var position in pathway)
                {
                    if (lastPosition == null)
                    {
                        lastPosition = position;
                        continue;
                    }

                    var direction = (position - lastPosition.Value).GetMapDirection();
                    if (lastDirection != null && lastDirection != direction)
                    {
                        minimizedPathway.Add(lastPosition.Value);
                    }

                    lastDirection = direction;
                    lastPosition = position;
                }
                minimizedPathway.Add(lastPosition.Value);

                entity.ReplaceCharacterAction_MovePrediction(
                    newPathway: minimizedPathway
                );

                var action = entity.AsCharacterAction(game);
                action.CanExecute = true;
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.CharacterAction_MoveFocus);
        }

    }
}
