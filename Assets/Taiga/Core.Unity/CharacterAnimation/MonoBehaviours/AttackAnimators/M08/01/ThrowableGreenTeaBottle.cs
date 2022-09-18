using System;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterAnimation.M08._01
{
    public class ThrowableGreenTeaBottle
    {
        private static ThrowableGreenTeaBottle _instance;

        public Vector3 OriginPosition { get; set; }
        public Vector3 DestinationPosition { get; set; }
        public float ThrowDelay { get; set; }
        public Action OnHit { get; set; }
        public Action OnHitFinish { get; set; }

        public static ThrowableGreenTeaBottle Instance => _instance ??= new ThrowableGreenTeaBottle();

        public void Hit(float delay)
        {
            OnHit?.Invoke();
        }

        public void FinishHit()
        {
            OnHitFinish?.Invoke();
        }
        
    }
}