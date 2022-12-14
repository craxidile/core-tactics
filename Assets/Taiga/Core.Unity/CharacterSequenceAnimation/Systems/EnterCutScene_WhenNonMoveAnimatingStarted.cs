using System.Collections.Generic;
using Entitas;
using Taiga.Core.CharacterSequence;

namespace Taiga.Core.Unity.CharacterSequenceAnimation
{

    internal class EnterCutScene_WhenNonMoveAnimatingStarted : ReactiveSystem<GameEntity>
    {
        public GameContext game;

        public EnterCutScene_WhenNonMoveAnimatingStarted(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            game.isGame_CharacterSequenceCutScene = true;
        }

        protected override bool Filter(GameEntity entity) => !entity
            .AsCharacterSequence(game)
            .IsMove();

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.CharacterSequence_Animating
            );
        }
    }
}
