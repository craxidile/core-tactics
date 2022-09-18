using System.Collections.Generic;
using System.Linq;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Placement;
using UnityEngine;

namespace Taiga.Core.CharacterSequence
{
    internal class UpdateCharacter_WhenSequenceMoveCommitted : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public UpdateCharacter_WhenSequenceMoveCommitted(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var characterContext = game.AsCharacterContext();
            foreach (var entity in entities)
            {
                var sequence = entity.AsCharacterSequence(game);
                var moveSequence = entity.AsCharacterSequence_Move(game);
                var character = characterContext.GetCharacter(sequence.CharacterId).Value;
                var characterPlacement = character.AsCharacter_Placement();
                var movementSteps = moveSequence.MovementSteps;
                var lastMovement = movementSteps.Last();
                characterPlacement.SetPlacement(
                    position: lastMovement.toPosition,
                    facing: lastMovement.direction
                );
            }
        }

        protected override bool Filter(GameEntity entity) => entity.hasCharacterSequence_Move;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.CharacterSequence_Commit);
        }

    }
}
