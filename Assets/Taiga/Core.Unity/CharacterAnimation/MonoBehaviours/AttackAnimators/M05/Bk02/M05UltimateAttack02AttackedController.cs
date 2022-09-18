using DG.Tweening;
using Taiga.Core.Unity.Camera;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation
{
    public class M05UltimateAttack02AttackedController : BaseM05UltimateAttack02
    {
        private const float _DistanceScale = 0.3f;
        private const float _EffectMoveX = -0.2f;

        public override CharacterAnimator CharacterAnimator { get; set; }
        public Vector2Int? AttackerPosition { get; internal set; }
        public MapDirection MapDirection { get; set; }
        public AudioClip HitClip { get; internal set; }

        private Vector3 _originalPosition;
        private Vector2Int _attackerPositionOffset;
        private Vector3 _originalEffectPosition;
        private Sequence _sequence;
        private Animation _attackingAnimation;
        private AudioSource _audioSource;
        private GameObject _effect;
        private bool _isFlip => GameCamera.Instance.Angle == 135 && MapDirection == MapDirection.East;

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

        internal void ShakeCharacter()
        {
            CharacterAnimator.body.DOShakePosition(1f, new Vector3(0.07f, 0, 0.07f), 50, 20, false, false);
        }

        internal void ShowHitEffect(float duration)
        {
            _effect.SetActive(true);
            DOTween.Sequence()
                .AppendInterval(duration)
                .AppendCallback(() => _effect.SetActive(false));
        }

        internal void HideEffects()
        {
            _effect.SetActive(false);
            _effect.transform.localPosition = _originalEffectPosition;
            CharacterAnimator.effect5.localPosition = _originalEffect5Position;
            CharacterAnimator.effect6.localPosition = _originalEffect6Position;
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
            var root = CharacterAnimator.root;

            _originalPosition = root.localPosition;
            _originalEffect5Position = CharacterAnimator.effect5.localPosition;
            _originalEffect6Position = CharacterAnimator.effect6.localPosition;

            var direction = (AttackerPosition.Value.GameToUnityPosition() - _originalPosition) * _DistanceScale;
            var movePos = _originalPosition + direction;

            _attackerPositionOffset = (_originalPosition.UnityToRoundGamePosition() - AttackerPosition.Value).NormalizeByDirection(MapDirection.GetNormalizeDirection());

            DOTween.Sequence()
                .AppendInterval(0.6f)
                .AppendCallback(() => CharacterAnimator.root.DOLocalMove(movePos, 0.4f));

            SetupHitEffect();

            _sequence = DOTween
                .Sequence()
                .AppendInterval(6f)
                .OnKill(() =>
                {
                    Debug.Log(">>killed<<");
                });

            var ultimateAttackController = CharacterAnimator.ultimateAttackController.gameObject;
            _attackingAnimation = ultimateAttackController.AddComponent<Animation>();
            _audioSource = ultimateAttackController.AddComponent<AudioSource>();

            var animationClip = Instantiate(Animation);

            _attackingAnimation.AddClip(animationClip, "attacking");
            _attackingAnimation.Play("attacking");

            return _sequence;
        }

        private MapDirection SetupFacingDirections(MapDirection attackerDirection)
        {
            Debug.Log($"I am stand in direction {GetStandDirectionFromTarget(_attackerPositionOffset)} from target");
            return CharacterAnimator.Facing;
        }

        private void SetupHitEffect()
        {
            _effect = CharacterAnimator.effect1.gameObject;
            _originalEffectPosition = _effect.transform.localPosition;
            _effect.transform.localPosition = new Vector3(0, 0.5f, 0);
        }

        internal void ShowEffectLeft(float duration)
        {
            CharacterAnimator.effect5.gameObject.SetActive(true);
            CharacterAnimator.effect6.gameObject.SetActive(false);
            CharacterAnimator.effect5.DOShakePosition(0.3f, new Vector3(0.04f, 0.04f, 0.04f), 20, 20, false, false);

            DOTween.Sequence()
                .AppendInterval(duration)
                .AppendCallback(() => CharacterAnimator.effect5.gameObject.SetActive(false));
        }

        internal void ShowEffectRight(float duration)
        {
            CharacterAnimator.effect6.localPosition = Effect6Position;
            CharacterAnimator.effect5.gameObject.SetActive(false);
            CharacterAnimator.effect6.gameObject.SetActive(true);
            CharacterAnimator.effect6.DOShakePosition(0.3f, new Vector3(0.04f, 0.04f, 0.04f), 20, 20, false, false);

            DOTween.Sequence()
                .AppendInterval(duration)
                .AppendCallback(() => CharacterAnimator.effect6.gameObject.SetActive(false));
        }

        private bool IsStandInDirectionFromTarget(Vector2Int _targetOffsetPosition, MapDirection _mapDirection)
        {
            var xDir = _targetOffsetPosition.x == 1 ? MapDirection.East : _targetOffsetPosition.x == -1 ? MapDirection.West : 0b0000;
            var yDir = _targetOffsetPosition.y == 1 ? MapDirection.North : _targetOffsetPosition.y == -1 ? MapDirection.South : 0b0000;
            return _mapDirection == ((byte)xDir) + yDir;
        }

        private MapDirection GetStandDirectionFromTarget(Vector2Int _targetOffsetPosition)
        {
            var xDir = _targetOffsetPosition.x == 1 ? MapDirection.East : _targetOffsetPosition.x == -1 ? MapDirection.West : 0b0000;
            var yDir = _targetOffsetPosition.y == 1 ? MapDirection.North : _targetOffsetPosition.y == -1 ? MapDirection.South : 0b0000;
            return ((byte)xDir) + yDir;
        }

        private void Damage()
        {
            MaskCharacter(false);
            CharacterAnimator.animator.SetTrigger("damaged");
            ShakeCharacter();
            ShowHitEffect(1);
            ShowTextEffect(GetStandDirectionFromTarget(_attackerPositionOffset));
            _audioSource.PlayOneShot(HitClip);
            // GameCamera.Instance.Camera.transform.DOShakeRotation(0.6f, 1.5f, 40);
        }

        private void ShowTextEffect(MapDirection mapDirection)
        {
            switch (mapDirection)
            {
                case MapDirection.North:
                case MapDirection.East:
                case ((byte)MapDirection.North) + MapDirection.East:
                case ((byte)MapDirection.South) + MapDirection.East:
                    ShowEffectLeft(1);
                    break;
                case MapDirection.South:
                case MapDirection.West:
                case ((byte)MapDirection.North) + MapDirection.West:
                case ((byte)MapDirection.South) + MapDirection.West:
                    ShowEffectRight(1);
                    break;
                default:
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

            Destroy(_audioSource);
            Destroy(_attackingAnimation);
            Destroy(this);
        }

        internal override void OnPrepare()
        {

        }

        internal override void OnAttackNW()
        {
            if (!_isFlip && IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.NorthWest))
            {
                Debug.Log("OnAttackNW");
                Damage();
            }
            else if (_isFlip && IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.NorthEast))
            {
                Debug.Log("Flip OnAttackNE");
                Damage();
            }
        }

        internal override void OnAttackN()
        {
            if (IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.North))
            {
                Debug.Log("OnAttackN");
                Damage();
            }
        }

        internal override void OnAttackNE()
        {
            if (!_isFlip && IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.NorthEast))
            {
                Debug.Log("OnAttackNE");
                Damage();
            }
            else if (_isFlip && IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.NorthWest))
            {
                Debug.Log("Flip OnAttackNW");
                Damage();
            }
        }

        internal override void OnAttackE()
        {
            if (!_isFlip && IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.East))
            {
                Debug.Log("OnAttackE");
                Damage();
            }
            else if (_isFlip && IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.West))
            {
                Debug.Log("Flip OnAttackW");
                Damage();
            }
        }

        internal override void OnAttackSE()
        {
            if (!_isFlip && IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.SouthEast))
            {
                Debug.Log("OnAttackSE");
                Damage();
            }
            else if (_isFlip && IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.SouthWest))
            {
                Debug.Log("Flip OnAttackSW");
                Damage();
            }
        }

        internal override void OnAttackS()
        {
            if (IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.South))
            {
                Debug.Log("OnAttackS");
                Damage();
            }
        }

        internal override void OnAttackSW()
        {
            if (!_isFlip && IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.SouthWest))
            {
                Debug.Log("OnAttackSW");
                Damage();
            }
            else if (_isFlip && IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.SouthEast))
            {
                Debug.Log("Flip OnAttackSE");
                Damage();
            }
        }

        internal override void OnAttackW()
        {
            if (!_isFlip && IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.West))
            {
                Debug.Log("OnAttackW");
                Damage();
            }
            else if (_isFlip && IsStandInDirectionFromTarget(_attackerPositionOffset, MapDirection.East))
            {
                Debug.Log("Flip OnAttackE");
                Damage();
            }
        }

        internal override void OnDown()
        {

        }

    }
}
