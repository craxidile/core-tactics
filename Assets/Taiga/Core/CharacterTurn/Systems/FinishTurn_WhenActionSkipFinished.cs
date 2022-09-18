using System.Collections.Generic;
using Entitas;
using Taiga.Core.CharacterAction;
using UnityEngine;

namespace Taiga.Core.CharacterTurn
{
    internal class FinishCharacterTurn_WhenActionSkipFinished : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public FinishCharacterTurn_WhenActionSkipFinished(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var characterTurn = game
                .AsCharacterTurnContext()
                .CurrentCharacterTurn
                .Value;

            characterTurn.entity.isCharacterTurn_Finished = true;
        }

        protected override bool Filter(GameEntity entity)
        {
            var currentCharacterTurn = game.AsCharacterTurnContext().CurrentCharacterTurn;
            if (currentCharacterTurn == null)
            {
                return false;
            }
            return entity.AsCharacterAction(game).ActionType == CharacterActionType.EndTurn;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(CharacterActionEvents.OnActionFinished);
        }

    }

}
