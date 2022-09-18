using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Taiga.Core.Unity.Camera
{
    
    public enum CameraAttackStatus
    {
        Idle,
        Rotating,
        Ready,
    }

    [Unique]
    public sealed class Camera : IComponent
    {
    }

    public sealed class Camera_AttackStatusChanged : IComponent
    {
        public CameraAttackStatus status;
    }

}
