using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Attack;
using Taiga.Core.Character.SpecialAttack.Entities;
using Taiga.Core.CharacterSequence;
using Taiga.Core.CharacterTurn;
using Taiga.Core.Unity.CharacterBanner;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterBanner
{
    internal class AnimateBanner_WhenCurrentTurnCharacterPostAnimationStarted : ReactiveSystem<GameEntity>
    {
        public GameContext game;

        public AnimateBanner_WhenCurrentTurnCharacterPostAnimationStarted(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var banner = CharacterBannerPresenter.Instance;

                var characterId = entity
                    .AsCharacterSequence(game)
                    .CharacterId;

                var character = game
                    .AsCharacterContext()
                    .GetCharacter(characterId);

                var specialAttackPoint = character
                    .AsCharacter_SpecialAttack()
                    .Point;

                banner.Animate(specialAttackPoint);
                banner.SetAnimateOnFinishedOnce(
                    () => entity.isCharacterSequence_PostAnimating = false
                );
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            var sequence = entity.AsCharacterSequence(game);
            var turn = game
                .AsCharacterTurnContext()
                .CurrentCharacterTurn
                .Value;
            return sequence.CharacterId == turn.CharacterId;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.CharacterSequence_PostAnimating
            );
        }
    }
}
