using System.Collections;
using System.Collections.Generic;
using Taiga.Core.Character.Placement;
using UnityEngine;

namespace Taiga.Core.Character.Attack
{
    public class SplitAttack_Strategy : DirectionAttackStrategy
    {
        readonly int attackPoint;
        readonly int bumpDistance = 0;
        readonly BumpType? bumpType;

        public SplitAttack_Strategy(CharacterEntity character, int attackPoint, int bumpDistance = 0, BumpType? bumpType = null) : base(character)
        {
            this.attackPoint = attackPoint;
            this.bumpDistance = bumpDistance;
            this.bumpType = bumpType;
        }

        // TODO Fix this
        public override AttackType AttackType => AttackType.Attack1;

        protected override ICollection<Vector2Int> DamagePositionOffsetsPerDirection => new HashSet<Vector2Int>()
        {
            new Vector2Int(1, 1),
            new Vector2Int(-1, 1),
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
                bumpInfo = bumpDistance > 0 ? new CharacterBumpInfo()
                {
                    toPosition = targetCharacterPosition + (selectedDirection.GetUnitVector() * bumpDistance),
                    type = bumpType.Value
                } : (CharacterBumpInfo?)null
            };

            return true;
        }
    }
}
