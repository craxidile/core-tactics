using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Taiga.Core.CharacterAction
{
    public enum CharacterActionType
    {
        Move,
        Attack,
        SpecialAttack,
        EndTurn
    }

    public sealed class CharacterAction : IComponent
    {
        public int characterId;
        public CharacterActionType type;
    }

    public sealed class CharacterAction_Executable : IComponent
    {
    }

    public sealed class CharacterAction_Execute : IComponent
    {
    }

    [Cleanup(CleanupMode.DestroyEntity)]
    public sealed class CharacterAction_Cancel : IComponent
    {
    }

    public sealed class CharacterAction_Finish : IComponent
    {
    }

    [Cleanup(CleanupMode.DestroyEntity)]
    public sealed class CharacterAction_Remove : IComponent
    {
    }

}
