using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Entitas;
using Taiga.Core.CharacterSequence;

namespace Taiga.Core.Unity.CharacterSequenceAnimation
{

    internal class WaitAndFinishSequence_WhenPostAnimationFinished : ReactiveSystem<GameEntity>
    {
        public GameContext game;

        public WaitAndFinishSequence_WhenPostAnimationFinished(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var sequences = entities
                .Select(e => e.AsCharacterSequence(game))
                .ToArray();

            DOVirtual.DelayedCall(0.5f, () =>
            {
                foreach (var sequence in sequences)
                {
                    sequence.Finish();
                }
            });
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.CharacterSequence_PostAnimating.Removed()
            );
        }
    }
}
