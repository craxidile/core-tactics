namespace Taiga.Core.Unity.CharacterAnimation
{

    public interface ICharacterAnimationPreset : IProvider
    {
        float DamagedFlySpeed { get; }

        float WalkSpeed { get; }
    }
}
