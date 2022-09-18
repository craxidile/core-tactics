using Entitas;

namespace Taiga.Core.Unity.Camera
{
    internal class CreateCamera_WhenStarted : IInitializeSystem
    {
        public GameContext game;

        public CreateCamera_WhenStarted(Contexts contexts)
        {
            this.game = contexts.game;
        }

        public void Initialize()
        {
            var gameCamera = GameCamera.Instance;
            var entity = game.CreateEntity();
            entity.isCamera = true;
            entity.AsEntity_GameObject(game)
                .Link(gameCamera.gameObject);
            gameCamera.SetAngle(315);
        }

    }

}
