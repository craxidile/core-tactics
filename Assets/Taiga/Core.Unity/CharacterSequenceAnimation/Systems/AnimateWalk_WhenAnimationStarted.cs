using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Placement;
using Taiga.Core.CharacterSequence;
using Taiga.Core.Unity.CharacterAnimation;

namespace Taiga.Core.Unity.CharacterSequenceAnimation
{
    internal class AnimateWalk_WhenAnimationStarted : ReactiveSystem<GameEntity>
    {
        public GameContext game;

        public AnimateWalk_WhenAnimationStarted(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var characterContext = game.AsCharacterContext();
            foreach (var entity in entities)
            {
                var sequence = entity
                    .AsCharacterSequence(game);

                var moveSequence = sequence
                    .AsCharacterSequence_Move();

                var movements = moveSequence.MovementSteps;

                var character = characterContext
                    .GetCharacter(sequence.CharacterId)
                    .Value;

                var characterAnimator = character
                    .AsGameObject()
                    .GetComponent<CharacterAnimator>();

                var lastCharacterPosition = character
                    .AsCharacter_Placement()
                    .Position;

                characterAnimator.Walk(movements);

                characterAnimator.SetFinishCallbackOnce(
                    () => sequence.entity.isCharacterSequence_Animating = false
                );
            }
        }

        protected override bool Filter(GameEntity entity) => entity.AsCharacterSequence(game).IsMove();

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.CharacterSequence_Animating);
        }
    }
}
