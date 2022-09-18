using Entitas;

namespace Taiga.Core.Character.HateCount
{
    public static class CharacterHateCountEvents
    {
        public static IMatcher<GameEntity> OnCharacterHateCountPoint => GameMatcher.Character_HateCountPoint;

    }
}
