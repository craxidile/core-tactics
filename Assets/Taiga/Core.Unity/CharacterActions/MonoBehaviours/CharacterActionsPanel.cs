using System.Collections.Generic;
using Sirenix.Serialization;
using Sirenix.OdinInspector;
using Taiga.Core.CharacterAction;
using UnityEngine;
using UnityEngine.UI;
using Taiga.Core;
using Taiga.Core.Unity.Audio;

public class CharacterActionsPanel : SerializedMonoBehaviour
{
    public delegate void CharacterActionDelegate(CharacterActionType action);

    internal static CharacterActionsPanel Instance { get; private set; }

    [OdinSerialize] private Dictionary<CharacterActionType, Button> buttonByAction;
    [SerializeField] private float fadeDuration = 0.5f;

    private CanvasGroup canvasGroup;

    CharacterActionDelegate callback;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        foreach (var pair in buttonByAction)
        {
            var action = pair.Key;
            var button = pair.Value;
            button.onClick.AddListener(() =>
            {
                callback?.Invoke(action);
                SoundEffectManager.Instance.PlaySoundEffect(SoundEffect.Click, true);
            });
        }

        StartCoroutine(canvasGroup.LerpAlpha(0, fadeDuration));
    }

    public void AskForActions(CharacterActionType[] actions, CharacterActionDelegate callback)
    {
        StopAllCoroutines();
        StartCoroutine(canvasGroup.LerpAlpha(1, fadeDuration));
        this.callback = callback;

        foreach (var button in buttonByAction.Values)
        {
            button.gameObject.SetActive(false);
        }

        foreach (var action in actions)
        {
            buttonByAction[action].gameObject.SetActive(true);
        }
    }

    public void ClearAsking()
    {
        StopAllCoroutines();
        StartCoroutine(canvasGroup.LerpAlpha(0, fadeDuration));
        callback = null;

        foreach (var button in buttonByAction.Values)
        {
            button.gameObject.SetActive(false);
        }
    }
}
