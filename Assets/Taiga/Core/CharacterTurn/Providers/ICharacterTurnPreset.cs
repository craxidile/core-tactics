using System;
using Sirenix.Serialization;
using Taiga.Core.Character.Attack;
using Taiga.Core.CharacterAction;
using UnityEngine;

namespace Taiga.Core.CharacterTurn
{

    public interface ICharacterTurnPreset : IProvider
    {
        int MaxTurnAffinity { get; }
        int GetAffinityUsage(CharacterActionType actionType);
    }
}
