namespace Taiga.Core.Unity.MapInput
{

    public class MapInputSystems : Feature
    {
        public MapInputSystems(Contexts contexts)
        {
            Add(new UpdatePointerPosition(contexts));
            Add(new UpdateMapCellHover_WhenPointerPositionChanged(contexts));
            Add(new TriggerMapCell_PointerTriggered(contexts));
        }
    }
}
