using UnityEngine;

namespace Taiga.Core.Unity.Camera
{

    [RequireComponent(typeof(CharacterAnimation.CharacterAnimator))]
    public class CharacterCameraFacer : MonoBehaviour
    {

        static int FacingParameter = Animator.StringToHash("facing");

        GameCamera gameCamera;
        CharacterAnimation.CharacterAnimator characterAnimation;

        private void Awake()
        {
            gameCamera = GameCamera.Instance;
            characterAnimation = GetComponent<CharacterAnimation.CharacterAnimator>();
        }

        void Update()
        {
            UpdateCharacterAnimation();
        }

        void UpdateCharacterAnimation()
        {
            var facingAngle = characterAnimation.Facing.GetAngle();
            var cameraAngle = gameCamera.Angle;
            var characterAnimator = characterAnimation.animator;
            var characterRenderer = characterAnimation.bodyRenderer;


            var angle = (cameraAngle - facingAngle) % 360;
            if (angle < 0)
            {
                angle += 360;
                angle %= 360;
            }

            if (angle >= 90 && angle <= 270)
            {
                characterAnimator.SetFloat(FacingParameter, 1);
            }
            else
            {
                characterAnimator.SetFloat(FacingParameter, -1);
            }

            if (angle < 90)
            {
                characterRenderer.flipX = true;
            }
            else if (angle < 180)
            {
                characterRenderer.flipX = false;
            }
            else if (angle < 270)
            {
                characterRenderer.flipX = true;
            }
            else
            {
                characterRenderer.flipX = false;
            }
        }
    }
}
