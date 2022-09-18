using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Taiga.Core.Unity
{
    public static class GameUnityPositionExtensions
    {
        public static Vector3 GameToUnityPosition(this Vector2Int position)
        {
            return new Vector3(position.x, 0, position.y);
        }

        public static Vector2 UnityToGamePosition(this Vector3 position)
        {
            return new Vector2(position.x, position.z);
        }

        public static Vector2Int UnityToRoundGamePosition(this Vector3 position)
        {
            return position
                .UnityToGamePosition()
                .Round();
        }

        public static Vector2Int Round(this Vector2 position)
        {
            return new Vector2Int(
                Mathf.RoundToInt(position.x),
                Mathf.RoundToInt(position.y)
            );
        }

        public static MapDirection GetMapDirectionFace(this Vector2Int position, Vector2Int faceTo)
        {
            Assert.IsTrue(position.x == faceTo.x || position.y == faceTo.y);
            if (position.x == faceTo.x)
            {
                return position.y > faceTo.y ? MapDirection.South : MapDirection.North;
            }
            if (position.y == faceTo.y)
            {
                return position.x > faceTo.x ? MapDirection.West : MapDirection.East;
            }

            throw new Exception("One axist must be equal in order to calculate facing direction");
        }

    }


}
