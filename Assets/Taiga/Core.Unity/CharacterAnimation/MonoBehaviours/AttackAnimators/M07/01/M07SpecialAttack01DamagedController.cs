using DG.Tweening;
using Taiga.Core.Unity.CharacterAnimation.Base;

namespace Taiga.Core.Unity.CharacterAnimation
{
    public class M07SpecialAttack01DamagedController : BaseM07SpecialAttack01
    {
        public override bool AutoMoveToward => true;
        public override CharacterTimelineAnimatorRole Role => CharacterTimelineAnimatorRole.Damaged;
        public override float MoveTowardAmount => 0.25f;
        public override string AttackAnimatorTrigger => null;

        protected override void OnAttack01()
        {
            // Will implement later
        }

        protected override void OnReset()
        {
            DOTween
                .Sequence()
                .AppendInterval(1f)
                .AppendCallback(() =>
                {
                    SetAnimatorTrigger($"up");
                    OnEnd();
                })
                .Play();
        }
    }
}