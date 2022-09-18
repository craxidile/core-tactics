using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Placement;
using Taiga.Core.CharacterSequence;
using UnityEngine;

namespace Taiga.Core.CharacterAction.Move
{
    internal class CreateActionSequence_WhenActionExecuted : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public CreateActionSequence_WhenActionExecuted(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var sequenceContext = game.AsCharacterSequenceContext();
            var characterContext = game.AsCharacterContext();
            foreach (var entity in entities)
            {
                var action = entity.AsCharacterAction(game);
                var moveAction = action.AsCharacterAction_Move();
                var characterId = action.CharacterId;
                var character = characterContext.GetCharacter(characterId);
                var fromPosition = character.AsCharacter_Placement().Position;
                var pathways = moveAction.PredictedPathways;
                var movements = new List<Movement>();
                foreach (var toPosition in pathways)
                {
                    var movement = new Movement
                    {
                        toPosition = toPosition,
                        direction = (toPosition - fromPosition).GetMapDirection()
                    };
                    movements.Add(movement);
                    fromPosition = toPosition;
                }
                sequenceContext.CreateInitialMove(
                    characterId: characterId,
                    movementSteps: movements
                );
            }
        }

        protected override bool Filter(GameEntity entity) => entity.AsCharacterAction(game).ActionType == CharacterActionType.Move;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(CharacterActionEvents.OnActionExecute);
        }

    }
}
