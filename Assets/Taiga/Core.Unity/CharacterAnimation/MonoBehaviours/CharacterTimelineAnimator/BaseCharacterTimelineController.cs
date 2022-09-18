using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using Taiga.Core.Character;
using Taiga.Core.Character.Attack;
using Taiga.Core.Character.Placement;
using Taiga.Core.Unity.CharacterAnimation.Base.Audio;
using Taiga.Core.Unity.CharacterAnimation.Base.Camera;
using Taiga.Core.Unity.CharacterAnimation.Base.CharacterTranslation;
using Taiga.Core.Unity.CharacterAnimation.Base.Effect;
using Taiga.Core.Unity.CharacterAnimation.Base.FocusLine;
using Taiga.Core.Unity.CharacterAnimation.Base.Sprite;
using Taiga.Core.Unity.CharacterAnimation.Base.ThrowableSprite;
using Taiga.Core.Unity.CharacterAnimation.Effect;
using Taiga.Core.Unity.Preset;
using Taiga.Core.Unity.UltimateAttack.Providers;
using Taiga.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Taiga.Core.Unity.CharacterAnimation.Base
{
    public abstract class BaseCharacterTimelineController : MonoBehaviour, ICharacterTimelineController
    {
        protected const string AnimationClipName = "animation";

        internal Vector3 OriginalPosition;
        internal Vector3 MoveTowardDelta;
        internal Animation AttackAnimation;
        internal AudioSource AudioSource;

        public AttackType AttackType { get; set; }
        public string ArchitypeId { get; set; }
        public abstract bool AutoMoveToward { get; }
        public abstract CharacterTimelineAnimatorRole Role { get; }
        public float MoveTowardDelay { get; set; }
        public abstract float MoveTowardAmount { get; }
        public abstract CharacterAnimator CharacterAnimator { get; set; }
        public abstract MapDirection Direction { get; set; }
        public abstract string AttackAnimatorTrigger { get; }
        
        public Vector2Int? AttackerPosition = null;
        public abstract AnimationClip AttackAnimationClip { get; set; }
        public CharacterEntity? CharacterEntity { get; set; }
        public Action<bool> OnBump { get; set; }
        public float AnimationDelay => AttackAnimationClip.length;
        public float AdditionalTimelineDelay { get; set; }

        private EffectController _effectController;
        private AudioController _audioController;
        private ThrowableSpriteController _throwableSpriteController;
        private SpriteController _spriteController;
        private CameraController _cameraController;
        private CharacterTranslationController _characterTranslationController;
        private FocusAnimationController _focusAnimationController;

        // Sub Controllers
        public EffectController EffectController => _effectController ??= new EffectController(this);
        public AudioController AudioController => _audioController ??= new AudioController(this);
        public ThrowableSpriteController ThrowableSpriteController =>
            _throwableSpriteController ??= new ThrowableSpriteController(this);
        public SpriteController SpriteController => _spriteController ??= new SpriteController(this);
        public CameraController CameraController => _cameraController ??= new CameraController(this);

        public FocusAnimationController FocusAnimationController =>
            _focusAnimationController ??= new FocusAnimationController(this);

        public CharacterTranslationController CharacterTranslationController =>
            _characterTranslationController ??= new CharacterTranslationController(this);


        public BaseCharacterTimelineController()
        {
            MoveTowardDelay = 0.15f;
        }

        // Focus Controller Relay Methods
        protected void ShowFocusLine(bool isLightFocus, float duration) =>
            FocusAnimationController.ShowFocusLine(isLightFocus, duration);

        // Camera Controller Relay Methods
        protected void ShakeCamera() => CameraController.ShakeCamera();
        protected void ShakeCameraLight() => CameraController.ShakeCameraLight();
        protected void ZoomOutCamera() => CameraController.ZoomOutCamera();

        // Sprite Controller Relay Methods
        protected void ShakeCharacter() => SpriteController.ShakeCharacter();
        protected void MaskCharacter(bool shortDelay = true) => SpriteController.MaskCharacter(shortDelay);

        protected void ShakeAndMaskCharacter(bool shortDelay = true) =>
            SpriteController.ShakeAndMaskCharacter(shortDelay);

        protected void SetAnimatorTrigger(string trigger) => SpriteController.SetAnimatorTrigger(trigger);

        // Character Translation Controller Relay Methods
        protected void MoveToward(Sequence sequence = null) => CharacterTranslationController.MoveToward(sequence);
        protected void MoveBack(float? delay = null) => CharacterTranslationController.MoveBack(delay);

        // Audio Controller Relay Methods
        private AudioSource CreateAudioSource() => AudioController.CreateAudioSource();

        protected void AddAudioClip(string key, string audioName) =>
            AudioController.AddAudioClip(key, audioName);

        protected void PlayAudioClip(string key) => AudioController.PlayAudioClip(key);
        
        // Throwable Sprite Controller Relay Methods
        protected void AddThrowableSprite(string key, string spriteName) =>
            ThrowableSpriteController.AddThrowableSprite(key, spriteName);
        protected void ThrowSprite(string key, float duration, Vector3 origin, Vector3 destination) =>
            ThrowableSpriteController.ThrowSprite(key, duration, origin, destination);
        protected void ResetThrowableSprite() => ThrowableSpriteController.ResetThrowableSprite();

        // Effect Controller Relay Methods
        private void InitializeEffects() => EffectController.InitializeEffects();

        private void AddEffectAssets(string characterArchitypeId) =>
            EffectController.AddEffectAssets(characterArchitypeId);

        protected void ShowBodyEffect(AttackEffectItem attackEffectItem, float delay) =>
            EffectController.ShowBodyEffect(attackEffectItem, delay);

        protected void ShowDirectedEffect(AttackEffectItem attackEffectItem, float delay) =>
            EffectController.ShowDirectedEffect(attackEffectItem, delay);

        protected void ShowLeftTextEffect() => EffectController.ShowLeftTextEffect();
        protected void ShowRightTextEffect() => EffectController.ShowRightTextEffect();
        protected void HideTextEffects() => EffectController.HideTextEffects();

        // Base class methods
        private Animation CreateAnimation()
        {
            var ultimateAttackController = CharacterAnimator.ultimateAttackController.gameObject;
            if (ultimateAttackController == null) return null;

            var attackingAnimation = ultimateAttackController.AddComponent<Animation>();
            attackingAnimation.playAutomatically = false;
            
            var animationClip = Instantiate(AttackAnimationClip);
            animationClip.legacy = true;
            attackingAnimation.AddClip(animationClip, AnimationClipName);

            return attackingAnimation;
        }

        public virtual Sequence OnStart()
        {
            SpriteRearranger.Rearrange();
            
            InitializeEffects();
            if (Role == CharacterTimelineAnimatorRole.Attacking && ArchitypeId != null) 
                AddEffectAssets(ArchitypeId);
            else if (Role == CharacterTimelineAnimatorRole.Damaged && CharacterEntity != null) 
                AddEffectAssets(CharacterEntity.Value.ArchitypeId);
                
            AudioSource = CreateAudioSource();

            var animator = CharacterAnimator.animator;

            CharacterAnimator.Facing = Role == CharacterTimelineAnimatorRole.Attacking
                ? Direction
                : Direction.GetOppsite();

            AttackAnimation = CreateAnimation();
            if (AttackAnimation == null) return DOTween.Sequence();
            AttackAnimation.Play(AnimationClipName);

            var sequence = DOTween.Sequence();

            if (AutoMoveToward)
            {
                MoveToward(sequence);
            }

            sequence.AppendCallback(() =>
            {
                if (AttackAnimatorTrigger == null) return;
                animator.SetTrigger(AttackAnimatorTrigger);
            });

            if (Role == CharacterTimelineAnimatorRole.Damaged)
            {
                Debug.Log($">>damaged_delay<< {AnimationDelay} {AdditionalTimelineDelay}");
                sequence.AppendInterval(AnimationDelay + AdditionalTimelineDelay);
            }

            return sequence;
        }

        public virtual void OnEnd()
        {
            if (AudioSource != null)
            {
                Destroy(AudioSource);
                AudioSource = null;
            }

            if (AttackAnimation != null)
            {
                Destroy(AttackAnimation);
                AttackAnimation = null;
            }

            Destroy(this);
        }
    }
}