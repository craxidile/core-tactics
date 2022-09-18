using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Taiga.Core;
using TMPro;
using UnityEngine;

public class GameplayPanel : MonoBehaviour
{
    internal static GameplayPanel Instance { get; private set; }

    [SerializeField] private CanvasGroup comboCanvasGroup;
    [SerializeField] private CanvasGroup damageCanvasGroup;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private TextMeshProUGUI damageText;


    private RectTransform comboRectTransform;
    private RectTransform damageRectTransform;
    private void Awake()
    {
        Instance = this;
        comboCanvasGroup.SetAlpha(0);
        damageCanvasGroup.SetAlpha(0);
        comboRectTransform = comboCanvasGroup.GetComponent<RectTransform>();
        damageRectTransform = damageCanvasGroup.GetComponent<RectTransform>();
    }

    internal void SetComboText(int combo, Vector3 position)
    {
        comboText.text = combo.ToString();
        //TODO: position is still not correct
        comboRectTransform.position = Camera.main.WorldToScreenPoint(position);

        ShowCanvas(comboCanvasGroup);
    }

    internal void SetDamageText(int damage, Vector3 position)
    {
        damageText.text = damage.ToString();
        //TODO: position is still not correct
        damageRectTransform.position = Camera.main.WorldToScreenPoint(position);

        ShowCanvas(damageCanvasGroup);
    }

    private void ShowCanvas(CanvasGroup canvasGroup)
    {
        StartCoroutine(canvasGroup.LerpAlpha(1, 0.25f, onComplete: () =>
        {
            DOTween
                .Sequence()
                .AppendInterval(1)
                .OnComplete(() => canvasGroup.SetAlpha(0));
        }));

    }

}
