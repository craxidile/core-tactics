using Taiga.Core.Unity.CharacterAnimation.Base;
using Taiga.Core.Unity.CharacterAnimation.Effect;

namespace Taiga.Core.Unity.CharacterAnimation.N16._00
{
    public class N16NormalAttack00AttackingController: BaseN16NormalAttack00
    {
        public override bool AutoMoveToward => false;
        public override CharacterTimelineAnimatorRole Role => CharacterTimelineAnimatorRole.Attacking;
        public override float MoveTowardAmount => 0;
        public override string AttackAnimatorTrigger => "attack_1";
        
        protected override void OnReset()
        {
            OnEnd();
        }

        protected override void OnStrike()
        {
            ShowDirectedEffect(AttackEffectItem.AttackEffectItem1, 0.8f);
        }
    }
}