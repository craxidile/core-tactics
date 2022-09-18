using System.Diagnostics;
using Taiga.Core.Unity.CharacterAnimation;
using Taiga.Core.Unity.MapCellHighlight;
using UnityEngine;

namespace Taiga.Core.Unity.Preset
{

    [CreateAssetMenu(fileName = "TaigaViewPreset", menuName = "Taiga Preset/View", order = 1)]
    public class GameViewPreset : ScriptableObject, IMapCellHighlightPreset, ICharacterAnimationPreset
    {
        [Header("Animations")] public float damageFlySpeed;
        public float DamagedFlySpeed => damageFlySpeed;

        public float walkDurationSpeed;
        public float WalkSpeed => walkDurationSpeed;

        [Header("Map Cell Highlight")] public Color walkHighlightColor;
        public Color cursorHighlightColor;
        public Color attackHighlightColor;
        public Color preAttackHighlightColor;
        public Color blockageHighlightColor;
        public float focusOpacity;

        public Color GetColor(HighlightMode mode, bool focus)
        {
            var color = Color.white;
            if (mode == HighlightMode.PreAttack)
            {
                color = preAttackHighlightColor;
            }
            else if (mode == HighlightMode.Attack)
            {
                color = attackHighlightColor;
            }
            else if (mode == HighlightMode.Walk)
            {
                color = !focus ? walkHighlightColor : cursorHighlightColor;
            }
            else if (mode == HighlightMode.Blockage)
            {
                color = blockageHighlightColor;
            }

            if (focus)
            {
                color.a = focusOpacity;
            }

            return color;
        }
    }

}
