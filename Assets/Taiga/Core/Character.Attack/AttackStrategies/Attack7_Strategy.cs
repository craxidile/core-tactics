//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Assertions;

//namespace Taiga.Core.Character.Attack
//{

//    public class Attack7_Strategy : BaseAttackStrategy
//    {
//        int attackPoint;

//        public Attack7_Strategy(CharacterEntity character, int attackPoint) : base(character)
//        {
//            this.attackPoint = attackPoint;
//        }

//        protected override ICollection<Vector2Int> PossiblePositionOffsets => new HashSet<Vector2Int>() {
//                new Vector2Int(0, 1),
//                new Vector2Int(0, 2),
//                new Vector2Int(0, 3),
//                new Vector2Int(0, 4),
//                new Vector2Int(1, 1),
//                new Vector2Int(1, 2),
//                new Vector2Int(1, 3),
//                new Vector2Int(2, 2),
//                new Vector2Int(-2, -2)
//            };

//        protected override ICollection<Vector2Int> GetAttackPositionOffsets(
//            Vector2Int focusPositionOffset
//        ) => PossiblePositionOffsets;

//        protected override bool GetPositionOffsetDamageInfo(
//            MapDirection focusDirection,
//            Vector2Int focusPositionOffset,
//            Vector2Int attackPositionOffset,
//            CharacterEntity targetCharacter,
//            out DamageLevel level,
//            out int damage,
//            out int hitCount,
//            out Direction bumpDirection,
//            out int bumpLength
//        )
//        {
//            damage = character
//                .AsCharacter_Attack()
//                .CalculateDamage(
//                    targetCharacter.Id,
//                    attackPoint: attackPoint,
//                    out level
//                );

//            bumpDirection = Direction.Forward;
//            bumpLength = 0;
//            hitCount = 1;

//            return true;
//        }

//    }
//}



