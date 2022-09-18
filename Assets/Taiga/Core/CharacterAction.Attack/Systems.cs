using Entitas;

namespace Taiga.Core.CharacterAction.Attack
{

    public class CharacterActionAttackSystems : Feature
    {
        public CharacterActionAttackSystems(Contexts contexts)
        {
            Add(new CreateAttackStrategy_WhenActionCreated(contexts));
            Add(new CreateSelection_WhenStrategyCreated(contexts));
            Add(new CreateSelectionPossiblePositions_WhenSelectionChanged(contexts));
            Add(new CreateFocusPredictedPositionAndFocusValidation_WhenFocused(contexts));
            Add(new CreatePredictedDamage_WhenFinalSelectionFocusValid(contexts));
            Add(new MarkExecutable_WhenLastSelectionFocusValid(contexts));
            Add(new CreateActionSequence_WhenActionExecuted(contexts));
            Add(new UpdateCombo_WhenActionExecuted(contexts));
        }
    }

}
