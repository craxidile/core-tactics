using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;
using UnityEngine.Assertions;
using Taiga.Core.CharacterFactory;
using Taiga.Core.Unity.Preset;

namespace Taiga.Core.Unity.CharacterBanner
{
    public class CharacterBannerPresenter : MonoBehaviour
    {

        internal static CharacterBannerPresenter Instance { get; private set; }

        [Header("Banner")] [SerializeField] private Image banner;
        [SerializeField] private Image activeBg;
        [SerializeField] private Sprite localPlayerSprite;
        [SerializeField] private Sprite opponentSprite;

        [Header("HP")] [SerializeField] private Image healthProgress;
        [SerializeField] private TextMeshProUGUI healthText;

        [Header("SP")] [SerializeField] private CanvasGroup specialAttackPointRoot;
        [SerializeField] private Image[] specialAttackProgresses;

        [Header("Details")] [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI levelText;

        [Header("BloodType")] [SerializeField] GameCharacterGroupPreset bloodTypePreset;
        [SerializeField] private Image bloodTypeImage;

        [Header("Unused")] [SerializeField] private TextMeshProUGUI attackText;
        [SerializeField] private TextMeshProUGUI defendText;
        [SerializeField] private TextMeshProUGUI dexerityText;
        [SerializeField] private TextMeshProUGUI evasionText;
        [SerializeField] private TextMeshProUGUI criticalText;

        [SerializeField] private Color localPlayerColor;
        [SerializeField] private Color opponentColor;


        float specialAttackPointPerUnit;
        float specialAttackPoint;

        Action onAnimateFinished;
        public void SetAnimateOnFinishedOnce(Action callback)
        {
            this.onAnimateFinished = callback;
        }

        Sequence _mainSequence;

        Sequence MainSequence
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

        private void Awake()
        {
            Instance = this;
        }

        public void Animate(int newSpecialAttackPoint)
        {
            var duration = Mathf.Abs(newSpecialAttackPoint - specialAttackPoint) / 100;
            var tween = DOVirtual.Float(
                from: specialAttackPoint,
                to: newSpecialAttackPoint,
                duration: duration,
                newValue =>
                {
                    this.specialAttackPoint = newValue;
                    ForceUpdate();
                }
            );
            MainSequence.Append(tween);
        }

        void ForceUpdate()
        {
            Debug.Log($">>special_attack_point<< {specialAttackPoint}");
            for (var i = 0; i < specialAttackProgresses.Length; i++)
            {
                var unitValue = specialAttackPoint - i * specialAttackPointPerUnit;
                unitValue = Mathf.Min(specialAttackPointPerUnit, unitValue);
                unitValue = Mathf.Max(0, unitValue);
                Debug.Log($">>unit_value<< {unitValue}");
                specialAttackProgresses[i].fillAmount = unitValue / (specialAttackPointPerUnit * 1f);
            }
        }

        public void ShowSpecialAttack(
            int specialAttackPoint,
            int specialAttackPointPerUnit
        )
        {
            Debug.Log($">>special_attack_point<< {specialAttackPoint} {specialAttackPointPerUnit}");
            this.specialAttackPointPerUnit = specialAttackPointPerUnit;
            this.specialAttackPoint = specialAttackPoint;
            this.specialAttackPointRoot.alpha = 1;
            ForceUpdate();
        }

        public void HideSpecialAttack()
        {
            this.specialAttackPointRoot.alpha = 0;
        }

        public void ShowCharacterValues(
            string architypeId,
            bool isLocalPlayer,
            int level,
            int attack,
            int defend,
            int dexerity,
            int evasion,
            int critical,
            int maxHealth,
            int health
        )
        {
            var bannerPreset = Contexts.sharedInstance.GetProvider<ICharacterBannerPreset>();
            var bannerSprite = bannerPreset.GetBannerSprite(architypeId);
            banner.sprite = bannerSprite;

            healthProgress.fillAmount = health * 1f / (maxHealth * 1f);
            healthText.text = $"{health.ToString()}/{maxHealth.ToString()}";
            activeBg.sprite = isLocalPlayer ? localPlayerSprite : opponentSprite;

            var architypePreset = Contexts.sharedInstance.GetProvider<ICharacterArchitypePreset>();
            var architype = architypePreset.GetCharacterArchitypeProperty(architypeId);

            levelText.text = level.ToString();
            nameText.text = architype.name;
            bloodTypeImage.sprite = bloodTypePreset.GetBloodSpriteByGroup(architype.group);
        }
    }
}
