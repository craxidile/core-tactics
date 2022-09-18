using Entitas;
using UnityEngine;

namespace Taiga.Core.Unity.Camera
{
    internal class RotateCamera_WhenArrowInput : IExecuteSystem
    {
        public GameContext game;

        public RotateCamera_WhenArrowInput(Contexts contexts)
        {
            this.game = contexts.game;
        }

        public void Execute()
        {

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Rotate(-90);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Rotate(90);
            }
        }

        void Rotate(float deltaAngle)
        {
            var gameCamera = game.cameraEntity
                .AsGameObject()
                .GetComponent<GameCamera>();

            var currentAngle = gameCamera.Angle;
            gameCamera.SetAngle(currentAngle + deltaAngle);
        }
    }

}
