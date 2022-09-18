using System;
using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Taiga.Core.Unity.MapCellHighlight
{

    [Unique]
    public sealed class Game_MapCellHighlightDisable : IComponent
    {
    }

    [Unique]
    public sealed class Game_MapCellHighlightPreAttackPositions : IComponent
    {
        public IEnumerable<Vector2Int> positions;
    }

    public sealed class MapCell_Highlight : IComponent
    {
        public Color color;
    }

    public sealed class MapCell_OverrideHighlight : IComponent
    {
        public Color color;
        public bool focused;
    }

    [Cleanup(CleanupMode.RemoveComponent)]
    public sealed class MapCell_HighlightOff : IComponent
    {
    }

}
