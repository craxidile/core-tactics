using Entitas;

namespace Taiga.Core.CharacterAction.Move
{
    public static class CharacterActionMoveEvents
    {
        public static IMatcher<GameEntity> OnPossibilities => GameMatcher.CharacterAction_MovePossibilities;

        public static IMatcher<GameEntity> OnCharacterBlockages => GameMatcher.CharacterAction_MoveCharacterBlockages;

        public static IMatcher<GameEntity> OnFocus => GameMatcher.CharacterAction_MoveFocus;
    }
}
