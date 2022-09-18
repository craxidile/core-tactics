using System.Collections.Generic;
using Entitas;

namespace Taiga.Core.CharacterAction
{
    internal class FinishAction_WhenActionSkipCreated : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public FinishAction_WhenActionSkipCreated(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.isCharacterAction_Execute = true;
                entity.isCharacterAction_Finish = true;
            }
        }

        protected override bool Filter(GameEntity entity) => entity.characterAction.type == CharacterActionType.EndTurn;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.CharacterAction);
        }

    }

}
