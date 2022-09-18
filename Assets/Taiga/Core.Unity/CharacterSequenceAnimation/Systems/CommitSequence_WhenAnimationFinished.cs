using System.Collections.Generic;
using Entitas;
using Taiga.Core.CharacterSequence;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterSequenceAnimation
{
    internal partial class CommitSequence_WhenAnimationFinished : ReactiveSystem<GameEntity>
    {
        public GameContext game;

        public CommitSequence_WhenAnimationFinished(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.AsCharacterSequence(game).Commit();
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.CharacterSequence_Animating.Removed()
            );
        }
    }
}
