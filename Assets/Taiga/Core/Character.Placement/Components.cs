using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Taiga.Core.Character.Placement
{

    public sealed class Character_FacingDirection : IComponent
    {
        public MapDirection value;
    }

    public sealed class Character_Position : IComponent
    {
        [EntityIndex] public Vector2Int value;
    }

}
