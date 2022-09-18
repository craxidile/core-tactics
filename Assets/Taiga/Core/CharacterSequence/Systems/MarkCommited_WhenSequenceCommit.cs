using System.Collections.Generic;
using Entitas;

namespace Taiga.Core.CharacterSequence
{
    internal class MarkCommited_WhenSequenceCommit : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public MarkCommited_WhenSequenceCommit(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.isCharacterSequence_Commited = true;
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.CharacterSequence_Commit);
        }

    }
}
