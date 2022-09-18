namespace Taiga.Core.Unity.MapCell
{

    public class MapCellSystems : Feature
    {
        public MapCellSystems(Contexts contexts)
        {
            Add(new CreateMapCellGameObjectSystem(contexts));
        }
    }
}
