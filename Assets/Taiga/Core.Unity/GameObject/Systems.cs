using Entitas;

namespace Taiga.Core.Unity
{

    public class GameObjectLinkSystems : Feature
    {
        public GameObjectLinkSystems(Contexts contexts)
        {
            Add(new RemoveLink_WhenUnlinked(contexts));
        }
    }

}
