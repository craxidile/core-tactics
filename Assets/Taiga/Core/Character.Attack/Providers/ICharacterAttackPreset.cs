namespace Taiga.Core.Character.Attack
{
    public interface ICharacterAttackPreset : IProvider
    {
        bool GetGroupRelativeAttackRate(CharacterGroup attacker, CharacterGroup damaged, out float rate);
        int SpecialAttackPointPerUnit { get; }
        float CriticalAttackFactor { get; }
        int BumpDamage { get; }
    }
}
