using System.Collections.Generic;
using Entitas;

namespace Taiga.Core.CharacterSequence
{

    internal class DestroySequence_WhenSequenceFinished : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public DestroySequence_WhenSequenceFinished(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.isCharacterSequence_Destroy = true;
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.CharacterSequence_Finish);
        }

    }
}
