using DG.Tweening;
using Taiga.Core.Unity.Camera;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.Base.Camera
{
    public class CameraController
    {
        private BaseCharacterTimelineController _characterTimelineController;

        public CameraController(BaseCharacterTimelineController characterTimelineController)
        {
            _characterTimelineController = characterTimelineController;
        }

        internal void ShakeCamera()
        {
            UnityEngine.Camera.current.gameObject.transform.DOShakeRotation(1f, 1.5f, 40);
        }
        internal void ShakeCameraLight()
        {
            UnityEngine.Camera.current.gameObject.transform.DOShakeRotation(0.5f, 0.5f, 15);
        }
        internal void ZoomOutCamera()
        {
            var gameCamera = Object.FindObjectOfType<GameCamera>();
            gameCamera.ZoomOutAndUndim();
        }
        
    }
}