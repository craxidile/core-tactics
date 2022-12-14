using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.CharacterSequence;
using Taiga.Core.Unity.Audio.Providers;
using Taiga.Core.Unity.Camera;
using Taiga.Core.Unity.CharacterAnimation;
using Taiga.Core.Unity.Demo.Providers;
using Taiga.Core.Unity.Preset;
using Taiga.Core.Unity.UltimateAttack.Providers;
using Taiga.Utils;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterSequenceAnimation
{

    internal class AdjustAttackFacing_WhenAnimationStarted : ReactiveSystem<GameEntity>
    {
        public GameContext game;

        public AdjustAttackFacing_WhenAnimationStarted(Contexts contexts) : base(contexts.game)
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

                var attackSequence = entity
                    .AsCharacterSequence_Attack(game);

                var character = characterContext
                    .GetCharacter(sequence.CharacterId)
                    .Value;

                var characterAnimator = character
                    .AsGameObject()
                    .GetComponent<CharacterAnimator>();
                
                characterAnimator.AudioPreset = game.GetProvider<IGameAudioPreset>();
                characterAnimator.UltimateAttackPreset = game.GetProvider<IGameUltimateAttackPreset>();
                characterAnimator.Facing = attackSequence.Direction;

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
