using System;
using UnityEngine;

namespace Taiga.Core
{

    public enum MapDirection : byte
    {
        North = 0b1000,
        East = 0b0100,
        South = 0b0010,
        West = 0b0001,
        NorthWest = North + West,
        NorthEast = North + East,
        SouthWest = South + West,
        SouthEast = South + East,
    }

    public enum Direction : int
    {
        Forward = 0,
        Right = 1,
        Backward = 2,
        Left = 3
    }

}
