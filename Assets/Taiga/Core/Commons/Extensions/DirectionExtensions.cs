using System;
using UnityEngine;

namespace Taiga.Core
{

    public static class DirectionExtensions
    {

        public static Direction TurnCountToDirection(this int turn)
        {
            turn %= 4;
            if (turn < 0)
            {
                turn += 4;
            }

            return (Direction)turn;
        }

        public static Direction GetNearestDirection(this float angle)
        {
            angle %= 360;
            if (angle < 0)
            {
                angle += 360;
            }

            if (angle <= 45)
            {
                return Direction.Forward;
            }
            else if (angle <= 135)
            {
                return Direction.Right;
            }
            else if (angle <= 225)
            {
                return Direction.Backward;
            }
            else if (angle <= 315)
            {
                return Direction.Left;
            }
            return Direction.Forward;
        }

    }
}
