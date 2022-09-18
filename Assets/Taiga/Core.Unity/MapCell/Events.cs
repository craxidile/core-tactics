using Entitas;

namespace Taiga.Core.Unity.MapCell
{
    public static class MapCellEvents
    {
        public static IMatcher<GameEntity> OnTrigger => GameMatcher.CharacterAction;
    }
}
