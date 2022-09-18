using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.CharacterSequence;
using Taiga.Core.Unity.CharacterAnimation;

namespace Taiga.Core.Unity.CharacterSequenceAnimation
{
    internal class AnimateBlocked_WhenAnimationStarted : ReactiveSystem<GameEntity>
    {
        public GameContext game;

        public AnimateBlocked_WhenAnimationStarted(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var characterContext = game.AsCharacterContext();
            var sequenceContext = game.AsCharacterSequenceContext();
            foreach (var entity in entities)
            {
                var sequence = entity
                    .AsCharacterSequence(game);

                var blockedSequence = entity
                    .AsCharacterSequence_Blocked(game);

                var character = characterContext
                    .GetCharacter(sequence.CharacterId)
                    .Value;

                var characterAnimator = character
                    .AsGameObject()
                    .GetComponent<CharacterAnimator>();

                var attackType = blockedSequence
                    .SourceSequence
                    .AsCharacterSequence_Attack()
                    .AttackType;

                characterAnimator.Block(
                    attackType,
                    blockedSequence.BumpDirection.GetOppsite()
                );

                characterAnimator.SetFinishCallbackOnce(
                    () => sequence.entity.isCharacterSequence_Animating = false
                );
            }
        }

        protected override bool Filter(GameEntity entity) => entity
            .AsCharacterSequence(game)
            .IsBlocked();

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.CharacterSequence_Animating);
        }
    }

}
