using System.Collections.Generic;
using System.Linq;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Health;
using Taiga.Core.CharacterSequence;
using Taiga.Core.Unity.Camera;

namespace Taiga.Core.Unity.CharacterSequenceAnimation
{

    internal class StartConsequenceAnimation_WhenSequenceAttackAnimated : ReactiveSystem<GameEntity>
    {
        public GameContext game;

        public StartConsequenceAnimation_WhenSequenceAttackAnimated(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var consequences = entity
                    .AsCharacterSequence(game)
                    .Consequences;

                foreach (var consequence in consequences)
                {
                    consequence.entity.isCharacterSequence_Animating = true;
                }
            }
        }

        protected override bool Filter(GameEntity entity) => entity
            .AsCharacterSequence(game)
            .IsAttack();

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.CharacterSequence_Animating);
        }
    }

}
