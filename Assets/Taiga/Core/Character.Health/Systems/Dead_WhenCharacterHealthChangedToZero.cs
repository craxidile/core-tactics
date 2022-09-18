using System.Collections.Generic;
using Entitas;

namespace Taiga.Core.CharacterHealth
{
    internal class Dead_WhenCharacterHealthChangedToZero : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public Dead_WhenCharacterHealthChangedToZero(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.isCharacter_HealthDead = true;
            }
        }

        protected override bool Filter(GameEntity entity) => entity.character_HealthPoint.value <= 0;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Character_HealthPoint);
        }

    }
}
