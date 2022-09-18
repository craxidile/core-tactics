using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.CharacterAction;
using Taiga.Core.CharacterTurn;

namespace Taiga.Core.Unity.CharacterActions
{
    public class ClearAskingActions_WhenCharacterTurnFinished : ReactiveSystem<GameEntity>
    {
        GameContext game;

        public ClearAskingActions_WhenCharacterTurnFinished(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var characterTurn = entity.AsCharacterTurn(game);
                var characterId = characterTurn.CharacterId;

                var character = game
                    .AsCharacterContext()
                    .GetCharacter(characterId);

                var characterActionsPresenter = character
                    .AsGameObject()
                    .GetComponent<CharacterActionsPresenter>();

                characterActionsPresenter.ClearAsking();

                CharacterActionsPanel.Instance?.ClearAsking();
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(CharacterTurnEvents.OnCharacterTurnFinish);
        }
    }
}
