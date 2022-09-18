using System;
using System.Collections.Generic;
using System.Linq;
using Taiga.Core.Character.Placement;
using Taiga.Core.Map;
using UnityEngine;
using UnityEngine.Assertions;

namespace Taiga.Core.CharacterAction_Move.Move
{
    public static class CharacterActionMoveContextExtensions
    {
        public static CharacterActionMoveContext AsCharacterActionMoveContext(this GameContext game)
        {
            return new CharacterActionMoveContext(game);
        }
    }

    public struct CharacterActionMoveContext
    {

        GameContext game;

        public CharacterActionMoveContext(GameContext game)
        {
            this.game = game;
        }
    }

}
