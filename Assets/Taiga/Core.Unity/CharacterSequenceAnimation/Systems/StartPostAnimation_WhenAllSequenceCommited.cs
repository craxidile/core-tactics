using System.Collections.Generic;
using System.Linq;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.CharacterSequence;

namespace Taiga.Core.Unity.CharacterSequenceAnimation
{

    internal class StartPostAnimation_WhenAllSequenceCommited : ReactiveSystem<GameEntity>
    {
        public GameContext game;

        public StartPostAnimation_WhenAllSequenceCommited(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var sequenceEntities = game
                .GetGroup(GameMatcher.CharacterSequence)
                .GetEntities();

            foreach (var entity in sequenceEntities)
            {
                entity.isCharacterSequence_PostAnimating = true;
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            return game
                .GetGroup(GameMatcher.CharacterSequence)
                .GetEntities()
                .Select(e => e.AsCharacterSequence(game))
                .All(sequence => sequence.IsCommited);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(CharacterSequenceEvents.OnSequenceCommited);
        }
    }

}
