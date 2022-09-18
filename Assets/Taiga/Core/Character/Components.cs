using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Taiga.Core.Character
{

    [Unique]
    public sealed class Game_NextCharacterId : IComponent
    {
        public int value;
    }

    public sealed class Character : IComponent
    {
        [PrimaryEntityIndex] public int id;
        [EntityIndex] public int ownerPlayerId;
        public string architypeId;
        public int level;
    }

    public enum CharacterGroup
    {
        O,
        A,
        B,
        AB
    }

}
