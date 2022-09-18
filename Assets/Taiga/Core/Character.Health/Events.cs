using Entitas;

namespace Taiga.Core.Character.Health
{
    public static class CharacterHealthEvents
    {
        public static IMatcher<GameEntity> OnDamage => GameMatcher.CharacterDamage;

        public static IMatcher<GameEntity> OnCharacterDead => GameMatcher.Character_HealthDead;

        public static IMatcher<GameEntity> OnCharacterHealth => GameMatcher.Character_HealthPoint;

    }
}
