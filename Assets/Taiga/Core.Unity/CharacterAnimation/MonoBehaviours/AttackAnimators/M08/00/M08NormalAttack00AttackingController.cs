using Taiga.Core.Unity.CharacterAnimation.Base;
using Taiga.Core.Unity.CharacterAnimation.Effect;

namespace Taiga.Core.Unity.CharacterAnimation.M08._00
{
    public class M08NormalAttack00AttackingController: BaseM08NormalAttack00
    {
        public override bool AutoMoveToward => true;
        public override CharacterTimelineAnimatorRole Role => CharacterTimelineAnimatorRole.Attacking;
        public override float MoveTowardAmount => 0.15f;
        public override string AttackAnimatorTrigger => "attack_1";
        
        protected override void OnReset()
        {
            MoveBack();
            OnEnd();
        }

        protected override void OnStrike()
        {
            ShowDirectedEffect(AttackEffectItem.AttackEffectItem1, 0.8f);
        }
    }
}