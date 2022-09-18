using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using Taiga.Core.Character.Attack;
using Taiga.Core.Unity.Character;
using Taiga.Core.Unity.CharacterAnimation.Effect;
using Taiga.Core.Unity.Preset;
using Taiga.Utils;
using UnityEditor.Animations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Taiga.Core.Unity.CharacterAnimation.Base.Effect
{
    public class EffectController
    {
        private const float EffectMoveX = -0.2f;

        private readonly Vector3 _defaultDirectedEffectPosition = new Vector3(0, 0.5f, -0.3f);
        private readonly Vector3 _defaultDirectedEffectScale = new Vector3(1, 1, 1);
        private readonly Vector3 _defaultBodyEffectPosition = new Vector3(0, 0, 0);
        private readonly Vector3 _defaultBodyEffectScale = new Vector3(1, 1, 1);

        private BaseCharacterTimelineController _characterTimelineController;
        private Dictionary<AttackEffectItem, GameAssetsPreset.SpecialAttackItemTransforms> _effectTransformMap;
        private Dictionary<AttackEffectItem, RuntimeAnimatorController> _effectAnimationClipMap;
        private Vector3 _originalLeftTextEffectPosition;
        private Vector3 _originalRightTextEffectPosition;
        private Sequence _directedAnimationSequence;
        private int _directedAnimationSequenceId;
        private Vector3 _originalDirectedEffectRootPosition;
        private Vector3 _originalDirectedEffectSpritePosition;
        private bool? _isComboLeft;

        private float CameraAngle
        {
            get
            {
                Debug.Log(
                    $">>check_null<< {UnityEngine.Camera.current == null} {UnityEngine.Camera.current?.gameObject == null}");
                var cameraRotation = UnityEngine.Camera.current.gameObject.transform.rotation;
                return cameraRotation.eulerAngles.y;
            }
        }

        private Vector3 LeftTextEffectPosition
        {
            get
            {
                return new Vector3(
                    _originalLeftTextEffectPosition.x + (Math.Abs(CameraAngle - 315) < 0.1f ? EffectMoveX : 0f),
                    _originalLeftTextEffectPosition.y,
                    _originalLeftTextEffectPosition.z
                );
            }
        }

        private Vector3 RightTextEffectPosition
        {
            get
            {
                return new Vector3(
                    _originalRightTextEffectPosition.x + (Math.Abs(CameraAngle - 315) < 0.1f ? EffectMoveX : 0f),
                    _originalRightTextEffectPosition.y,
                    _originalRightTextEffectPosition.z
                );
            }
        }

        private GameObject DirectedEffect
        {
            get
            {
                switch (_characterTimelineController.Direction)
                {
                    case MapDirection.East:
                        return _characterTimelineController.CharacterAnimator.effect3.gameObject;
                    case MapDirection.West:
                        return _characterTimelineController.CharacterAnimator.effect4.gameObject;
                    case MapDirection.North:
                        return _characterTimelineController.CharacterAnimator.effect2.gameObject;
                    case MapDirection.South:
                        return _characterTimelineController.CharacterAnimator.effect1.gameObject;
                    default:
                        return _characterTimelineController.CharacterAnimator.effect1.gameObject;
                }
            }
        }

        public EffectController(BaseCharacterTimelineController characterTimelineController)
        {
            _characterTimelineController = characterTimelineController;
            _effectAnimationClipMap = new Dictionary<AttackEffectItem, RuntimeAnimatorController>();
        }

        internal void InitializeEffects()
        {
            _isComboLeft = null;
            _originalLeftTextEffectPosition = _characterTimelineController.CharacterAnimator.effect5.localPosition;
            _originalRightTextEffectPosition = _characterTimelineController.CharacterAnimator.effect6.localPosition;
        }

        internal void AddEffectAssets(string characterArchitypeId)
        {
            var contexts = Contexts.sharedInstance;

            var attackEffectAssetPreset = contexts.GetProvider<ICharacterAttackEffectAssetPreset>();
            var effectAssetsMap = attackEffectAssetPreset.GetEffectAssetsMap();

            // Get effect item transform map and keep in the object
            var characterAnimationPreset = contexts.GetProvider<ICharacterAnimatorPreset>();
            var specialAttackAssetMap = characterAnimationPreset.GetSpecialAttackAssetMap(characterArchitypeId);
            Debug.Log($">>effect_transform_map<<  {characterArchitypeId} {_effectTransformMap}");
            _effectTransformMap = specialAttackAssetMap[_characterTimelineController.AttackType].effectItemTransformMap;

            foreach (var effectKey in _effectTransformMap.Keys)
            {
                var effectDetails = _effectTransformMap[effectKey];
                var effectType = effectDetails.effectType;
                var effectAnimationClip = effectAssetsMap[effectType];
                _effectAnimationClipMap.Add(effectKey, effectAnimationClip);
            }
        }

        internal void ShowBodyEffect(AttackEffectItem attackEffectItem, float delay)
        {
            Debug.Log($">>show_body<<");
            var isFront = _characterTimelineController.CharacterAnimator.animator.GetFloat($"facing") < 0;
            var isFlipped = _characterTimelineController.CharacterAnimator.bodyRenderer.flipX;

            var effect = _characterTimelineController.CharacterAnimator.chargeEffect;
            var effectGameObject = effect.gameObject;

            if (_effectTransformMap.ContainsKey(attackEffectItem))
            {
                var effectTransform = _effectTransformMap[attackEffectItem];
                var position = isFront
                    ? (effectTransform.frontNormalTransform?.localPosition ?? _defaultBodyEffectPosition)
                    : (effectTransform.backNormalTransform?.localPosition ?? _defaultBodyEffectPosition);
                if (isFlipped)
                {
                    position = isFront
                        ? effectTransform.frontFlippedTransform?.localPosition ??
                          new Vector3(-position.x, position.y, position.z)
                        : effectTransform.backFlippedTransform?.localPosition ??
                          new Vector3(-position.x, position.y, position.z);
                }

                Debug.Log($">>wink_position<< {position}");

                var scale = isFront
                    ? (effectTransform.frontNormalTransform?.localScale ?? _defaultBodyEffectScale)
                    : (effectTransform.backNormalTransform?.localScale ?? _defaultBodyEffectScale);
                if (isFlipped)
                {
                    scale = isFront
                        ? effectTransform.frontFlippedTransform?.localScale ?? scale
                        : effectTransform.backFlippedTransform?.localScale ?? scale;
                }

                effectGameObject.transform.localPosition = position;
                effectGameObject.transform.localScale = scale;
            }

            if (_effectTransformMap.ContainsKey(attackEffectItem))
            {
                var effectAnimator = effectGameObject.GetComponent<Animator>();
                effectAnimator.runtimeAnimatorController = _effectAnimationClipMap[attackEffectItem];
            }

            DOTween
                .Sequence()
                .AppendCallback(() => effectGameObject.SetActive(true))
                .AppendInterval(delay)
                .AppendCallback(() =>
                {
                    effectGameObject.SetActive(false);
                    effectGameObject.transform.localPosition = _defaultBodyEffectPosition;
                    effectGameObject.transform.localScale = _defaultBodyEffectScale;
                })
                .Play();
        }

        internal void ShowDirectedEffect(AttackEffectItem attackEffectItem, float delay)
        {
            var currentSequenceId = _directedAnimationSequenceId = (_directedAnimationSequenceId + 1) % int.MaxValue;

            var effectRoot = _characterTimelineController.CharacterAnimator.directedEffectRoot;
            var effectRootGameObject = effectRoot.gameObject;

            var effectSprite = _characterTimelineController.CharacterAnimator.directedEffectSprite;
            var effectSpriteGameObject = effectSprite.gameObject;

            var effectAnimator = effectSpriteGameObject.GetComponent<Animator>();

            if (_directedAnimationSequence != null)
            {
                Debug.Log($">>directed_effect<< kill");
                _directedAnimationSequence = null;
                effectAnimator.runtimeAnimatorController = null;
                effectRootGameObject.transform.localPosition = _originalDirectedEffectRootPosition;
                effectSpriteGameObject.transform.localPosition = _originalDirectedEffectSpritePosition;
            }

            var isFront = _characterTimelineController.CharacterAnimator.animator.GetFloat($"facing") < 0;
            var isFlipped = _characterTimelineController.CharacterAnimator.bodyRenderer.flipX;

            _originalDirectedEffectRootPosition = effectRootGameObject.transform.localPosition;
            _originalDirectedEffectSpritePosition = effectSpriteGameObject.transform.localPosition;

            var position = _defaultDirectedEffectPosition;
            var scale = _defaultDirectedEffectScale;

            if (_effectTransformMap.ContainsKey(attackEffectItem))
            {
                var effectTransform = _effectTransformMap[attackEffectItem];

                position = isFront
                    ? (effectTransform.frontNormalTransform?.localPosition ?? _defaultDirectedEffectPosition)
                    : (effectTransform.backNormalTransform?.localPosition ?? _defaultDirectedEffectPosition);
                Debug.Log($">>front<< {isFront}");
                if (isFlipped)
                {
                    var calcFlippedPosition = new Vector3(-position.x, position.y, position.z);
                    position = isFront
                        ? effectTransform.frontFlippedTransform?.localPosition ?? calcFlippedPosition
                        : effectTransform.backFlippedTransform?.localPosition ?? calcFlippedPosition;
                }

                scale = isFront
                    ? (effectTransform.frontNormalTransform?.localScale ?? _defaultDirectedEffectScale)
                    : (effectTransform.backNormalTransform?.localScale ?? _defaultDirectedEffectScale);
                if (isFlipped)
                {
                    scale = isFront
                        ? effectTransform.frontFlippedTransform?.localScale ?? scale
                        : effectTransform.backFlippedTransform?.localScale ?? scale;
                }
            }

            effectRootGameObject.transform.localScale = scale;

            var spriteX = position.z;
            var spriteY = position.y;
            var spriteZ = position.x;

            var direction = _characterTimelineController.Role == CharacterTimelineAnimatorRole.Attacking
                ? _characterTimelineController.Direction
                : _characterTimelineController.Direction.GetOppsite();
            switch (direction)
            {
                case MapDirection.East:
                    Debug.Log("east");
                    effectRootGameObject.transform.localPosition = new Vector3(-spriteX, spriteY, 0);
                    effectSpriteGameObject.transform.localPosition = new Vector3(0, 0, spriteZ);
                    break;
                case MapDirection.West:
                    Debug.Log("west");
                    effectRootGameObject.transform.localPosition = new Vector3(spriteX, spriteY, 0);
                    effectSpriteGameObject.transform.localPosition = new Vector3(0, 0, -spriteZ);
                    break;
                case MapDirection.North:
                    Debug.Log("north");
                    effectRootGameObject.transform.localPosition = new Vector3(0, spriteY, -spriteX);
                    effectSpriteGameObject.transform.localPosition = new Vector3(-spriteZ, 0, 0);
                    break;
                case MapDirection.South:
                    Debug.Log("south");
                    effectRootGameObject.transform.localPosition = new Vector3(0, spriteY, spriteX);
                    effectSpriteGameObject.transform.localPosition = new Vector3(spriteZ, 0, 0);
                    break;
            }

            SpriteRearranger.Rearrange();

            if (_effectTransformMap.ContainsKey(attackEffectItem))
            {
                effectAnimator.runtimeAnimatorController = _effectAnimationClipMap[attackEffectItem];
            }

            _directedAnimationSequence = DOTween
                .Sequence()
                .AppendCallback(() => effectRootGameObject.SetActive(true))
                .AppendInterval(delay)
                .OnComplete(() =>
                {
                    Debug.Log(
                        $">>directed_effect<< {currentSequenceId} {_directedAnimationSequenceId} killed {currentSequenceId == _directedAnimationSequenceId}");
                    if (currentSequenceId != _directedAnimationSequenceId) return;
                    _directedAnimationSequence = null;
                    effectAnimator.runtimeAnimatorController = null;
                    effectRootGameObject.SetActive(false);
                    effectRootGameObject.transform.localPosition = _originalDirectedEffectRootPosition;
                    effectSpriteGameObject.transform.localPosition = _originalDirectedEffectSpritePosition;
                });
            _directedAnimationSequence.Play();
        }

        internal void ShowLeftTextEffect()
        {
            if (_isComboLeft == null) _isComboLeft = false;
            _characterTimelineController.CharacterAnimator.effect5.localPosition = LeftTextEffectPosition;
            _characterTimelineController.CharacterAnimator.effect5.gameObject.SetActive(true);
            _characterTimelineController.CharacterAnimator.effect6.gameObject.SetActive(false);
            _characterTimelineController.CharacterAnimator.effect5.DOShakePosition(0.3f,
                new Vector3(0.04f, 0.04f, 0.04f), 20, 20, false, false);
            var characterStatus = _characterTimelineController.CharacterAnimator.root.gameObject
                .GetComponent<CharacterStatusPresenter>();
            characterStatus.SetComboText(1, _isComboLeft.Value);
        }

        internal void ShowRightTextEffect()
        {
            if (_isComboLeft == null) _isComboLeft = true;
            _characterTimelineController.CharacterAnimator.effect6.localPosition = RightTextEffectPosition;
            _characterTimelineController.CharacterAnimator.effect5.gameObject.SetActive(false);
            _characterTimelineController.CharacterAnimator.effect6.gameObject.SetActive(true);
            _characterTimelineController.CharacterAnimator.effect6.DOShakePosition(0.3f,
                new Vector3(0.04f, 0.04f, 0.04f), 20, 20, false, false);
            var characterStatus = _characterTimelineController.CharacterAnimator.root.gameObject
                .GetComponent<CharacterStatusPresenter>();
            characterStatus.SetComboText(1, _isComboLeft.Value);
        }

        internal void HideTextEffects()
        {
            _characterTimelineController.CharacterAnimator.effect5.localPosition = _originalLeftTextEffectPosition;
            _characterTimelineController.CharacterAnimator.effect6.localPosition = _originalRightTextEffectPosition;
            _characterTimelineController.CharacterAnimator.effect5.gameObject.SetActive(false);
            _characterTimelineController.CharacterAnimator.effect6.gameObject.SetActive(false);
        }
    }
}