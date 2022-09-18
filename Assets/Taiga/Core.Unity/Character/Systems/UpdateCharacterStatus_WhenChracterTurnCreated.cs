using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Health;
using Taiga.Core.CharacterTurn;

namespace Taiga.Core.Unity.Character
{

    internal class UpdateCharacterStatus_WhenChracterTurnCreated : ReactiveSystem<GameEntity>
    {
        public GameContext game;

        public UpdateCharacterStatus_WhenChracterTurnCreated(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var characterTurn = entity.AsCharacterTurn(game);

                var character = game
                    .AsCharacterContext()
                    .GetCharacter(characterTurn.CharacterId)
                    .Value;

                var characterHealth = character.AsCharacter_Health();
                var characterGameObject = character.AsGameObject();
                var statusPresenter = characterGameObject.GetComponent<CharacterStatusPresenter>();

                statusPresenter.ShowCharacterValues(
                    isLocalPlayer: character.IsLocalPlayer,
                    maxHealth: characterHealth.MaxHealth,
                    health: characterHealth.Health
                );

                statusPresenter.ShowAsCurrentTurn();
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(CharacterTurnEvents.OnCharacterTurn);
        }
    }

}
