using System.Collections.Generic;
using Entitas;
using Taiga.Core.CharacterSequence;

namespace Taiga.Core.Unity.CharacterSequenceAnimation
{
    internal class StartAnimation_WhenInitialSequenceCreated : ReactiveSystem<GameEntity>
    {
        public GameContext game;

        public StartAnimation_WhenInitialSequenceCreated(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.isCharacterSequence_Animating = true;
            }
        }

        protected override bool Filter(GameEntity entity) => entity.AsCharacterSequence(game).IsInitial;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(CharacterSequenceEvents.OnSequence);
        }
    }

}
