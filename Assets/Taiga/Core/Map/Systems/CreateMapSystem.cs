using Entitas;

namespace Taiga.Core.Map
{

    public class CreateMapSystems : IInitializeSystem
    {

        GameContext game;

        public CreateMapSystems(Contexts contexts)
        {
            this.game = contexts.game;
        }

        public void Initialize()
        {
            var mapPreset = game.GetProvider<IMapPreset>();
            var positions = mapPreset.GetMapTerrain();
            foreach (var position in positions)
            {
                var cellEntity = game.CreateEntity();
                cellEntity.AddMapCell(position);
            }
        }
    }
}
