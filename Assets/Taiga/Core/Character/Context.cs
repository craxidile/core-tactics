using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Taiga.Core.Character
{
    public static class CharacterContextExtensions
    {
        public static CharacterContext AsCharacterContext(this GameContext game)
        {
            return new CharacterContext(game);
        }
    }

    public struct CharacterContext
    {
        GameContext game;

        public CharacterContext(GameContext game)
        {
            this.game = game;
        }

        public CharacterEntity CreateCharacter(
            string architypeId,
            int ownerPlayerId,
            int level
        )
        {
            var entity = game.CreateEntity();
            entity.AddCharacter(
                newArchitypeId: architypeId,
                newId: GenerateNextId(),
                newLevel: level,
                newOwnerPlayerId: ownerPlayerId
            );
            return entity.AsCharacter(game);
        }

        public CharacterEntity? GetCharacter(int id)
        {
            var entity = game.GetEntityWithCharacterId(id);
            return entity?.AsCharacter(game);
        }

        public IEnumerable<CharacterEntity> AllCharacters
        {
            get
            {
                var game = this.game;
                return game
                    .GetGroup(GameMatcher.Character)
                    .GetEntities()
                    .Select(entity => entity.AsCharacter(game));
            }
        }

        int GenerateNextId()
        {
            int id = 0;
            if (!game.hasGame_NextCharacterId)
            {
                var entity = game.CreateEntity();
                entity.AddGame_NextCharacterId(id);
            }
            else
            {
                id = game.game_NextCharacterId.value;
            }
            game.ReplaceGame_NextCharacterId(id + 1);
            return id;
        }

        public IEnumerable<CharacterEntity> GetCharactersByPlayerId(int playerId)
        {
            var game = this.game;
            return game
                .GetEntitiesWithCharacterOwnerPlayerId(playerId)
                .Select(e => e.AsCharacter(game));
        }
    }

}
