using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Taiga.Core.Unity.Character
{
    public class CharacterStatusPresenter : MonoBehaviour
    {
        public CanvasGroup pointerRoot;
        public CanvasGroup statusRoot;

        public Image healthProgress;

        public Sprite localPlayerSprite;
        public Sprite opponentSprite;

        [Header("COMBO")] [SerializeField] private TextMeshProUGUI damageText;
        [SerializeField] private TextMeshProUGUI comboText;
        [SerializeField] private CanvasGroup comboCanvasGroup;
        [SerializeField] private CanvasGroup damageCanvasGroup;
        [SerializeField] private Transform comboPosW;
        [SerializeField] private Transform comboPosE;
        [SerializeField] private Transform comboPosN;
        [SerializeField] private Transform comboPosS;

        bool isVisible;
        bool currentTurn;
        bool isLocalPlayer;
        float maxHealth;
        float health;

        private void Awake()
        {
            ShowAsCurrentTurn(false);

            comboCanvasGroup.SetAlpha(0);
            ;
            damageCanvasGroup.SetAlpha(0);
        }

        Action onAnimateFinished;

        public void SetAnimateOnFinishedOnce(Action callback)
        {
            this.onAnimateFinished = callback;
        }

        public Sequence _mainSequence;

        public Sequence MainSequence
        {
            get
            {
                Assert.IsFalse(
                    _mainSequence != null && _mainSequence.IsActive(),
                    "Cannot set any value when animating"
                );
                if (_mainSequence == null || !_mainSequence.IsActive())
                {
                    _mainSequence = DOTween.Sequence();
                    _mainSequence.OnComplete(() =>
                    {
                        if (onAnimateFinished != null)
                        {
                            onAnimateFinished();
                            onAnimateFinished = null;
                        }
                    });
                }

                return _mainSequence;
            }
        }

        public void ShowAsCurrentTurn(bool active = true)
        {
            currentTurn = active;
            ForceUpdate();
        }

        public void SetVisible(bool value)
        {
            isVisible = value;
            ForceUpdate();
        }

        public void ShowCharacterValues(
            bool isLocalPlayer,
            int maxHealth,
            int health
        )
        {
            this.maxHealth = maxHealth;
            this.health = health;
            this.isLocalPlayer = isLocalPlayer;
            ForceUpdate();
        }

        internal void SetDamageText(int damage)
        {
            damageText.text = damage.ToString();
            ShowCanvas(damageCanvasGroup);
        }

        internal void SetComboText(int combo, bool isLeft = true)
        {
            var transform = comboCanvasGroup.GetComponent<RectTransform>();
            if (isLeft)
            {
                transform.localPosition = new Vector3(-0.4f, 0.655f, 0);
            }
            else
            {
                transform.localPosition = new Vector3(0.4f, 0.655f, 0);
            }
            comboText.text = combo.ToString();
            ShowCanvas(comboCanvasGroup);
        }

        internal void SetComboText(int combo, MapDirection attackerFacing)
        {
            comboText.text = combo.ToString();

            // switch (attackerFacing)
            // {
            //     case MapDirection.North:
            //         comboCanvasGroup.transform.localPosition = comboPosN.localPosition;
            //         break;
            //     case MapDirection.South:
            //         comboCanvasGroup.transform.localPosition = comboPosS.localPosition;
            //         break;
            //     case MapDirection.West:
            //         comboCanvasGroup.transform.localPosition = comboPosW.localPosition;
            //         break;
            //     case MapDirection.East:
            //         comboCanvasGroup.transform.localPosition = comboPosE.localPosition;
            //         break;
            //     default:
            //         break;
            // }

            ShowCanvas(comboCanvasGroup);
        }

        private void ShowCanvas(CanvasGroup canvas)
        {
            StartCoroutine(canvas.LerpAlpha(1, 0.25f, onComplete: () =>
            {
                DOTween
                    .Sequence()
                    // .AppendInterval(1)
                    .AppendInterval(1.5f)
                    .OnComplete(() => canvas.DOFade(0, 0.3f));
            }));
        }

        void ForceUpdate()
        {
            var progressValue = health / maxHealth;
            healthProgress.fillAmount = progressValue;
            healthProgress.sprite = isLocalPlayer ? localPlayerSprite : opponentSprite;
            pointerRoot.alpha = currentTurn ? 1 : 0;
            statusRoot.alpha = 1; // (currentTurn || !isVisible) ? 0 : 1;
        }

        public void Animate(int newHealth)
        {
            var duration = Mathf.Abs(health - newHealth) / 100;
            var tween = DOVirtual.Float(
                    from: health,
                    to: newHealth,
                    duration: duration,
                    newValue =>
                    {
                        this.health = newValue;
                        ForceUpdate();
                    }
                )
                .SetEase(Ease.Linear);
            MainSequence.Append(tween);
        }
    }
}