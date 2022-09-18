using System.Collections.Generic;
using Taiga.Core.Character.Placement;
using UnityEngine;

namespace Taiga.Core.Character.Attack
{
    public class SelectionAttack_Strategy : DirectionAttackStrategy
    {
        readonly int attackPoint;
        readonly Vector2Int[] damagePositionOffsetsPerDirection;

        public SelectionAttack_Strategy(
            CharacterEntity character,
            int attackPoint,
            Vector2Int[] damagePositionOffsetsPerDirection,
            bool throughable) : base(character, throughable)
        {
            this.attackPoint = attackPoint;
            this.damagePositionOffsetsPerDirection = damagePositionOffsetsPerDirection;
        }

        // TODO Fix this
        public override AttackType AttackType => AttackType.SpecialAttack5;

        protected override ICollection<Vector2Int> DamagePositionOffsetsPerDirection => new HashSet<Vector2Int>(damagePositionOffsetsPerDirection);

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
                bumpInfo = new CharacterBumpInfo()
                {
                    toPosition = targetCharacterPosition + selectedDirection.GetUnitVector(),
                    type = BumpType.Drag,
                }
            };

            return true;
        }

    }
}
