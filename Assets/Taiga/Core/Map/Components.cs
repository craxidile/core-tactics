using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Taiga.Core.Map
{

    public sealed class MapCell : IComponent
    {
        [PrimaryEntityIndex] public Vector2Int position;
    }

}
