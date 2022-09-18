using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Taiga.Core.Unity.Preset;
using Taiga.Core.Character;
using UnityEngine.UI;
using Taiga.Core.Unity.CharacterBanner;
using Taiga.Core.Unity.Audio;

namespace Taiga.Core.Unity.CharacterCooldownRank
{

    public struct CharacterCooldownRankItem
    {
        public int id;
        public string architypeId;
        public string name;
        public int cooldown;
        public bool isLocalPlayer;
        public CharacterGroup group;
        public int level;
        public int maxHealth;
        public int health;
    }

    public class CharacterCooldownRankPresenter : SerializedMonoBehaviour
    {

        internal static CharacterCooldownRankPresenter Instance { get; private set; }

        [Header("Config")] [SerializeField] GameCharacterGroupPreset bloodTypePreset;
        [SerializeField] private Sprite localPlayerBorderSprite;
        [SerializeField] private Sprite opponentBorderSprite;
        [SerializeField] private Sprite localPlayerBGSprite;
        [SerializeField] private Sprite opponentBGSprite;
        [SerializeField] private Sprite localPlayerHealthSprite;
        [SerializeField] private Sprite opponentHealthSprite;

        [SerializeField] private GameObject root;
        [SerializeField] private CharacterCooldownRankItemPresenter itemPresenterTemplate;


        Dictionary<int, CharacterCooldownRankItemPresenter> itemPresenterById;

        private HorizontalLayoutGroup layoutGroup;
        private ICharacterBannerPreset bannerPreset;
        private bool inited;

        private void Awake()
        {
            Instance = this;
            itemPresenterById = new Dictionary<int, CharacterCooldownRankItemPresenter>();
            itemPresenterTemplate
                .gameObject
                .SetActive(false);
        }

        public void SetItems(IEnumerable<CharacterCooldownRankItem> items)
        {
            var pendingIds = new HashSet<int>(itemPresenterById.Keys);
            bannerPreset = bannerPreset ?? Contexts.sharedInstance.GetProvider<ICharacterBannerPreset>();
            // Skip first item from items list
            foreach (var pair in items.Skip(1).Select((value, index) => (value, index)))
            {
                var item = pair.value;
                var i = pair.index;

                var hasPresenter = itemPresenterById.TryGetValue(
                    item.id,
                    out var presenter
                );

                if (!hasPresenter)
                {
                    presenter = Instantiate(itemPresenterTemplate, root.transform);
                    presenter.gameObject.SetActive(true);
                    presenter.SetTitle($"{item.name} ({item.id})");
                    itemPresenterById[item.id] = presenter;
                }

                var info = new CharacterTurnInfo
                {
                    level = item.level,
                    maxHealth = item.maxHealth,
                    health = item.health,
                    characterSprite = bannerPreset.GetTurnBannerSprite(item.architypeId),
                    bloodTypeSprite = bloodTypePreset.GetBloodSpriteByGroup(item.group),
                    bgSprite = item.isLocalPlayer ? localPlayerBGSprite : opponentBGSprite,
                    borderSprite = item.isLocalPlayer ? localPlayerBorderSprite : opponentBorderSprite,
                    healthSprite = item.isLocalPlayer ? localPlayerHealthSprite : opponentHealthSprite,
                };

                presenter.SetInfo(info);
                presenter.SetCooldown(item.cooldown);
                presenter.SetIndexWithAnimation(i, !hasPresenter);
                pendingIds.Remove(item.id);
            }

            foreach (var pendingId in pendingIds)
            {
                var presenter = itemPresenterById[pendingId];
                itemPresenterById.Remove(pendingId);
                Destroy(presenter.gameObject);
            }

            // Prevent playing sound on first frame
            if (!inited)
            {
                inited = true;
            }
            else
            {
                //SoundEffectManager.Instance.PlaySoundEffect(SoundEffect.Rollover);
            }
        }

        internal void SetEnableLayoutGroup(bool value)
        {
            layoutGroup = layoutGroup ?? GetComponent<HorizontalLayoutGroup>();
            layoutGroup.enabled = value;
        }
    }
}
