using UnityEngine.Assertions;

namespace Taiga.Core.Player
{
    public static class PlayerContextExtensions
    {
        public static PlayerContext AsPlayerContext(this GameContext game)
        {
            return new PlayerContext(game);
        }
    }

    public struct PlayerContext
    {
        GameContext game;

        public PlayerContext(GameContext game)
        {
            this.game = game;
        }

        public PlayerEntity GetPlayer(int playerId)
        {
            return game
                .GetEntityWithPlayer(playerId)
                .AsPlayer(game);
        }

        public PlayerEntity LocalPlayer => GetPlayer(0);

        public PlayerEntity CreatePlayer(int givenPlayerId)
        {
            var entity = game.CreateEntity();
            entity.AddPlayer(
                newId: givenPlayerId
            );
            return entity.AsPlayer(game);
        }
    }

}
