using System.Collections.Generic;
using System.Linq;
using Taiga.Core.Character;

namespace Taiga.Core.CharacterSequence
{

    public static class CharacterSequenceEntityExtensions
    {
        public static CharacterSequenceEntity AsCharacterSequence(this IGameScopedEntity entity)
        {
            return new CharacterSequenceEntity(entity.context, entity.entity);
        }

        public static CharacterSequenceEntity AsCharacterSequence(this GameEntity entity, GameContext context)
        {
            return new CharacterSequenceEntity(context, entity);
        }
    }

    public struct CharacterSequenceEntity : IGameScopedEntity
    {
        public CharacterSequenceEntity(GameContext context, GameEntity entity)
        {
            this.context = context;
            this.entity = entity;
        }

        public GameContext context { get; private set; }

        public GameEntity entity { get; private set; }

        public IEnumerable<CharacterSequenceEntity> Consequences
        {
            get
            {
                var context = this.context;
                return context
                    .GetEntitiesWithCharacterSequence_SourceSequenceEntity(entity)
                    .Select(e => e.AsCharacterSequence(context));
            }
        }

        public int CharacterId => entity.characterSequence.characterId;

        public bool IsInitial => entity.isCharacterSequence_Initial;

        public bool IsCommited => entity.isCharacterSequence_Commited;

        public bool IsFinished => entity.isCharacterSequence_Finish;

        public CharacterEntity Character
        {
            get
            {
                return context
                    .AsCharacterContext()
                    .GetCharacter(CharacterId)
                    .Value;
            }
        }

        public void Commit()
        {
            entity.isCharacterSequence_Commit = true;
        }

        public void Finish()
        {
            entity.isCharacterSequence_Finish = true;
        }
    }
}
