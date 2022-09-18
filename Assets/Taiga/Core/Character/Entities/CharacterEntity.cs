using Taiga.Core.CharacterFactory;
using UnityEngine;

namespace Taiga.Core.Character
{

    public static class CharacterEntityExtensions
    {
        public static CharacterEntity AsCharacter(this IGameScopedEntity entity)
        {
            return new CharacterEntity(entity.context, entity.entity);
        }

        public static CharacterEntity AsCharacter(this GameEntity entity, GameContext context)
        {
            return new CharacterEntity(context, entity);
        }
    }

    public struct CharacterEntity : IGameScopedEntity
    {
        public CharacterEntity(GameContext context, GameEntity entity)
        {
            this.context = context;
            this.entity = entity;
        }

        public GameContext context { get; private set; }

        public GameEntity entity { get; private set; }

        public int Id => entity.character.id;

        public string ArchitypeId => entity.character.architypeId;

        public int OwnerPlayerId => entity.character.ownerPlayerId;

        public int Level => entity.character.level;

        public string Name
        {
            get
            {
                var architypePreset = context.GetProvider<ICharacterArchitypePreset>();
                var architype = architypePreset.GetCharacterArchitypeProperty(ArchitypeId);
                return architype.name;
            }
        }

        public CharacterGroup Group
        {
            get
            {
                var architypePreset = context.GetProvider<ICharacterArchitypePreset>();
                var architype = architypePreset.GetCharacterArchitypeProperty(ArchitypeId);
                return architype.group;
            }
        }

        public bool IsLocalPlayer => OwnerPlayerId == 0;

    }
}
