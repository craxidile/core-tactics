using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Health;
using Taiga.Core.Character.Placement;
using UnityEngine;

namespace Taiga.Core.CharacterSequence
{

    internal class UpdateCharacter_WhenSequenceDamagedCommitted : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public UpdateCharacter_WhenSequenceDamagedCommitted(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var characterContext = game.AsCharacterContext();
            foreach (var entity in entities)
            {
                var sequence = entity.AsCharacterSequence(game);
                var damageSequence = entity.AsCharacterSequence_Damaged(game);

                var character = characterContext
                    .GetCharacter(sequence.CharacterId)
                    .Value;

                var characterHealth = character.AsCharacter_Health();
                var characterPosition = character.AsCharacter_Placement();

                characterHealth.Damage(damageSequence.Damage);

                var facingDirection = damageSequence
                    .BumpDirection
                    .GetOppsite();

                if (damageSequence.EndPosition != null)
                {
                    characterPosition.SetPlacement(
                        position: damageSequence.EndPosition.Value,
                        facing: facingDirection
                    );
                }
                else
                {
                    characterPosition.SetPlacement(
                        position: characterPosition.Position,
                        facing: facingDirection
                    );
                }
            }
        }

        protected override bool Filter(GameEntity entity) => entity.hasCharacterSequence_Damaged;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.CharacterSequence_Commit);
        }

    }
}
