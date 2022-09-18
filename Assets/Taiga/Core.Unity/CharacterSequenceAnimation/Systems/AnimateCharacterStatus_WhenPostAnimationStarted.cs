using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Health;
using Taiga.Core.CharacterSequence;
using Taiga.Core.CharacterTurn;
using Taiga.Core.Unity.CharacterAnimation;
using Taiga.Core.Unity.Character;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterSequenceAnimation
{
    internal class AnimateCharacterStatus_WhenPostAnimationStarted : ReactiveSystem<GameEntity>
    {
        public GameContext game;

        public AnimateCharacterStatus_WhenPostAnimationStarted(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var characterContext = game
                .AsCharacterContext();

            foreach (var entity in entities)
            {
                var sequence = entity
                    .AsCharacterSequence(game);

                var character = characterContext
                    .GetCharacter(sequence.CharacterId)
                    .Value;

                var statusPresenter = character
                    .AsGameObject()
                    .GetComponent<CharacterStatusPresenter>();

                var characterHealth = character
                    .AsCharacter_Health();

                var characterAnimator = character
                    .AsGameObject()
                    .GetComponent<CharacterAnimator>();

                statusPresenter.Animate(
                    newHealth: characterHealth.Health
                );

                statusPresenter.SetAnimateOnFinishedOnce(() =>
                {
                    if (characterHealth.IsAlive)
                    {
                        sequence.entity.isCharacterSequence_PostAnimating = false;
                    }
                    else
                    {
                        characterAnimator.Dead();
                        characterAnimator.SetFinishCallbackOnce(
                            () => sequence.entity.isCharacterSequence_PostAnimating = false
                        );
                    }
                });
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            var sequence = entity.AsCharacterSequence(game);
            var turn = game
                .AsCharacterTurnContext()
                .CurrentCharacterTurn
                .Value;
            return sequence.CharacterId != turn.CharacterId;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.CharacterSequence_PostAnimating
            );
        }
    }
}
