using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Taiga.Core.CharacterActionPrediction.Move
{
    public sealed class CharacterAction_MoveCharacterBlockages : IComponent
    {
        public ICollection<Vector2Int> positions;
    }

    public sealed class CharacterAction_MovePossibilities : IComponent
    {
        public ICollection<Vector2Int> positions;
        public object pathwayCalculationCache;
    }

    public sealed class CharacterAction_MoveFocus : IComponent
    {
        public Vector2Int position;
    }

    public sealed class CharacterAction_MovePrediction : IComponent
    {
        public IEnumerable<Vector2Int> pathway;
    }

}
