using System.Collections.Generic;
using Entitas;
using Entitas.Unity;

namespace Taiga.Core.Unity
{
    public class RemoveLink_WhenUnlinked : ReactiveSystem<GameEntity>
    {
        public GameContext game;

        public RemoveLink_WhenUnlinked(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.entity_GameObject
                    .value
                    .Unlink();

                entity.RemoveEntity_GameObject();
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Entity_GameObjectUnlink);
        }
    }

}
