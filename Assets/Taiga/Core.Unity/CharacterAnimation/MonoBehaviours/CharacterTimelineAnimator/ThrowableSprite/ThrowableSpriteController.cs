using System.Collections.Generic;
using DG.Tweening;
using Taiga.Core.Unity.Audio.Providers;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.Base.ThrowableSprite
{
    public class ThrowableSpriteController
    {
        private BaseCharacterTimelineController _characterTimelineController;
        private Dictionary<string, UnityEngine.Sprite> _spriteMap;
        private Vector3 _originalPosition;

        public ThrowableSpriteController(BaseCharacterTimelineController characterTimelineController)
        {
            _spriteMap = new Dictionary<string, UnityEngine.Sprite>();
            _characterTimelineController = characterTimelineController;
        }

        internal void AddThrowableSprite(string key, string spriteName)
        {
            if (key == null || spriteName == null) return;

            var throwableSpritePreset = Contexts.sharedInstance.GetProvider<IThrowableSpritePreset>();
            var sprite = throwableSpritePreset.GetThrowableSpriteSourceByName(spriteName);

            if (sprite == null) return;
            if (!_spriteMap.ContainsKey(key))
            {
                _spriteMap.Add(key, sprite);
            }
            else
            {
                _spriteMap[key] = sprite;
            }
        }

        internal void ThrowSprite(string key, float duration, Vector3 origin, Vector3 destination)
        {
            var throwableDock = _characterTimelineController.CharacterAnimator.throwableDock.gameObject;
            var throwableTransform = throwableDock.transform;
            _originalPosition = throwableTransform.position;

            var spriteRenderer = throwableDock.GetComponent<SpriteRenderer>();
            Debug.Log($">>throw<< {throwableDock} {spriteRenderer}");
            if (!spriteRenderer || !_spriteMap.ContainsKey(key)) return;
            spriteRenderer.sprite = _spriteMap[key];

            var midpoint = (destination - origin) / 2 + origin;
            midpoint.y += 0.5f;

            var path = new[] { origin, midpoint, destination };

            throwableTransform.DOPath(path, duration, PathType.CatmullRom);
            throwableTransform.DOLocalRotate(new Vector3(0, 0f, 360), duration, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Incremental)
                .SetEase(Ease.Linear);
        }

        internal void ResetThrowableSprite()
        {
            var throwableDock = _characterTimelineController.CharacterAnimator.throwableDock.gameObject;
            var throwableTransform = throwableDock.transform;
            throwableTransform.position = _originalPosition;
            throwableTransform.localRotation = Quaternion.Euler(Vector3.zero);

            var spriteRenderer = throwableDock.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = null;
        }
    }
}