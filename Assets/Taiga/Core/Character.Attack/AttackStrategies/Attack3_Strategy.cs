using System.Collections.Generic;
using Taiga.Core.Character.Placement;
using UnityEngine;
using UnityEngine.Assertions;

namespace Taiga.Core.Character.Attack
{

    public class Attack3_Strategy : CharacterSpecificAttackStrategy
    {
        int attackPoint;

        public Attack3_Strategy(CharacterEntity character, int attackPoint) : base(character)
        {
            this.attackPoint = attackPoint;
        }

        public override ICollection<Vector2Int> CharacterSelectionPositionOffsetsPerDirection => new HashSet<Vector2Int>
        {
            new Vector2Int(0, 1),
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 0),
        };

        public override ICollection<Vector2Int> TargetSelectionPositionOffsets => new HashSet<Vector2Int>
        {
            new Vector2Int(0, 1),
            new Vector2Int(0, 2),
            new Vector2Int(0, 3),
            new Vector2Int(0, 4),
            new Vector2Int(0, -1),
            new Vector2Int(0, -2),
            new Vector2Int(0, -3),
            new Vector2Int(0, -4),
        };

        protected override bool GetCharacterDamageInfo(
            MapDirection attackDirection,
            CharacterEntity targetCharacter,
            Vector2Int targetPosititonOffset,
            out CharacterDamageInfo info)
        {
            var damage = character
                .AsCharacter_Attack()
                .CalculateDamage(
                    targetCharacter.Id,
                    attackPoint: attackPoint,
                    out var level
                );

            var position = character
                .AsCharacter_Placement()
                .Position;

            var targetPosition = position + targetPosititonOffset
                .TransformToMapDirection(attackDirection);

            info = new CharacterDamageInfo
            {
                characterId = targetCharacter.Id,
                level = level,
                damage = damage,
                hitCount = 1,
                fromDirection = attackDirection,
                bumpInfo = new CharacterBumpInfo()
                {
                    toPosition = targetPosition,
                    type = BumpType.Fly,
                }
            };

            return true;
        }

    }
}
