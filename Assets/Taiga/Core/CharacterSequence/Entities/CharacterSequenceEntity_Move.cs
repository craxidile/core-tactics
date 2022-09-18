using System.Collections.Generic;

namespace Taiga.Core.CharacterSequence
{

    public static class CharacterSequenceEntity_MoveExtensions
    {
        public static CharacterSequenceEntity_Move AsCharacterSequence_Move(this IGameScopedEntity entity)
        {
            return new CharacterSequenceEntity_Move(entity.context, entity.entity);
        }

        public static CharacterSequenceEntity_Move AsCharacterSequence_Move(this GameEntity entity, GameContext context)
        {
            return new CharacterSequenceEntity_Move(context, entity);
        }

        public static bool IsMove(this CharacterSequenceEntity characterSequence)
        {
            return characterSequence.entity.hasCharacterSequence_Move;
        }
    }

    public struct CharacterSequenceEntity_Move : IGameScopedEntity
    {
        public CharacterSequenceEntity_Move(GameContext context, GameEntity entity)
        {
            this.context = context;
            this.entity = entity;
        }

        public GameContext context { get; private set; }

        public GameEntity entity { get; private set; }

        public IEnumerable<Movement> MovementSteps => entity.characterSequence_Move.movementSteps;

    }
}
