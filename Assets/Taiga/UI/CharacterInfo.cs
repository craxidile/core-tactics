using System.Collections;
using System.Collections.Generic;
using Taiga.Core;
using Taiga.Core.Character.Attack;
using Taiga.Core.CharacterFactory;
using Taiga.Core.Unity.Preset;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoData
{
    public string ArchitypeId { get; private set; }
    public Sprite Sprite { get; private set; }
    public int Level { get; private set; }
    public int Health { get; private set; }
    public int MaxHealth { get; private set; }
    public Sprite BgSprite { get; private set; }
    public CharacterEntity_Attack Attack { get; private set; }

    public CharacterInfoData(string architypeId, Sprite sprite, int level, int health, int maxHealth, Sprite bgSprite, CharacterEntity_Attack attack)
    {
        ArchitypeId = architypeId;
        Sprite = sprite;
        Level = level;
        Health = health;
        MaxHealth = maxHealth;
        BgSprite = bgSprite;
        Attack = attack;
    }
}

public class CharacterInfo : MonoBehaviour
{
    [Header("Image")]
    [SerializeField] private Image image;
    [SerializeField] private Image activeBg;

    [Header("HP")]
    [SerializeField] private Image healthProgress;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("SP")]
    [SerializeField] private CanvasGroup specialAttackPointRoot;
    [SerializeField] private Image[] specialAttackProgresses;

    [Header("Details")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI levelText;

    [Header("BloodType")]
    [SerializeField] private GameCharacterGroupPreset bloodTypePreset;
    [SerializeField] private Image bloodTypeImage;

    internal void SetupInfo(CharacterInfoData characterInfo)
    {
        image.sprite = characterInfo.Sprite;

        healthProgress.fillAmount = characterInfo.Health / characterInfo.MaxHealth;
        healthText.text = $"{characterInfo.Health}/{characterInfo.MaxHealth}";
        activeBg.sprite = characterInfo.BgSprite;

        var architypePreset = Contexts.sharedInstance.GetProvider<ICharacterArchitypePreset>();
        var architype = architypePreset.GetCharacterArchitypeProperty(characterInfo.ArchitypeId);

        levelText.text = characterInfo.Level.ToString();
        nameText.text = architype.name;
        bloodTypeImage.sprite = bloodTypePreset.GetBloodSpriteByGroup(architype.group);

        if (characterInfo.Attack.HasSpecialAttack)
        {
            var specialAttackPointPerUnit = Contexts.sharedInstance
                .GetProvider<ICharacterAttackPreset>()
                .SpecialAttackPointPerUnit;
            ShowSpecialAttack(characterInfo.Attack.SpecialAttackPoint, specialAttackPointPerUnit);
        }
        else
        {
            HideSpecialAttack();
        }
    }


    private void ShowSpecialAttack(
        int specialAttackPoint,
        int specialAttackPointPerUnit
    )
    {
        specialAttackPointRoot.alpha = 1;

        UpdateSpecialAttackUI();

        void UpdateSpecialAttackUI()
        {
            for (var i = 0; i < specialAttackProgresses.Length; i++)
            {
                var unitValue = specialAttackPoint - i * specialAttackPointPerUnit;
                unitValue = Mathf.Min(specialAttackPointPerUnit, unitValue);
                unitValue = Mathf.Max(0, unitValue);
                specialAttackProgresses[i].fillAmount = unitValue / (specialAttackPointPerUnit * 1f);
            }
        }
    }

    private void HideSpecialAttack()
    {
        specialAttackPointRoot.alpha = 0;
    }
}

