using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.CharacterTurn;

namespace Taiga.Core.Unity.Character
{

    internal class UpdateCharacterStatus_WhenChracterTurnFinished : ReactiveSystem<GameEntity>
    {
        public GameContext game;

        public UpdateCharacterStatus_WhenChracterTurnFinished(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var characterId = entity.AsCharacterTurn(game).CharacterId;
                var character = game.AsCharacterContext().GetCharacter(characterId);
                var characterGameObject = character.AsGameObject();
                characterGameObject
                    .GetComponent<CharacterStatusPresenter>()
                    .ShowAsCurrentTurn(false);
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(CharacterTurnEvents.OnCharacterTurnFinish);
        }
    }

}
