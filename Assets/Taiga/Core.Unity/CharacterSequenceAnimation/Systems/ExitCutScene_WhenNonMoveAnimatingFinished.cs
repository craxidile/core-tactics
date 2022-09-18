using System.Collections.Generic;
using System.Linq;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.CharacterSequence;

namespace Taiga.Core.Unity.CharacterSequenceAnimation
{
    internal class ExitCutScene_WhenNonMoveAnimatingFinished : ReactiveSystem<GameEntity>
    {
        public GameContext game;

        public ExitCutScene_WhenNonMoveAnimatingFinished(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var isAnimating = game.AsCharacterSequenceContext()
                .Sequences
                .Any(sequence => sequence.entity.isCharacterSequence_Animating);

            if (!isAnimating)
            {
                game.isGame_CharacterSequenceCutScene = false;
            }
        }

        protected override bool Filter(GameEntity entity) => !entity
            .AsCharacterSequence(game)
            .IsMove();

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.CharacterSequence_Animating.Removed()
            );
        }
    }
}
