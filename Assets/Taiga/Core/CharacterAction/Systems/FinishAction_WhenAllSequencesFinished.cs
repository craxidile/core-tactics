using System.Collections.Generic;
using System.Linq;
using Entitas;
using Taiga.Core.CharacterSequence;

namespace Taiga.Core.CharacterAction
{
    internal class FinishAction_WhenAllSequencesFinished : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public FinishAction_WhenAllSequencesFinished(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var currentAction = game
                .AsCharacterActionContext()
                .CurrentAction
                .Value;

            currentAction.entity.isCharacterAction_Finish = true;
        }

        protected override bool Filter(GameEntity entity)
        {
            return game
                .GetGroup(GameMatcher.CharacterSequence)
                .GetEntities()
                .Select(e => e.AsCharacterSequence(game))
                .All(sequence => sequence.IsFinished);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(CharacterSequenceEvents.OnSequenceFinish);
        }

    }

}
