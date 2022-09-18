using Entitas;

namespace Taiga.Core.CharacterTurn
{
    public static class CharacterTurnEvents
    {

        public static IMatcher<GameEntity> OnGameCharacterTurnOrder => GameMatcher.Game_CharacterTurnOrder;

        public static IMatcher<GameEntity> OnCharacterTurn => GameMatcher.CharacterTurn;

        public static IMatcher<GameEntity> OnCharacterTurnFinish => GameMatcher.CharacterTurn_Finished;

        public static IMatcher<GameEntity> OnCharacterTurnPossibleActions => GameMatcher.CharacterTurn_PossibleActions;

    }

}
