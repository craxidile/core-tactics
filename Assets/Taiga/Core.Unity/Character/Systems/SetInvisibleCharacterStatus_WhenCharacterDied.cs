using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Health;

namespace Taiga.Core.Unity.Character
{
    internal class SetInvisibleCharacterStatus_WhenCharacterDied : ReactiveSystem<GameEntity>
    {
        public GameContext game;

        public SetInvisibleCharacterStatus_WhenCharacterDied(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var character = entity.AsCharacter(game);

                var presenter = character
                    .AsGameObject()
                    .GetComponent<CharacterStatusPresenter>();

                presenter.SetVisible(false);
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                CharacterHealthEvents.OnCharacterDead
            );
        }
    }

}
