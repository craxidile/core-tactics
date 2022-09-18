using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Taiga.Core.Player
{
    public sealed class Player : IComponent
    {
        [PrimaryEntityIndex] public int id;
    }

}
