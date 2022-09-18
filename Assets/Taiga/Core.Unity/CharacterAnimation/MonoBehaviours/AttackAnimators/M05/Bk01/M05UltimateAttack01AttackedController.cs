using DG.Tweening;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.Bk01
{
    public class M05UltimateAttack01AttackedController : BaseM05UltimateAttack01
    {
        private const float _DistanceScale = 0.2f;
        private const float _EffectMoveX = -0.2f;

        public override CharacterAnimator CharacterAnimator { get; set; }
        public Vector2Int? AttackerPosition { get; internal set; }
        public MapDirection MapDirection { get; set; }

        private Vector3 _originalPosition;
        private Sequence _sequence;
        private Animation _attackingAnimation;

        private GameObject _effect;
        private bool _isAttackerStandRight;
        private Vector3 _originalEffect5Position;
        private Vector3 _originalEffect6Position;

        private Vector3 Effect6Position
        {
            get
            {
                return new Vector3(
                    _originalEffect6Position.x + _EffectMoveX,
                    _originalEffect6Position.y,
                    _originalEffect6Position.z
                );
            }
        }

        internal override Sequence Initialize()
        {
            _originalEffect5Position = CharacterAnimator.effect5.localPosition;
            _originalEffect6Position = CharacterAnimator.effect6.localPosition;

            CharacterAnimator.Facing = MapDirection.GetOppsite();

            var root = CharacterAnimator.root;

            _originalPosition = root.localPosition;
            var direction = (AttackerPosition.Value.GameToUnityPosition() - _originalPosition) * _DistanceScale;
            var movePos = _originalPosition + direction;

            DOTween.Sequence()
                .AppendInterval(0.6f)
                .AppendCallback(() => CharacterAnimator.root.DOLocalMove(movePos, 0.4f));

            SetupHitEffect(MapDirection);

            _sequence = DOTween
                .Sequence()
                .AppendInterval(6f)
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

        private void SetupHitEffect(MapDirection direction)
        {
            var attackPositionOffset = (_originalPosition.UnityToRoundGamePosition() - AttackerPosition.Value).NormalizeByDirection(direction.GetNormalizeDirection());
            _isAttackerStandRight = attackPositionOffset.x == 1;
            switch (direction)
            {
                case MapDirection.East:
                    _effect = attackPositionOffset.x == 1 ? CharacterAnimator.effect2.gameObject : CharacterAnimator.effect1.gameObject;
                    break;
                case MapDirection.West:
                    _effect = attackPositionOffset.x == 1 ? CharacterAnimator.effect1.gameObject : CharacterAnimator.effect2.gameObject;
                    break;
                case MapDirection.North:
                    _effect = attackPositionOffset.x == 1 ? CharacterAnimator.effect4.gameObject : CharacterAnimator.effect3.gameObject;
                    break;
                case MapDirection.South:
                    _effect = attackPositionOffset.x == 1 ? CharacterAnimator.effect3.gameObject : CharacterAnimator.effect4.gameObject;
                    break;
            }
        }

        internal override void OnReset()
        {
            _sequence.Kill();
            HideEffects();

            var animator = CharacterAnimator.animator;

            DOTween
                .Sequence()
                .AppendCallback(() =>
                {
                    CharacterAnimator.animator.SetTrigger($"stand");
                    CharacterAnimator.root.DOLocalMove(_originalPosition, 0.2f);
                })
                .Play();

            Destroy(_attackingAnimation);
            Destroy(this);
        }

        internal override void OnPrepare()
        {

        }

        internal override void OnAttack1()
        {
            MaskCharacter();
            CharacterAnimator.animator.SetTrigger($"damaged");
            ShakeCharacter();
            ShowHitEffect(1);

            if (_isAttackerStandRight)
                ShowEffectLeft(1);
            else
                ShowEffectRight(1);
        }

        internal override void OnDown()
        {
            CharacterAnimator.animator.Play("SitTrigger", -1, 0);
        }

        private void ShakeCharacter()
        {
            CharacterAnimator.body.DOShakePosition(0.5f, new Vector3(0.15f, 0, 0.15f), 50, 20, false, false);
        }

        private void ShowEffectLeft(float duration)
        {
            CharacterAnimator.effect5.gameObject.SetActive(true);
            CharacterAnimator.effect6.gameObject.SetActive(false);
            CharacterAnimator.effect5.DOShakePosition(0.3f, new Vector3(0.04f, 0.04f, 0.04f), 20, 20, false, false);

            DOTween.Sequence()
                .AppendInterval(duration)
                .AppendCallback(() => CharacterAnimator.effect5.gameObject.SetActive(false));
        }

        private void ShowEffectRight(float duration)
        {
            CharacterAnimator.effect6.localPosition = Effect6Position;
            CharacterAnimator.effect5.gameObject.SetActive(false);
            CharacterAnimator.effect6.gameObject.SetActive(true);
            CharacterAnimator.effect6.DOShakePosition(0.3f, new Vector3(0.04f, 0.04f, 0.04f), 20, 20, false, false);

            DOTween.Sequence()
                .AppendInterval(duration)
                .AppendCallback(() => CharacterAnimator.effect6.gameObject.SetActive(false));
        }

        private void ShowHitEffect(float duration)
        {
            _effect.SetActive(true);
            DOTween.Sequence()
                .AppendInterval(duration)
                .AppendCallback(() => _effect.SetActive(false));
        }

        private void HideEffects()
        {
            _effect.SetActive(false);

            CharacterAnimator.effect5.localPosition = _originalEffect5Position;
            CharacterAnimator.effect6.localPosition = _originalEffect6Position;
            CharacterAnimator.effect5.gameObject.SetActive(false);
            CharacterAnimator.effect6.gameObject.SetActive(false);
        }

        private void MaskCharacter(bool shortDelay = true)
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
    }
}
