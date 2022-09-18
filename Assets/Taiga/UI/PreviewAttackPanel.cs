using System;
using System.Collections;
using System.Collections.Generic;
using Taiga.Core;
using Taiga.Core.CharacterAction;
using Taiga.Core.CharacterFactory;
using Taiga.Core.Unity.Audio;
using Taiga.Core.Unity.CharacterBanner;
using Taiga.Core.Unity.MapInput;
using Taiga.Core.Unity.Preset;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PreviewAttackPanel : MonoBehaviour
{
    internal static PreviewAttackPanel Instance { get; private set; }

    private const float LOW_HIT_PERCENT_CHANGE = 0.3f;
    private const float LERP_DURATION = 0.5f;
    private const float NORMAL_HIT_PERCENT_CHANGE = LERP_DURATION;

    [Header("Character info")]
    [SerializeField] private CharacterInfo attackerInfo;
    [SerializeField] private CharacterInfo defenderInfo;

    [Header("Hit Info")]
    [SerializeField] private TextMeshProUGUI hitChanceText;

    [Header("Button")]
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    [Header("Config")]
    [SerializeField] private Sprite localPlayerSprite;
    [SerializeField] private Sprite opponentSprite;
    [SerializeField] private Color lowHitPercentChanceColor;
    [SerializeField] private Color normalHitPercentChanceColor;
    [SerializeField] private Color highHitPercentChanceColor;

    internal Sprite LocalPlayerSprite => localPlayerSprite;
    internal Sprite OpponentSprite => opponentSprite;

    private CanvasGroup canvasGroup;
    private CharacterActionEntity currentAction;

    private void Awake()
    {
        Instance = this;

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.SetAlpha(0);
    }

    private void Start()
    {
        confirmButton.onClick.AddListener(OnConfirmButtonClick);
        cancelButton.onClick.AddListener(OnCancelButtonClick);
    }

    private void OnConfirmButtonClick()
    {
        currentAction.Execute();

        HidePanel();
        SoundEffectManager.Instance.PlaySoundEffect(SoundEffect.Click, true);
    }

    private void OnCancelButtonClick()
    {
        // EDIT: Pond
        MapGroundInput.enabled = true;
        currentAction.Cancel();

        HidePanel();
        SoundEffectManager.Instance.PlaySoundEffect(SoundEffect.Click, true);
    }

    private void HidePanel()
    {
        // EDIT: Pond
        MapGroundInput.enabled = true;
        StopAllCoroutines();
        StartCoroutine(canvasGroup.LerpAlpha(0, LERP_DURATION));
    }

    private void SetHitChance(float hitChancePercentage)
    {
        if (hitChancePercentage <= LOW_HIT_PERCENT_CHANGE)
        {
            hitChanceText.color = lowHitPercentChanceColor;
        }
        else if (hitChancePercentage <= NORMAL_HIT_PERCENT_CHANGE)
        {
            hitChanceText.color = normalHitPercentChanceColor;
        }
        else
        {
            hitChanceText.color = highHitPercentChanceColor;
        }

        hitChanceText.text = string.Format("{0:0}%", hitChancePercentage * 100);
    }

    internal void Show(CharacterActionEntity action, CharacterInfoData attackerInfoData, CharacterInfoData defenderInfoData, float hitChancePercentage)
    {
        // EDIT: Pond
        MapGroundInput.enabled = false;
        currentAction = action;
        attackerInfo.SetupInfo(attackerInfoData);
        defenderInfo.SetupInfo(defenderInfoData);
        SetHitChance(hitChancePercentage);

        StopAllCoroutines();
        StartCoroutine(canvasGroup.LerpAlpha(1, LERP_DURATION));
    }
}