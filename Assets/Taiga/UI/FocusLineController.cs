using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Taiga.Core;
using UnityEngine;

public class FocusLineController : MonoBehaviour
{
    internal static FocusLineController Instance { get; private set; }

    private const string _LIGHT = "focus_light";
    private const string _DARK = "focus_dark";

    [SerializeField] private Animator animator;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        Instance = this;
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.SetAlpha(0);
    }

    internal void ShowFocusLine(bool isLightFocus, float duration)
    {
        animator.Play(isLightFocus ? _LIGHT : _DARK, -1, 0);
        _canvasGroup.SetAlpha(1);
        DOTween
            .Sequence()
            .AppendInterval(duration)
            .OnComplete(() => _canvasGroup.SetAlpha(0));
    }
}
