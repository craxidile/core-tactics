using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Taiga.Core.Unity.MapInput
{

    public static class MapInputEvents
    {
        public static IMatcher<GameEntity> OnPointer => GameMatcher.Map_PointerPosition;

        public static IMatcher<GameEntity> OnCellHover => GameMatcher.MapCell_PointerHover;

        public static IMatcher<GameEntity> OnCellTrigger => GameMatcher.MapCell_PointerTrigger;
    }

    [Unique]
    public sealed class MapCell_PointerHover : IComponent
    {
    }

    [Cleanup(CleanupMode.RemoveComponent)]
    public sealed class MapCell_PointerTrigger : IComponent
    {
    }

    [Unique]
    public sealed class Map_PointerPosition : IComponent
    {
        public Vector2 position;
    }

    [Cleanup(CleanupMode.RemoveComponent)]
    public sealed class Map_PointerTrigger : IComponent
    {
    }
}
