using System.Collections.Generic;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterBanner
{
    public interface ICharacterBannerPreset : IProvider
    {
        Sprite GetBannerSprite(string characterArchitypeId);
        Sprite GetTurnBannerSprite(string characterArchitypeId);
        Sprite GetPreviewAttackBannerSprite(string characterArchitypeId);
        Sprite GetCharacterPhotoSprite(string characterArchitypeId);
        Sprite GetCharacterTitleSprite(string characterArchitypeId);
    }
}
