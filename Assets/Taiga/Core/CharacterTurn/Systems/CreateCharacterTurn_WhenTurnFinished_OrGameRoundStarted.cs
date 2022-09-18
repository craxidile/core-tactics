using System.Collections.Generic;
using System.Linq;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Health;
using Taiga.Core.GameRound;

namespace Taiga.Core.CharacterTurn
{
    internal class CreateCharacterTurn_WhenTurnFinished_OrGameRoundStarted : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public CreateCharacterTurn_WhenTurnFinished_OrGameRoundStarted(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var characterId = game.game_CharacterTurnOrder.characterIds.First();
            var turnContext = game.AsCharacterTurnContext();
            turnContext.CreateCharacterTurn(characterId);
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.AnyOf(
                    GameMatcher.CharacterTurn_Finished,
                    GameRoundEvents.OnStart
                )
            );
        }

    }

}
