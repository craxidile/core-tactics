using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character.Placement;
using Taiga.Core.CharacterTurn;

namespace Taiga.Core.Unity.Camera
{
    internal class FocusOnCharacter_WhenCharacterTurnCreated : ReactiveSystem<GameEntity>
    {
        public GameContext game;

        public FocusOnCharacter_WhenCharacterTurnCreated(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var characterId = entity
                    .AsCharacterTurn(game)
                    .CharacterId;

                game.cameraEntity
                    .AsGameObject()
                    .GetComponent<GameCamera>()
                    .FocusOnCharacter(characterId);

            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(CharacterTurnEvents.OnCharacterTurn);
        }
    }

}
