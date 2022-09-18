using System.Collections.Generic;
using Taiga.Core.Character.Placement;
using UnityEngine;
using UnityEngine.Assertions;

namespace Taiga.Core.Character.Attack
{

    public class Attack2_Strategy : PositionAttackStrategy
    {
        readonly int attackPoint;

        public Attack2_Strategy(CharacterEntity character, int attackPoint) : base(character)
        {
            this.attackPoint = attackPoint;
        }

        public override AttackType AttackType => AttackType.Attack2;

        protected override ICollection<Vector2Int> SelectablePositionOffsetsPerDirection => new HashSet<Vector2Int>()
        {
            new Vector2Int(0, 1),
            new Vector2Int(0, 2),
            new Vector2Int(0, 3)
        };

        protected override ICollection<Vector2Int> DamagePositionOffsetsFromSelectedPosition => new HashSet<Vector2Int>()
        {
            new Vector2Int(0, 0),
            new Vector2Int(0, 1),
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 0),
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
