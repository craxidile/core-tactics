using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Taiga.Core.Character.Attack
{
    public static class CharacterAttackContextExtensions
    {
        public static CharacterAttackContext AsCharacterAttackContext(this GameContext game)
        {
            return new CharacterAttackContext(game);
        }
    }

    public struct CharacterAttackContext
    {
        GameContext game;

        public CharacterAttackContext(GameContext game)
        {
            this.game = game;
        }

        public int SpecialAttackPointPerUnit
        {
            get
            {
                return game
                    .GetProvider<ICharacterAttackPreset>()
                    .SpecialAttackPointPerUnit;
            }
        }
    }

}
