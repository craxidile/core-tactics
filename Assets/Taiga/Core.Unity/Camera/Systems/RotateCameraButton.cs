using Taiga.Core.Unity;
using Taiga.Core.Unity.Camera;
using UnityEngine;

public class RotateCameraButton : MonoBehaviour
{
    private GameCamera gameCamera;

    public void Rotate(float deltaAngle)
    {
        gameCamera = gameCamera ?? Contexts.sharedInstance.game.cameraEntity
            .AsGameObject()
            .GetComponent<GameCamera>();

        var currentAngle = gameCamera.Angle;
        gameCamera.SetAngle(currentAngle + deltaAngle);
    }
}
