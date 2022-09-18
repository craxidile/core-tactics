using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace Taiga.Core.GameRound
{
    public static class GameRoundContextExtensions
    {
        public static GameRoundContext AsGameRoundContext(this GameContext game)
        {
            return new GameRoundContext(game);
        }
    }

    public struct GameRoundContext
    {
        GameContext game;

        public GameRoundContext(GameContext game)
        {
            this.game = game;
        }

        public void Start()
        {
            game.isGame_Start = true;
        }
    }

}
