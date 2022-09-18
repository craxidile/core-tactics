using DG.Tweening;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation
{
    public class S17UltimateAttack01AttackedController : BaseS17UltimateAttack01
    {
        private const float MoveTowardAmount = 0.25f;
        private const float EffectMoveX = -0.2f;
        private readonly Vector3 Effect5Translation = new Vector3(0f, 0f, 0f);
        private readonly Vector3 Effect6Translation = new Vector3(0f, 0f, 0f);

        public override CharacterAnimator CharacterAnimator { get; set; }
        public MapDirection MapDirection { get; set; }

        private float CameraAngle
        {
            get
            {

                var cameraRotation = UnityEngine.Camera.current.gameObject.transform.rotation;
                return cameraRotation.eulerAngles.y;
            }
        }

        private Vector3 Effect5Position
        {
            get
            {
                return new Vector3(
                    _originalEffect5Position.x + (CameraAngle == 315 ? EffectMoveX : 0f),
                    _originalEffect5Position.y,
                    _originalEffect5Position.z
                ) + Effect5Translation;
            }
        }

        private Vector3 Effect6Position
        {
            get
            {
                return new Vector3(
                    _originalEffect6Position.x + (CameraAngle == 315 ? EffectMoveX : 0f),
                    _originalEffect6Position.y,
                    _originalEffect6Position.z
                ) + Effect6Translation;
            }
        }

        private float _moveX;
        private float _moveZ;
        private Vector3 _originalPosition;
        private Vector3 _originalEffect5Position;
        private Vector3 _originalEffect6Position;
        private Sequence _sequence;
        private Animation _attackingAnimation;

        internal void ShakeCharacter()
        {
            CharacterAnimator.body.DOShakePosition(0.15f, new Vector3(0.08f, 0, 0.08f), 28, 20, false, false);
        }

        internal void ShowEffect5()
        {
            CharacterAnimator.effect5.localPosition = Effect5Position;
            CharacterAnimator.effect5.gameObject.SetActive(true);
            CharacterAnimator.effect6.gameObject.SetActive(false);
            CharacterAnimator.effect5.DOShakePosition(0.3f, new Vector3(0.04f, 0.04f, 0.04f), 20, 20, false, false);
        }

        internal void ShowEffect6()
        {
            CharacterAnimator.effect6.localPosition = Effect6Position;
            CharacterAnimator.effect5.gameObject.SetActive(false);
            CharacterAnimator.effect6.gameObject.SetActive(true);
            CharacterAnimator.effect6.DOShakePosition(0.3f, new Vector3(0.04f, 0.04f, 0.04f), 20, 20, false, false);
        }

        internal void HideEffects()
        {
            CharacterAnimator.effect5.localPosition = _originalEffect5Position;
            CharacterAnimator.effect6.localPosition = _originalEffect6Position;
            CharacterAnimator.effect5.gameObject.SetActive(false);
            CharacterAnimator.effect6.gameObject.SetActive(false);
        }

        internal void MaskCharacter(bool shortDelay = true)
        {
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(shortDelay ? 0f : 0.18f);
            sequence.AppendCallback(() =>
            {
                CharacterAnimator.bodyRenderer.color = new Color(1f, 0.5f, 0.5f, 1f);
            });
            sequence.AppendInterval(shortDelay ? 0.15f : 0.25f);
            sequence.AppendCallback(() =>
            {
                CharacterAnimator.bodyRenderer.color = Color.white;
            });
            sequence.Play();
        }

        internal override Sequence Initialize()
        {
            _originalEffect5Position = CharacterAnimator.effect5.localPosition;
            _originalEffect6Position = CharacterAnimator.effect6.localPosition;

            CharacterAnimator.Facing = MapDirection.GetOppsite();

            var root = CharacterAnimator.root;

            _originalPosition = root.localPosition;
            switch (CharacterAnimator.Facing)
            {
                case MapDirection.East:
                    _moveX = MoveTowardAmount;
                    break;
                case MapDirection.West:
                    _moveX = -MoveTowardAmount;
                    break;
                case MapDirection.North:
                    _moveZ = MoveTowardAmount;
                    break;
                case MapDirection.South:
                    _moveZ = -MoveTowardAmount;
                    break;
            }

            _sequence = DOTween
                .Sequence()
                .AppendInterval(1.1667f)
                .OnKill(() =>
                {
                    Debug.Log(">>killed<<");
                });

            var ultimateAttackController = CharacterAnimator.ultimateAttackController.gameObject;
            _attackingAnimation = ultimateAttackController.AddComponent<Animation>();
            var animationClip = Instantiate(Animation);
            
            _attackingAnimation.AddClip(animationClip, "attacking");
            _attackingAnimation.Play("attacking");

            return _sequence;
        }
        internal override void OnReset()
        {
            Debug.Log(">>reset<<");
            _sequence.Kill();
            HideEffects();
            
            CharacterAnimator.animator.SetTrigger("guard");

            // var animator = CharacterAnimator.animator;

            // DOTween
                // .Sequence()
                // .AppendInterval(1f)
                // .AppendCallback(() =>
                // {
                //     animator.SetTrigger($"up");
                // })
                // .Play();

            Destroy(_attackingAnimation);
            Destroy(this);
        }

        private Sequence _punchSequence = null;

        internal override void OnPunch1()
        {
            Debug.Log(">>punched<<");
            ShowEffect5();
            CharacterAnimator.animator.SetTrigger("damaged_still");
            MaskCharacter(false);
            CharacterAnimator.root.DOShakePosition(0.2f, new Vector3(0.04f, 0, 0.04f), 28, 0, false,
                false);

            
            // _punchSequence = DOTween.Sequence()
            //         .Append(CharacterAnimator.root.DOShakePosition(0.2f, new Vector3(0.08f, 0, 0.08f), 28, 0))
                    // .AppendInterval(0.4f)
                // .AppendCallback(() =>
                // {
                //     HideEffects();
                // });
                ;
                
            // _punchSequence.Play();
        }

        internal override void OnPunch2()
        {
            Debug.Log($"punch_02");
            OnPunch1();
            // MaskCharacter(false);
            // _punchSequence?.Restart(false);
        }

    }
}
