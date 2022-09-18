using UnityEngine;

namespace Taiga.Core.Character.Attack
{
    public class AttackStrategyPreset : IAttackStrategyPreset
    {

        public int specialAttackPointUsage;
        public int SpecialAttackPointUsage => specialAttackPointUsage;

        public IAttackStrategy CreateStrategy(
            AttackType attackType,
            int attackPoint,
            CharacterEntity character
        )
        {
            // TODO: Fix this!
            switch (attackType)
            {
                case AttackType.Attack1:
                    return new Attack1_Strategy(character, attackPoint);
                case AttackType.Attack2:
                    return new Attack2_Strategy(character, attackPoint);
                case AttackType.Attack3:
                    return new Attack3_Strategy(character, attackPoint);
                case AttackType.Attack4:
                    return new Attack4_Strategy(character, attackPoint);
                case AttackType.Attack5:
                    return new Attack4_Strategy(character, attackPoint);
                case AttackType.Attack6:
                    return new Attack4_Strategy(character, attackPoint);
                case AttackType.SpecialAttack1:
                    return new Attack1_Strategy(character, attackPoint);
                case AttackType.SpecialAttack2:
                    return new SplitAttack_Strategy(character, attackPoint);
                case AttackType.SpecialAttack3:
                    return new AroundAttack_Strategy(character, attackPoint);
                case AttackType.SpecialAttack4:
                    return new RowAttack_Strategy(character, attackPoint, bumpDistance: 1, bumpType: BumpType.Drag);
                case AttackType.SpecialAttack5:
                    return new SelectionAttack_Strategy(character, attackPoint,
                        new Vector2Int[]
                        {
                            new Vector2Int(0, 1),
                            new Vector2Int(0, 2),
                            new Vector2Int(0, 3)
                        }, false
                    );
                case AttackType.SpecialAttack6:
                    return new SplitAttack_Strategy(character, attackPoint);
                default:
                    System.Diagnostics.Debug.Assert(false, "Unknown attack type!");
                    return null;
            }
        }
    }
}
