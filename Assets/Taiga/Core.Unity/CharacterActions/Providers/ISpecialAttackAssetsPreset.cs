using Taiga.Core;
using Taiga.Core.Character.Attack;
using Taiga.Core.Unity.Preset;

namespace DefaultNamespace
{
    public interface ISpecialAttackAssetsPreset : IProvider
    {
        GameAssetsPreset.SpecialAttackAssets? GetSpecialAttackAssets(string architypeId, AttackType attackType);
    }
}
