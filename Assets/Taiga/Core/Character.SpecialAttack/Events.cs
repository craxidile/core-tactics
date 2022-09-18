using Entitas;

namespace Taiga.Core.Character.SpecialAttack
{
    public static class CharacterSpecialAttackEvents
    {
        public static IMatcher<GameEntity> OnCharacterSpecialAttackPoint => GameMatcher.Character_SpecialAttackCurrentPoint;

    }
}
