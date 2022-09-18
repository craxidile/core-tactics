using System.Collections.Generic;
using System.Linq;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Placement;
using Taiga.Core.CharacterSequence;
using Taiga.Core.Unity.Audio.Providers;
using Taiga.Core.Unity.Camera;
using Taiga.Core.Unity.CharacterAnimation;
using Taiga.Core.Unity.Demo.Providers;
using Taiga.Core.Unity.Preset;
using Taiga.Core.Unity.UltimateAttack.Providers;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterSequenceAnimation
{
    internal class AdjustDamagedFacing_WhenAnimationStarted : ReactiveSystem<GameEntity>
    {
        public GameContext game;

        public AdjustDamagedFacing_WhenAnimationStarted(Contexts contexts) : base(contexts.game)
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

                var damagedSequence = entity
                    .AsCharacterSequence_Damaged(game);

                var character = characterContext
                    .GetCharacter(sequence.CharacterId)
                    .Value;

                var characterAnimator = character
                    .AsGameObject()
                    .GetComponent<CharacterAnimator>();
                characterAnimator.AudioPreset = game.GetProvider<IGameAudioPreset>();
                characterAnimator.UltimateAttackPreset = game.GetProvider<IGameUltimateAttackPreset>();
                characterAnimator.Facing = damagedSequence.BumpDirection.GetOppsite();
            }
        }

        protected override bool Filter(GameEntity entity) => entity
            .AsCharacterSequence(game)
            .IsDamaged();

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.CharacterSequence_Animating);
        }
    }
}