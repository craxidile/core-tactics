using System.Collections.Generic;
using UnityEngine;

namespace Taiga.Core.Map
{
    public interface IMapPreset : IProvider
    {
        ICollection<Vector2Int> GetMapTerrain();
    }
}
