using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using Taiga.Core.Character.Attack;
using UnityEngine;

namespace Taiga.Core.CharacterSequence
{

    public struct Movement
    {
        public MapDirection direction;
        public Vector2Int toPosition;
    }

    [Unique]
    public sealed class CharacterSequence_Initial : IComponent
    {
    }

    public sealed class CharacterSequence : IComponent
    {
        public int characterId;
    }

    public sealed class CharacterSequence_Move : IComponent
    {
        public IEnumerable<Movement> movementSteps;
    }

    public sealed class CharacterSequence_Attack : IComponent
    {
        public AttackType attackType;
        public MapDirection direction;
    }

    public sealed class CharacterSequence_Block : IComponent
    {
        public MapDirection bumpDirection;
    }

    public sealed class CharacterSequence_SourceSequenceEntity : IComponent
    {
        [EntityIndex] public GameEntity value;
    }

    public sealed class CharacterSequence_Damaged : IComponent
    {
        public int damage;
        public MapDirection bumpDirection;
    }

    public sealed class CharacterSequence_DamagedEndPosition : IComponent
    {
        public Vector2Int value;
    }

    public sealed class CharacterSequence_DamagedBumpPosition : IComponent
    {
        public Vector2Int value;
    }

    public sealed class CharacterSequence_Commit : IComponent
    {
    }

    public sealed class CharacterSequence_Commited : IComponent
    {
    }

    public sealed class CharacterSequence_Finish : IComponent
    {
    }

    [Cleanup(CleanupMode.DestroyEntity)]
    public sealed class CharacterSequence_Destroy : IComponent
    {
    }

}
