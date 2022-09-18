using UnityEngine;

namespace Taiga.Core
{
    public static class Vector2Extensions
    {

        public static float GetAngle(this Vector2 vector)
        {
            var degree = Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg;
            return degree < 0 ? degree + 360 : degree;
        }
    }
}
