using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Health;

namespace Taiga.Core.Unity.Character
{
    internal class UpdateCharacterStatus_WhenCharacterCreated : ReactiveSystem<GameEntity>
    {
        public GameContext game;

        public UpdateCharacterStatus_WhenCharacterCreated(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var character = entity.AsCharacter(game);
                var characterHealth = character.AsCharacter_Health();
                var characterGameObject = character.AsGameObject();
                var statusPresenter = characterGameObject.GetComponent<CharacterStatusPresenter>();
                statusPresenter.ShowCharacterValues(
                    isLocalPlayer: character.IsLocalPlayer,
                    maxHealth: characterHealth.MaxHealth,
                    health: characterHealth.Health
                );
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                CharacterEvents.OnCharacterCreated
            );
        }
    }

}
