namespace Taiga.Core.Unity.MapCellHighlight
{

    public class MapCellHighlightSystems : Feature
    {
        public MapCellHighlightSystems(Contexts contexts)
        {
            Add(new UpdateWalkHighlight_WhenMoveActionPosibilitiesChanged(contexts));
            Add(new UpdateAttackHighlights_WhenAttackStrategySelectionChanged(contexts));
            Add(new UpdateWalkHighlightFocus_WhenMoveActionPredictionChanged(contexts));
            Add(new UpdateAttackHighlightsFocus_WhenAttackStrategySelectionFocusChanged(contexts));
            Add(new OffAllHighlight_WhenCharacterActionCanceledOrExecuted(contexts));
            Add(new RemoveHighlight_WhenHighlightOff(contexts));
            Add(new UpdateCellHighlightPresenter_WhenHighlightChanged(contexts));
            Add(new UpdateCellHighlightPresenter_WhenMapHightlightDisableChanged(contexts));
        }
    }
}
