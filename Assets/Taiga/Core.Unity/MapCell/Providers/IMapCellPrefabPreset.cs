using UnityEngine;

namespace Taiga.Core.Unity.MapCell
{
    public interface IMapCellPrefabPreset : IProvider
    {
        GameObject MapCellPrefab { get; }
    }
}
