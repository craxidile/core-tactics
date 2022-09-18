using Taiga.Core;
using Taiga.Core.Unity.Audio;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    private const int On = 1;
    private const int Off = 0;
    private const float Duration = 0.2f;

    [SerializeField] private Toggle toggle;
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private CanvasGroup openCanvasGroup;
    [SerializeField] private CanvasGroup closeCanvasGroup;

    private bool inited;

    void Start()
    {
        toggle.isOn = false;
        inited = true;
    }

    void OnEnable()
    {
        toggle.onValueChanged.AddListener(delegate
        {
            StopAllCoroutines();
            StartCoroutine(canvasGroup.LerpAlpha(toggle.isOn ? On : Off, Duration));
            StartCoroutine(openCanvasGroup.LerpAlpha(toggle.isOn ? Off : On, Duration));
            StartCoroutine(closeCanvasGroup.LerpAlpha(toggle.isOn ? On : Off, Duration));

            // Prevent playing sound on first frame
            if (inited)
            {
                SoundEffectManager.Instance.PlaySoundEffect(SoundEffect.Click, true);
            }
        });
    }

    void OnDisable()
    {
        toggle.onValueChanged.RemoveAllListeners();
    }
}
