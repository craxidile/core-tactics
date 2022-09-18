using System.Collections.Generic;
using Entitas;

namespace Taiga.Core.CharacterAction
{
    internal class RemoveAction_WhenActionFinished : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public RemoveAction_WhenActionFinished(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.isCharacterAction_Remove = true;
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.CharacterAction_Finish);
        }

    }


}
