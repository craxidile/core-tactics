using System;
using System.Collections;
using UnityEngine;


namespace Taiga.Core
{
    public static class CanvasGroupExtension
    {

        public static IEnumerator LerpAlpha(this CanvasGroup source, float endValue, float duration, bool adjustInteractable = true, Action onComplete = null)
        {
            float time = 0;
            float startValue = source.alpha;
            float progress = 0f;

            while (time < duration)
            {
                progress = Mathf.Lerp(startValue, endValue, time / duration);
                source.SetAlpha(progress, adjustInteractable);
                time += Time.deltaTime;
                yield return null;
            }
            source.SetAlpha(endValue, adjustInteractable);
            onComplete?.Invoke();
        }

        public static bool SetInteractive(this CanvasGroup source, bool interactAble)
        {
            return source.interactable = interactAble;
        }

        //--------------------------------------------------------------------------------------------------------------

        public static bool SetBlocksraycasts(this CanvasGroup source, bool blocksraycasts)
        {
            return source.blocksRaycasts = blocksraycasts;
        }

        //--------------------------------------------------------------------------------------------------------------

        public static CanvasGroup SetAlpha(this CanvasGroup source, float alpha, bool adjustInteractAble = true)
        {
            source.alpha = alpha;
            if (adjustInteractAble)
            {
                source.SetInteractive(alpha >= 1);
                source.SetBlocksraycasts(alpha > 0);
            }
            return source;
        }
    }
}
