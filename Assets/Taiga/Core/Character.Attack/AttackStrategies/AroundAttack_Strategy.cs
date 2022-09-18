using System.Collections;
using System.Collections.Generic;
using Taiga.Core.Character.Placement;
using UnityEngine;

namespace Taiga.Core.Character.Attack
{
    public class AroundAttack_Strategy : DirectionAttackStrategy
    {
        readonly int attackPoint;

        public AroundAttack_Strategy(CharacterEntity character, int attackPoint) : base(character)
        {
            this.attackPoint = attackPoint;
        }

        // TODO Fix this
        public override AttackType AttackType => AttackType.Attack1;

        protected override ICollection<Vector2Int> DamagePositionOffsetsPerDirection => new HashSet<Vector2Int>()
        {
            new Vector2Int(0, 1),
            new Vector2Int(0, -1),
            new Vector2Int(1, 0),
            new Vector2Int(-1, 0),
            new Vector2Int(1, 1),
            new Vector2Int(-1, 1),
            new Vector2Int(1, -1),
            new Vector2Int(-1, -1),
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
