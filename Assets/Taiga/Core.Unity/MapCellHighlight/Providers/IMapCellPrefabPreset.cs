using UnityEngine;

namespace Taiga.Core.Unity.MapCellHighlight
{
    public enum HighlightMode
    {
        PreAttack,
        Attack,
        Walk,
        Blockage,
    }

    public interface IMapCellHighlightPreset : IProvider
    {
        Color GetColor(HighlightMode mode, bool highlight);
    }
}
