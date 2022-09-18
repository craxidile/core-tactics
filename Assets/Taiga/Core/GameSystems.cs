using Entitas;
using Taiga.Core.Character.HateCount;
using Taiga.Core.Character.SpecialAttack;
using Taiga.Core.CharacterAction;
using Taiga.Core.CharacterAction.Attack;
using Taiga.Core.CharacterAction.Move;
using Taiga.Core.CharacterHealth;
using Taiga.Core.CharacterSequence;
using Taiga.Core.CharacterTurn;
using Taiga.Core.Map;
using UnityEngine;

namespace Taiga.Core
{

    public class GameSystems : Systems
    {
        public GameSystems(Contexts contexts)
        {
            Add(new MapSystems(contexts));
            Add(new CharacterActionSystems(contexts));
            Add(new CharacterActionAttackSystems(contexts));
            Add(new CharacterActionMoveSystems(contexts));
            Add(new CharacterSequenceSystems(contexts));
            Add(new CharacterTurnSystems(contexts));
            Add(new CharacterHealthSystems(contexts));
            Add(new CharacterSpecialAttackSystems(contexts));
            Add(new CharacterHateCountSystems(contexts));
        }
    }
}
