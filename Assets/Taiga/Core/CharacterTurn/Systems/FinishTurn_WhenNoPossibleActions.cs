using System.Collections.Generic;
using System.Linq;
using Entitas;
using Taiga.Core.Character.Health;
using Taiga.Core.CharacterAction;

namespace Taiga.Core.CharacterTurn
{
    internal class FinishTurn_WhenNoPossibleActions : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public FinishTurn_WhenNoPossibleActions(Contexts contexts) : base(contexts.game)
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
            var currentCharacterTurn = game
                .AsCharacterTurnContext()
                .CurrentCharacterTurn;

            if (currentCharacterTurn == null)
            {
                return false;
            }

            var actions = entity
                .characterTurn_PossibleActions
                .actions;

            if (actions.Length < 0)
            {
                return true;
            }

            return actions.Length == 1 && actions[0] == CharacterActionType.EndTurn;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.CharacterTurn_PossibleActions);
        }

    }

}
