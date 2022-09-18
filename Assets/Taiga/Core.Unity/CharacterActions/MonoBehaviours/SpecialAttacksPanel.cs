using System.Collections.Generic;
using DefaultNamespace;
using Sirenix.Serialization;
using Sirenix.OdinInspector;
using UnityEngine;
using Taiga.Core;
using Taiga.Core.Character.Attack;
using Taiga.Core.Unity.Audio;
using Taiga.Core.CharacterFactory;

public class SpecialAttacksPanel : SerializedMonoBehaviour
{
    public delegate void SpecialAttackDelegate(AttackType attackType);

    internal static SpecialAttacksPanel Instance { get; private set; }

    [OdinSerialize] private Dictionary<int, SpecialAttackItem> buttonByIndex;
    [SerializeField] private float fadeDuration = 0.5f;

    private CanvasGroup canvasGroup;
    SpecialAttackDelegate callback;
    private List<AttackType> eligibleAttackTypes;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        foreach (var pair in buttonByIndex)
        {
            var index = pair.Key;
            var specialAttackItem = pair.Value;
            specialAttackItem.AddOnClickListener(() =>
            {
                if (index >= eligibleAttackTypes.Count) return;
                callback?.Invoke(eligibleAttackTypes[index]);
                SoundEffectManager.Instance.PlaySoundEffect(SoundEffect.Click, true);
            });
        }

        StartCoroutine(canvasGroup.LerpAlpha(0, fadeDuration));
    }

    public void AskForActions(string architypeId, List<AttackType> allAttackTypes, List<AttackType> eligibleAttackTypes, SpecialAttackDelegate callback)
    {
        StopAllCoroutines();
        StartCoroutine(canvasGroup.LerpAlpha(1, fadeDuration));

        var specialAttackAssetsPreset = Contexts.sharedInstance.GetProvider<ISpecialAttackAssetsPreset>();

        this.eligibleAttackTypes = eligibleAttackTypes;
        this.callback = callback;

        var actionTypeCount = eligibleAttackTypes.Count;
        foreach (var index in buttonByIndex.Keys)
        {
            var specialAttackItem = buttonByIndex[index];
            var active = index < actionTypeCount;

            specialAttackItem.gameObject.SetActive(active);

            if (!active) continue;

            var attackType = eligibleAttackTypes[index];
            var specialAttackAssets = specialAttackAssetsPreset.GetSpecialAttackAssets(architypeId, attackType);
            var architypePreset = Contexts.sharedInstance.GetProvider<ICharacterArchitypePreset>();

            if (specialAttackAssets == null)
            {
                continue;
            }

            var characterArchitypePreset = architypePreset.GetCharacterArchitypeProperty(architypeId);
            var attackName = specialAttackAssets?.name;
            var attackDescription = specialAttackAssets?.description;
            var attackBanner = specialAttackAssets?.banner;
            var attackHit = 1; //TODO:
            var attackStatus = 0; //TODO:
            var attackDmg = characterArchitypePreset.specialAttack;
            var attackUnitUsage = characterArchitypePreset.specialAttackUnitUsage; //TODO: Change to new unitUsage property in `SpecialAttackUnitBounds`

            var info = new SpecialAttackInfo(
                attackBanner,
                attackName,
                attackHit,
                attackStatus,
                attackDmg,
                attackUnitUsage
            );

            specialAttackItem.SetInfo(info);
        }
    }

    public void ClearAsking()
    {
        StopAllCoroutines();
        StartCoroutine(canvasGroup.LerpAlpha(0, fadeDuration));
        callback = null;

        foreach (var button in buttonByIndex.Values)
        {
            button.gameObject.SetActive(false);
        }
    }
}
