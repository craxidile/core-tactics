using DG.Tweening;
using Taiga.Core.Unity.Effect.Providers;
using Taiga.Core.Unity.Preset;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation
{
    public class HitTextEffectController : MonoBehaviour
    {
        private const string Layer = "Effect";

        internal void ShowTextEffect(HitTextEffectType type, float duration,
            Vector3 position = default(Vector3),
            Vector3 rotation = default(Vector3),
            Vector3 scale = default(Vector3))
        {
            var textEffect = TextEffectProvider.Instance.GetTextEffectByType(type);
            var spriteRenderer = new GameObject(type.ToString()).AddComponent<SpriteRenderer>();
            spriteRenderer.transform.parent = this.transform;
            spriteRenderer.transform.localPosition = position != default(Vector3) ? position : Vector3.zero;
            spriteRenderer.transform.localRotation = rotation != default(Vector3) ? Quaternion.Euler(rotation) : Quaternion.identity;
            spriteRenderer.transform.localScale = scale != default(Vector3) ? scale : Vector3.one;

            spriteRenderer.sprite = textEffect;
            spriteRenderer.sortingLayerName = Layer;

            spriteRenderer.gameObject.SetActive(true);

            DOTween.Sequence()
                .AppendInterval(duration)
                .AppendCallback(() =>
                {
                    spriteRenderer.gameObject.SetActive(false);
                }).OnComplete(() =>
                {
                    Destroy(spriteRenderer.gameObject);
                });
        }
    }
}
