using System;
using System.Collections.Generic;
using DefaultNamespace;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Taiga.Core.Character.Attack;
using Taiga.Core.Unity.Character;
using Taiga.Core.Unity.CharacterAnimation;
using Taiga.Core.Unity.CharacterAnimation.Effect;
using Taiga.Core.Unity.CharacterBanner;
using Taiga.Core.Unity.MapCell;
using UnityEngine;

namespace Taiga.Core.Unity.Preset
{
    [CreateAssetMenu(fileName = "TaigaAssetsPreset", menuName = "Taiga Preset/Assets", order = 1)]
    public class GameAssetsPreset : SerializedScriptableObject, IMapCellPrefabPreset, ICharacterPrefabPreset, ICharacterBannerPreset, ICharacterAnimatorPreset, ISpecialAttackAssetsPreset, ICharacterAttackEffectAssetPreset
    {
        [Serializable]
        public struct CharacterAnimationConfig
        {
            public int attackEffectDelay;
            public Vector3 attack1EffectOffset;
            public Vector3 attack2EffectOffset;
            public Vector3 attack3EffectOffset;
            public Vector3 attack4EffectOffset;
        }

        public struct SpecialAttackItemTransforms
        {
            public AttackEffectType effectType;
            public Transform frontNormalTransform;
            public Transform frontFlippedTransform;
            public Transform backNormalTransform;
            public Transform backFlippedTransform;
        }

        public struct SpecialAttackAssets
        {
            public string name;
            [Multiline(3)] public string description;
            public Sprite banner;
            public Dictionary<AttackEffectItem, SpecialAttackItemTransforms> effectItemTransformMap;
        }

        [Serializable]
        public struct CharacterArchitypeConfig
        {
            public Sprite banner;
            public Sprite turnBanner;
            public Sprite previewAttackBanner;
            public Sprite characterTitle;
            public Sprite characterPhoto;
            public RuntimeAnimatorController animatorController;
            public CharacterAnimationConfig animation;
            public Dictionary<AttackType, SpecialAttackAssets> specialAttackAssetsMap;
        }

        public GameObject characterPrefab;
        public GameObject CharacterPrefab => characterPrefab;
        public GameObject mapCellPrefab;
        public GameObject MapCellPrefab => mapCellPrefab;
        
        [SerializeField] public Dictionary<AttackEffectType, RuntimeAnimatorController> effectAssetsMap;
        [SerializeField] Dictionary<string, CharacterArchitypeConfig> characterArchitypeConfigs;

        public Sprite GetBannerSprite(string architypeId)
        {
            return characterArchitypeConfigs[architypeId].banner;
        }

        public Sprite GetTurnBannerSprite(string architypeId)
        {
            return characterArchitypeConfigs[architypeId].turnBanner;
        }

        public Sprite GetPreviewAttackBannerSprite(string architypeId)
        {
            return characterArchitypeConfigs[architypeId].previewAttackBanner;
        }

        public Sprite GetCharacterPhotoSprite(string architypeId)
        {
            return characterArchitypeConfigs[architypeId].characterPhoto;
        }

        public Sprite GetCharacterTitleSprite(string architypeId)
        {
            return characterArchitypeConfigs[architypeId].characterTitle;
        }

        public Dictionary<AttackType, SpecialAttackAssets> GetSpecialAttackAssetMap(string architypeId)
        {
            return characterArchitypeConfigs[architypeId].specialAttackAssetsMap;
        }

        public RuntimeAnimatorController GetAnimatorController(string architypeId)
        {
            return characterArchitypeConfigs[architypeId].animatorController;
        }

        public CharacterAnimationConfig GetAnimationConfig(string architypeId)
        {
            return characterArchitypeConfigs[architypeId].animation;
        }

        public ICharacterAttackAnimator GetAttackAnimator(AttackType attackType)
        {
            return new BasicAttackAnimator();
        }

        public SpecialAttackAssets? GetSpecialAttackAssets(string architypeId, AttackType attackType)
        {
            var map = characterArchitypeConfigs[architypeId].specialAttackAssetsMap;
            if (map == null || !map.ContainsKey(attackType)) return null;
            return map[attackType];
        }
        public Dictionary<AttackEffectType, RuntimeAnimatorController> GetEffectAssetsMap()
        {
            return effectAssetsMap;
        }
    }

}
