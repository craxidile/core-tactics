using Entitas;

namespace Taiga.Core.GameRound
{
    public static class GameRoundEvents
    {

        public static IMatcher<GameEntity> OnStart => GameMatcher.Game_Start;

    }
}
