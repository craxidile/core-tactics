using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using Taiga.Core.CharacterAction;

namespace Taiga.Core.CharacterTurn
{

    [Unique]
    public sealed class Game_CharacterTurnOrder : IComponent
    {
        public IEnumerable<int> characterIds;
    }

    public sealed class Character_TurnProperty : IComponent
    {
        public int affinityRegainRate;
    }

    public sealed class Character_TurnAffinity : IComponent
    {
        public int value;
    }

    public sealed class CharacterTurn : IComponent
    {
        public int characterId;
    }

    [Cleanup(CleanupMode.DestroyEntity)]
    public sealed class CharacterTurn_Finished : IComponent
    {
    }

    public sealed class CharacterTurn_PossibleActions : IComponent
    {
        public CharacterActionType[] actions;
    }

}
