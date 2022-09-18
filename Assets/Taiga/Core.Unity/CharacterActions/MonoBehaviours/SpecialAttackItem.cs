using Taiga.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal struct SpecialAttackInfo
{
    internal Sprite Sprite;
    internal string Name;
    internal int HitCount;
    internal int Status; //TODO: implement
    internal float Damage;
    internal int UnitUsage;

    public SpecialAttackInfo(Sprite sprite, string name, int hitCount, int status, float damage, int unitUsage)
    {
        this.Sprite = sprite;
        this.Name = name;
        this.HitCount = hitCount;
        this.Status = status;
        this.Damage = damage;
        this.UnitUsage = unitUsage;
    }
}

[RequireComponent(typeof(ButtonHover))]
public class SpecialAttackItem : MonoBehaviour
{
    private const string HIT_TEXT = "Hits";
    private const float HOVER_ALPHA = 1f;
    private const float DEFAULT_ALPHA = 0.5f;

    [SerializeField] private Image bannerImage;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI hitcountText;
    [SerializeField] private Image statusImage;
    [SerializeField] private TextMeshProUGUI damageText;

    [Header("SP")]
    [SerializeField] private Image[] specialAttackProgresses;

    [Header("Hover")]
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color defaultColor;

    internal void SetInfo(SpecialAttackInfo data)
    {
        bannerImage.sprite = data.Sprite;
        text.text = data.Name;
        hitcountText.text = $"{data.HitCount.ToString()} {HIT_TEXT}";
        // statusImage.sprite = data.attackStatus == 0 ? null : Resources.Load<Sprite>($"Status/{attackStatus}");
        damageText.text = $"{data.Damage.ToString()}";

        for (int i = 0; i < specialAttackProgresses.Length; i++)
        {
            var specialAttackUnit = specialAttackProgresses[i];
            specialAttackUnit.gameObject.SetActive(i < data.UnitUsage);
        }
    }

    private ButtonHover buttonHover;
    internal void AddOnClickListener(UnityEngine.Events.UnityAction onClick)
    {
        buttonHover = buttonHover ?? GetComponent<ButtonHover>();
        buttonHover.onClick.AddListener(onClick);
    }

    private Image image;
    private CanvasGroup canvasGroup;
    public void SetHover(bool isHover)
    {
        image = image ?? GetComponent<Image>();
        canvasGroup = canvasGroup ?? GetComponent<CanvasGroup>();

        image.color = isHover ? hoverColor : defaultColor;
        canvasGroup.SetAlpha(isHover ? HOVER_ALPHA : DEFAULT_ALPHA);
    }

    private void OnDestroy()
    {
        buttonHover?.onClick.RemoveAllListeners();
    }
}
