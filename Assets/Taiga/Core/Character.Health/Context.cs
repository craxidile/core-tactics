using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Taiga.Core.Character.Health
{
    public static class CharacterHealthContextExtensions
    {
        public static CharacterHealthContext AsCharacterHealthContext(this GameContext game)
        {
            return new CharacterHealthContext(game);
        }
    }

    public struct CharacterHealthContext
    {
        GameContext game;

        public CharacterHealthContext(GameContext game)
        {
            this.game = game;
        }

        public IEnumerable<CharacterEntity> AliveCharacters
        {
            get
            {
                return game.AsCharacterContext()
                    .AllCharacters
                    .Where(character => character.AsCharacter_Health().IsAlive);
            }
        }
    }

}
