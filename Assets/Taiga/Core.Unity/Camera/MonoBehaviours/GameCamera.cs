using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DG.Tweening;
using Taiga.Core.Character;
using Taiga.Core.Character.Placement;
using Taiga.Utils;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace Taiga.Core.Unity.Camera
{

    public enum GameCameraMode
    {
        Normal,
        Zoom
    }
    
    public delegate void ZoomInAndDimCallback();

    public class GameCamera : MonoBehaviour
    {
        public static GameCamera Instance { get; private set; }

        [SerializeField] Transform root;

        [SerializeField] UnityEngine.Camera currentCamera;

        public UnityEngine.Camera Camera => currentCamera;

        public float Angle => root.eulerAngles.y;

        private bool _zoomedIn;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            UpdateFocusPosition();
        }

        private float fps = 0f;
        private void OnGUI()
        {
            var newFPS = 1.0f / Time.smoothDeltaTime;
            fps = Mathf.Lerp(fps, newFPS, 0.0005f);
            // GUI.Label(new Rect(0, 0, 100, 100), "FPS: " + ((int)fps).ToString());
        }

        Tween tween;
        public Tween SetAngle(float angle, bool withAnimation = true, float delay = 0.3f)
        {
            if (tween != null && tween.IsActive())
            {
                return null;
            }

            angle %= 360;
            var rotation = root.eulerAngles;
            rotation.y = angle;

            if (withAnimation)
            {
                tween = DOTween.Sequence()
                    .Append(root.DORotate(rotation, delay))
                    // .AppendInterval(0.1f)
                    .AppendCallback(() =>
                    {
                        // Debug.Log(">>rearrange<<");
                        SpriteRearranger.Rearrange();
                    });
            }
            else
            {
                tween = DOTween.Sequence();
                root.eulerAngles = rotation;
            }
            return tween;
        }

        public void SetMode(GameCameraMode mode, bool withAnimation = true)
        {
            //TODO use on attack cut scene
        }

        int? focusCharacterId;
        GameObject focusGameObject;

        public void FocusOnCharacter(int characterId, bool withAnimation = true)
        {
            focusCharacterId = characterId;
            focusGameObject = null;
        }

        public void FocusOnCharacterWithZoom(int characterId, bool withAnimation = true, float zoom = 1)
        {
            focusCharacterId = characterId;
            focusGameObject = null;
        }

        public void ZoomInAndDim(int? cameraAngle = 315, ZoomInAndDimCallback callback = null)
        {
            if (_zoomedIn)
            {
                return;
            }
            // SetAngle(315, true, 0.6f);
            // tween.WaitForKill();

            _zoomedIn = true;
            var light = FindObjectOfType<Light>();

            // var sequence = DOTween.Sequence();

            // if (cameraAngle != null)
            // {
            //     sequence = sequence.Append(SetAngle(cameraAngle.Value, true, 0.6f))
            //         .AppendInterval(0.1f)
            //         .AppendCallback(() =>
            //         {
            //             Debug.Log(">>rearrange<<");
            //             RearrangeSprites();
            //         });
            // }
            // else
            // {
            //     RearrangeSprites();
            // }

            // .AppendInterval(0.1f)
            // .AppendCallback(() =>
            // {
            //     Debug.Log(">>rearrange<<");
            //     RearrangeSprites();
            // });

            Sequence sequence;
            if (cameraAngle != null)
            {
                sequence = SetAngle(cameraAngle.Value, true, 0.6f) as Sequence;
            }
            else
            {
                sequence = DOTween.Sequence();
            }

            sequence
                .AppendCallback(() =>
                {
                    // DOTween.To(
                    //     () => light.intensity,
                    //     x => { light.intensity = x; },
                    //     .5f,
                    //     .2f
                    // ).SetEase(Ease.InCubic);

                    DOTween.To(
                        () => currentCamera.orthographicSize,
                        x => { currentCamera.orthographicSize = x; },
                        1.7f,
                        .2f
                    ).SetEase(Ease.InCubic);
                })
                .AppendCallback(() =>
                {
                    if (callback == null) return;
                    callback();
                })
                .Play();
        }


        public void ZoomOutAndUndim()
        {
            if (!_zoomedIn)
            {
                return;
            }
            _zoomedIn = false;
            var light = FindObjectOfType<Light>();
            // TODO: Separate this
            // DOTween.To(
            //     () => light.intensity,
            //     x => { light.intensity = x; },
            //     1f,
            //     .1f
            // ).SetEase(Ease.InCubic);
            DOTween.To(
                () => currentCamera.orthographicSize,
                x => { currentCamera.orthographicSize = x; },
                3f,
                .2f
            ).SetEase(Ease.InCubic);
        }

        public void Unfocus()
        {
            focusGameObject = null;
        }

        private void UpdateFocusPosition()
        {
            if (focusGameObject == null && focusCharacterId != null)
            {
                var game = Contexts.sharedInstance.game;
                var characterGameObject = game.AsCharacterContext()
                    .GetCharacter(focusCharacterId.Value)
                    .AsGameObject();
                focusGameObject = characterGameObject;
            }

            if (focusGameObject != null)
            {
                var firstPosition = root.transform.position;
                var lastPosition = focusGameObject.transform.position;
                root.transform.position = firstPosition + (lastPosition - firstPosition) / 3f;
            }
        }

    }
}
