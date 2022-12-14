using System.Collections.Generic;
using Taiga.Core.Character.Placement;
using UnityEngine;
using UnityEngine.Assertions;

namespace Taiga.Core.Character.Attack
{
    public class Attack4_Strategy : DirectionAttackStrategy
    {
        readonly int attackPoint;

        public Attack4_Strategy(CharacterEntity character, int attackPoint) : base(character)
        {
            this.attackPoint = attackPoint;
        }

        public override AttackType AttackType => AttackType.Attack4;

        protected override ICollection<Vector2Int> DamagePositionOffsetsPerDirection => new HashSet<Vector2Int>()
        {
            new Vector2Int(0, 1)
        };

        protected override bool GetCharacterDamageInfo(
            MapDirection selectedDirection,
            CharacterEntity targetCharacter,
            out CharacterDamageInfo info
        )
        {
            var damage = character
                .AsCharacter_Attack()
                .CalculateDamage(
                    targetCharacter.Id,
                    attackPoint: attackPoint,
                    out var level
                );

            var targetCharacterPosition = targetCharacter
                .AsCharacter_Placement()
                .Position;

            info = new CharacterDamageInfo
            {
                characterId = targetCharacter.Id,
                level = level,
                damage = damage,
                hitCount = 1,
                fromDirection = selectedDirection,
                bumpInfo = null
            };

            return true;
        }

    }
}
