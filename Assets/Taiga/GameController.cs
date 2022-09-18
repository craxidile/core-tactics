using Entitas;
using Taiga.Core.Unity;
using Taiga.Core;
using UnityEngine;

namespace Taiga
{

    public class GameController
    {
        readonly Systems _systems;

        public GameController(Contexts contexts)
        {
            _systems = new MainSystems(contexts);
        }

        public void Initialize()
        {
            _systems.Initialize();
        }

        public void Execute()
        {
            _systems.Execute();
            _systems.Cleanup();
        }

        public void TearDown()
        {
            _systems.TearDown();
        }

    }

    public class MainSystems : Systems
    {
        public MainSystems(Contexts contexts)
        {
            Add(new PresetSystems(contexts));
            Add(new GameSystems(contexts));
            Add(new PresenterSystems(contexts));
            Add(new GameInitializeSystem(contexts));
            Add(new GameCleanupSystems(contexts));
        }
    }

}
