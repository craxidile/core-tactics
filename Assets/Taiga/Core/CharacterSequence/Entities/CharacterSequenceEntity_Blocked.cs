using Taiga.Core.Character.Placement;
using UnityEngine;

namespace Taiga.Core.CharacterSequence
{

    public static class CharacterSequenceEntity_BlockedExtensions
    {
        public static CharacterSequenceEntity_Blocked AsCharacterSequence_Blocked(this IGameScopedEntity entity)
        {
            return new CharacterSequenceEntity_Blocked(entity.context, entity.entity);
        }

        public static CharacterSequenceEntity_Blocked AsCharacterSequence_Blocked(this GameEntity entity, GameContext context)
        {
            return new CharacterSequenceEntity_Blocked(context, entity);
        }

        public static bool IsBlocked(this CharacterSequenceEntity characterSequence)
        {
            return characterSequence.entity.hasCharacterSequence_Block;
        }
    }

    public struct CharacterSequenceEntity_Blocked : IGameScopedEntity
    {
        public CharacterSequenceEntity_Blocked(GameContext context, GameEntity entity)
        {
            this.context = context;
            this.entity = entity;
        }

        public GameContext context { get; private set; }

        public GameEntity entity { get; private set; }

        public MapDirection BumpDirection => entity.characterSequence_Block.bumpDirection;

        public CharacterSequenceEntity SourceSequence => entity
            .characterSequence_SourceSequenceEntity
            .value
            .AsCharacterSequence(context);
    }
}
