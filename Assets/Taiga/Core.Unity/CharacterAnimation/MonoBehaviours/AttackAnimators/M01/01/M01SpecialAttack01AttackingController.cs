using DG.Tweening;
using Taiga.Core.Character.Placement;
using Taiga.Core.Unity.CharacterAnimation.Base;
using Taiga.Core.Unity.CharacterAnimation.Bk01;
using Taiga.Core.Unity.CharacterAnimation.Effect;
using Taiga.Core.Unity.Effect.Providers;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.M01._01
{
    public class M01SpecialAttack01AttackingController : BaseM01SpecialAttack01
    {
        private const string ChargeClip = "Charge";
        private const string HardPunchClip = "HardPunchClip";

        private const float ChargeEffectDuration = 1.7f;
        private const float RunningTime = 0.75f;
        private const float StartRunningNormalizedTime = 0.56f;
        private const float EndRunningNormalizedTime = 0.62f;
        private const float BeforeAttackNormalizedTime = 0.65f;
        private const float BeforeEventAttackNormalizedTime = 0.398f;

        public override bool AutoMoveToward => true;
        public override CharacterTimelineAnimatorRole Role => CharacterTimelineAnimatorRole.Attacking;
        public override float MoveTowardAmount => 0f;
        public override string AttackAnimatorTrigger => "attack_special_1";

        public override Sequence OnStart()
        {
            var sequence = base.OnStart();
            AddAudioClip(ChargeClip, "charge");
            AddAudioClip(HardPunchClip, "m09_punch_hard");
            return sequence;
        }

        internal override void OnReset()
        {
            CharacterEntity?.AsCharacter_Placement().SetPlacement(
                CharacterAnimator.root.localPosition.UnityToRoundGamePosition(),
                Direction);
            OnEnd();
        }

        internal override void OnPrepare()
        {
            Debug.Log(">>on_prepare<<");
            PlayAudioClip(ChargeClip);
            ShowBodyEffect(AttackEffectItem.SpecialAttackEffectItem1, ChargeEffectDuration);
        }

        internal override void OnRun()
        {
            var victim = FindObjectOfType<M01SpecialAttack01DamagedController>();
            var targetPos = (CharacterAnimator.root.localPosition - victim.CharacterAnimator.root.localPosition)
                .UnityToRoundGamePosition()
                .TransformByDirection(Core.Direction.Backward);
            var x = targetPos.x > 0 ? -1 : 1;
            var y = targetPos.y > 0 ? -1 : 1;
            targetPos = new Vector2Int(targetPos.x != 0 ? targetPos.x + x : targetPos.x,
                targetPos.y != 0 ? targetPos.y + y : targetPos.y);

            var animator = CharacterAnimator.animator;
            if (targetPos.magnitude >= 1)
            {
                CharacterAnimator.root
                    .DOLocalMove(CharacterAnimator.root.localPosition + targetPos.GameToUnityPosition(),
                        RunningTime * targetPos.magnitude / 3)
                    .SetEase(Ease.InOutSine)
                    .OnUpdate(() =>
                    {
                        var distance = (CharacterAnimator.root.localPosition -
                                        victim.CharacterAnimator.root.localPosition)
                            .UnityToRoundGamePosition()
                            .TransformByDirection(Core.Direction.Backward).magnitude;

                        if (distance > 1 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >=
                            EndRunningNormalizedTime)
                        {
                            animator.Play(StateName, -1, StartRunningNormalizedTime);
                        }
                        else if (distance <= 1 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime <
                                 EndRunningNormalizedTime)
                        {
                            FinishRunning();
                        }
                    });
            }
            else
            {
                FinishRunning();
            }

            void FinishRunning()
            {
                ShowBodyEffect(AttackEffectItem.SpecialAttackEffectItem1, ChargeEffectDuration / 2f);
                animator.Play(StateName, -1, BeforeAttackNormalizedTime);
                SetEventNormalizedTime(BeforeEventAttackNormalizedTime);
                victim.SetEventNormalizedTime(BeforeEventAttackNormalizedTime);
            }
        }

        internal override void OnAttack1()
        {
            ShowFocusLine(false, 1);
            ShakeCamera();
            PlayAudioClip(HardPunchClip);

            CharacterAnimator.ShowTextEffect(HitTextEffectType.JP05, 1);
            ShowDirectedEffect(AttackEffectItem.AttackEffectItem1, 1f);
        }

        internal override void OnDamaged()
        {
            //
        }

        internal override void OnGetUp()
        {
            //
        }
    }
}