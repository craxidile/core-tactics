using Entitas;

namespace Taiga.Core.Unity.Character
{
    public static class CharacterEvents
    {
        public static IMatcher<GameEntity> OnCharacterCreated => GameMatcher.Character;
    }
}
