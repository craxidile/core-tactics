using System.Diagnostics;
using UnityEngine;

namespace Taiga.Core.Unity.MapCellHighlight
{

    public class MapCellHighlightPresenter : MonoBehaviour
    {
        public SpriteRenderer pigment;
        public SpriteRenderer cursor;
        public SpriteRenderer characterPlacement;

        public Color walk;
        public Color attack;
        public Color preAttack;

        private void Awake()
        {
            ClearHighlight();
        }

        public void SetHighlight(Color color)
        {
            pigment.color = color;
            pigment.gameObject.SetActive(true);
            // TODO: fix this
            // cursor.gameObject.SetActive(false);
        }

        public void ClearPigmentColor()
        {
            pigment.color = Color.clear;
        }

        public void ClearHighlight()
        {
            pigment.gameObject.SetActive(false);
            cursor.gameObject.SetActive(false);
            characterPlacement.gameObject.SetActive(true);
        }

        internal void SetCursorHighlight(Color color)
        {
            // TODO: fix this
            // cursor.gameObject.SetActive(true);
            pigment.color = color;
        }

        internal void SetFocus()
        {
            cursor.gameObject.SetActive(true);
        }

        internal void ClearFocus()
        {
            cursor.gameObject.SetActive(false);
        }
    }
}
