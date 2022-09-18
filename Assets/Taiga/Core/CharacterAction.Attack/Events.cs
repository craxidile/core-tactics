using Entitas;

namespace Taiga.Core.CharacterAction.Attack
{
    public static class CharacterActionAttackEvents
    {
        public static IMatcher<GameEntity> OnAttackStrategySelection => GameMatcher.CharacterAction_AttackStrategySelection;

        public static IMatcher<GameEntity> OnAttackStrategyFocus => GameMatcher.CharacterAction_AttackStrategySelection_Focus;

        public static IMatcher<GameEntity> OnAttackStrategyPreviousSelectionPositions => GameMatcher.CharacterAction_AttackStrategyPreviousSelectionSelectedPosition;

        public static IMatcher<GameEntity> OnSpecialAttackReady => GameMatcher.CharacterAction_SpecialAttackReady;
    }
}
