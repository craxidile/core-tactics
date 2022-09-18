using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Taiga.Core
{
    public static class MapDirectionExtensions
    {

        public static MapDirection Shift(this MapDirection direction, int count)
        {
            count %= 4;
            var directionByte = (byte)direction;
            if (count < 0)
            {
                directionByte <<= count;
            }
            else
            {
                directionByte <<= 4;
                directionByte >>= count;
            }
            return (MapDirection)((directionByte >> 4) | directionByte & 0b1111);
        }

        public static IEnumerable<MapDirection> GetMapDirectionsFromMask(byte mask)
        {
            return Enum
                .GetValues(typeof(MapDirection))
                .Cast<MapDirection>()
                .Where(direction => (mask & (byte)direction) != 0);
        }

        public static MapDirection Transform(this MapDirection mapDirection, Direction direction)
        {
            return mapDirection.Shift((int)direction);
        }

        public static MapDirection GetOppsite(this MapDirection direction)
        {
            return direction.Transform(Direction.Backward);
        }

        public static Direction GetNormalizeDirection(this MapDirection direction)
        {
            var turn = 0;
            while (direction != MapDirection.North)
            {
                direction = direction.Shift(1);
                turn++;
            }
            return turn.TurnCountToDirection();
        }

        public static float GetAngle(this MapDirection direction)
        {
            if (direction == MapDirection.North)
            {
                return 0;
            }

            if (direction == MapDirection.South)
            {
                return 180;
            }

            if (direction == MapDirection.East)
            {
                return 90;
            }

            return 270;
        }

        public static MapDirection GetFineMapDirection(this float angle)
        {
            angle %= 360;
            if (angle < 0)
            {
                angle += 360;
            }

            if (angle > 337.5f || angle <= 22.5f)
            {
                return MapDirection.North;
            }
            else if (angle > 22.5f && angle <= 67.5f)
            {
                return MapDirection.NorthEast;
            }
            else if (angle > 67.5 && angle <= 112.5f)
            {
                return MapDirection.East;
            }
            else if (angle > 112.5f && angle <= 157.5f)
            {
                return MapDirection.SouthEast;
            }
            else if (angle > 157.5f && angle <= 202.5f)
            {
                return MapDirection.South;
            }
            else if (angle > 202.5f && angle <= 247.5f)
            {
                return MapDirection.SouthWest;
            }
            else if (angle > 247.5f && angle <= 292.5f)
            {
                return MapDirection.West;
            }

            return MapDirection.NorthWest;
        }

        public static MapDirection GetNearestMapDirection(this float angle)
        {
            angle %= 360;
            if (angle < 0)
            {
                angle += 360;
            }

            if (angle <= 45)
            {
                return MapDirection.North;
            }
            else if (angle <= 135)
            {
                return MapDirection.East;
            }
            else if (angle <= 225)
            {
                return MapDirection.South;
            }
            else if (angle <= 315)
            {
                return MapDirection.West;
            }

            return MapDirection.North;
        }


        public static Vector2Int GetUnitVector(this MapDirection direction)
        {
            if (direction == MapDirection.North)
            {
                return Vector2Int.up;
            }

            if (direction == MapDirection.West)
            {
                return Vector2Int.left;
            }

            if (direction == MapDirection.South)
            {
                return Vector2Int.down;
            }

            if (direction == MapDirection.East)
            {
                return Vector2Int.right;
            }

            if (direction == MapDirection.NorthEast)
            {
                return Vector2Int.up + Vector2Int.right;
            }

            if (direction == MapDirection.NorthWest)
            {
                return Vector2Int.up + Vector2Int.left;
            }

            if (direction == MapDirection.SouthEast)
            {
                return Vector2Int.down + Vector2Int.right;
            }

            return Vector2Int.down + Vector2Int.left;
        }
    }
}
