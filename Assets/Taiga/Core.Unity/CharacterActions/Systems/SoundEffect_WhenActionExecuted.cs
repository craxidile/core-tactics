using System.Collections.Generic;
using Entitas;
using Taiga.Core.Unity.Audio;

namespace Taiga.Core.Unity.CharacterActions
{
    internal class SoundEffect_WhenActionExecuted : ReactiveSystem<GameEntity>
    {
        public SoundEffect_WhenActionExecuted(Contexts contexts) : base(contexts.game)
        {

        }

        protected override void Execute(List<GameEntity> entities)
        {
            SoundEffectManager.Instance.PlaySoundEffect(SoundEffect.Click, true);
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AnyOf(GameMatcher.CharacterSequence_Move, GameMatcher.CharacterSequence_Attack));
        }
    }
}
