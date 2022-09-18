using System.Collections.Generic;
using System.Linq;
using Taiga.Core.CharacterAction;
using UnityEngine.Assertions;

namespace Taiga.Core.CharacterTurn
{
    public static class CharacterTurnContextExtensions
    {
        public static CharacterTurnContext AsCharacterTurnContext(this GameContext game)
        {
            return new CharacterTurnContext(game);
        }
    }

    public struct CharacterTurnContext
    {
        GameContext game;

        public CharacterTurnContext(GameContext game)
        {
            this.game = game;
        }

        public CharacterTurnEntity? CurrentCharacterTurn
        {
            get
            {
                return game.GetGroup(GameMatcher.CharacterTurn)
                    .GetEntities()
                    .FirstOrDefault(e => !e.isCharacterTurn_Finished)?
                    .AsCharacterTurn(game);
            }
        }

        public CharacterTurnEntity CreateCharacterTurn(int characterId)
        {
            Assert.IsTrue(CurrentCharacterTurn == null);
            var entity = game.CreateEntity();
            entity.AddCharacterTurn(characterId);
            return entity.AsCharacterTurn(game);
        }

        public IEnumerable<int> CharacterIdsOrder
        {
            get
            {
                return game.game_CharacterTurnOrder.characterIds;
            }
        }
    }

}
