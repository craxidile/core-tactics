using Entitas;

namespace Taiga.Core.Map
{
    public static class MapEvents
    {
        public static IMatcher<GameEntity> OnCellCreated => GameMatcher.MapCell;
    }
}
