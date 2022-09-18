using Entitas;

namespace Taiga.Core.Map
{

    public class MapSystems : Feature
    {
        public MapSystems(Contexts contexts)
        {
            Add(new CreateMapSystems(contexts));
        }
    }
}
