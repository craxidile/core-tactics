using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Taiga.Core.Unity
{

    public sealed class Entity_GameObject : IComponent
    {
        public GameObject value;
    }

    [Cleanup(CleanupMode.RemoveComponent)]
    public sealed class Entity_GameObjectUnlink : IComponent
    {
    }

}
