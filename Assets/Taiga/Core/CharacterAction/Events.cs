using Entitas;

namespace Taiga.Core.CharacterAction
{
    public static class CharacterActionEvents
    {
        public static IMatcher<GameEntity> OnActionCreated => GameMatcher.CharacterAction;

        public static IMatcher<GameEntity> OnActionCanceled => GameMatcher.CharacterAction_Cancel;

        public static IMatcher<GameEntity> OnActionExecute => GameMatcher.CharacterAction_Execute;

        public static IMatcher<GameEntity> OnActionFinished => GameMatcher.CharacterAction_Finish;

    }
}
