using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Taiga.Core.Character.Health
{
    public sealed class Character_HealthProperty : IComponent
    {
        public int maxHealth;
    }

    public sealed class Character_HealthPoint : IComponent
    {
        public int value;
    }

    public sealed class Character_HealthDead : IComponent
    {
    }

    [Cleanup(CleanupMode.DestroyEntity)]
    public sealed class CharacterDamage : IComponent
    {
        public int? sourceCharacterId;
        public int characterId;
        public int damage;
    }

}
