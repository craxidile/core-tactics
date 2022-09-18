using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

namespace Taiga.Core.Unity.CharacterCooldownRank
{
    public struct CharacterTurnInfo
    {
        public int level;
        public int maxHealth;
        public int health;
        internal Sprite characterSprite;
        internal Sprite bloodTypeSprite;
        internal Sprite bgSprite;
        internal Sprite borderSprite;
        internal Sprite healthSprite;
    }

    public class CharacterCooldownRankItemPresenter : MonoBehaviour
    {
        [Header("UI")] [SerializeField] private Image border;
        [SerializeField] private Image bg;
        [SerializeField] private Image characterImage;
        [SerializeField] private Image bloodTypeImage;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private Image healthProgress;

        [Header("Unused")] public TextMeshProUGUI nameText;
        public TextMeshProUGUI cooldownText;

        public void SetTitle(string value)
        {
            nameText.text = value;
        }

        public void SetCooldown(int value)
        {
            cooldownText.text = value.ToString();
        }

        public void SetIndex(int value)
        {
            transform.SetSiblingIndex(value);
        }

        internal void SetIndexWithAnimation(int value, bool isFirstInit)
        {
            if (isFirstInit)
            {
                SetIndex(value);
                return;
            }

            CharacterCooldownRankPresenter.Instance.SetEnableLayoutGroup(false);

            var targetPos = transform.parent.GetChild(value).localPosition;
            transform.DOLocalMoveX(targetPos.x, 0.5f).OnComplete(() =>
            {
                SetIndex(value);
                CharacterCooldownRankPresenter.Instance.SetEnableLayoutGroup(true);
            });
        }

        public void SetInfo(CharacterTurnInfo info)
        {
            characterImage.sprite = info.characterSprite;

            healthProgress.fillAmount = info.health * 1f / (info.maxHealth * 1f);
            healthProgress.sprite = info.healthSprite;
            bg.sprite = info.bgSprite;
            border.sprite = info.borderSprite;
            levelText.text = info.level.ToString();
            bloodTypeImage.sprite = info.bloodTypeSprite;
        }
    }
}
