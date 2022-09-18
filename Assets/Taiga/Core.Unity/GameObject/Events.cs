using Entitas;

namespace Taiga.Core.Unity.Character
{
    public static class GameObjectLinkEvents
    {
        public static IMatcher<GameEntity> OnGameObjectLinked => GameMatcher.Entity_GameObject;

        public static IMatcher<GameEntity> OnGameObjectUnlinked => GameMatcher.Entity_GameObjectUnlink;
    }
}
