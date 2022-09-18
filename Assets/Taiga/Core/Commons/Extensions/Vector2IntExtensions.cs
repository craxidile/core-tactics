using UnityEngine;
using UnityEngine.Assertions;

namespace Taiga.Core
{

    public static class Vector2IntExtensions
    {

        public static MapDirection GetMapDirection(this Vector2Int vector)
        {
            Assert.IsTrue(vector.x == 0 || vector.y == 0);
            Assert.IsFalse(vector.x == 0 && vector.y == 0);

            if (vector.x == 0)
            {
                return vector.y > 0 ? MapDirection.North : MapDirection.South;
            }
            else
            {
                return vector.x > 0 ? MapDirection.East : MapDirection.West;
            }
        }

        public static MapDirection GetMapDirectionFromTarget(this Vector2Int vector, Vector2Int targetPosition, MapDirection targetMapDirection)
        {
            var targetOffsetPosition = (vector - targetPosition).NormalizeByDirection(targetMapDirection.GetNormalizeDirection());
            var xDir = targetOffsetPosition.x == 1 ? MapDirection.East : targetOffsetPosition.x == -1 ? MapDirection.West : 0b0000;
            var yDir = targetOffsetPosition.y == 1 ? MapDirection.North : targetOffsetPosition.y == -1 ? MapDirection.South : 0b0000;
            return ((byte)xDir) + yDir;
        }

        public static Vector2 ToVector2(this Vector2Int vectorInt)
        {
            return new Vector2(vectorInt.x, vectorInt.y);
        }

        public static Vector2Int NormalizeByDirection(this Vector2Int offset, Direction direction)
        {
            if (direction == Direction.Left)
            {
                return offset.RotateRight();
            }

            if (direction == Direction.Backward)
            {
                return offset.RotateLeft().RotateLeft();
            }

            if (direction == Direction.Right)
            {
                return offset.RotateLeft();
            }

            return offset;
        }

        public static Vector2Int TransformToMapDirection(this Vector2Int offset, MapDirection direction)
        {
            if (direction == MapDirection.East)
            {
                return offset.RotateLeft();
            }

            if (direction == MapDirection.South)
            {
                return offset.RotateLeft().RotateLeft();
            }

            if (direction == MapDirection.West)
            {
                return offset.RotateRight();
            }

            return offset;
        }

        public static Vector2Int TransformByDirection(this Vector2Int offset, Direction direction)
        {
            if (direction == Direction.Left)
            {
                return offset.RotateLeft();
            }

            if (direction == Direction.Backward)
            {
                return offset.RotateLeft().RotateLeft();
            }

            if (direction == Direction.Right)
            {
                return offset.RotateRight();
            }

            return offset;
        }

        static Vector2Int RotateLeft(this Vector2Int offset)
        {
            return new Vector2Int(offset.y, -offset.x);
        }

        static Vector2Int RotateRight(this Vector2Int offset)
        {
            return new Vector2Int(-offset.y, offset.x);
        }

    }
}
