using SpaceWarsHex.Model;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace SpaceWarsHex.Helpers
{
    /// <summary>
    /// Helper methods for the <see cref="HexVector2"/>, <see cref="Direction6"/>, and <see cref="Direction12"/>.
    /// </summary>
    public static class HexVectorExtensions
    {

        /// <summary>
        /// The six possible unit vectors.
        /// </summary>
        public static HexVector2[] Directions = new HexVector2[]
        {
            new ( 1,  0),
            new ( 1, -1),
            new ( 0, -1),
            new (-1,  0),
            new (-1,  1),
            new ( 0,  1),
        };

        /// <summary>
        /// Gets a unit-length <see cref="HexVector2"/> that corresponds
        /// the the given <see cref="Direction6"/>, assumes face-oriented.
        /// </summary>
        /// <param name="direction"><see cref="Direction6"/></param>
        /// <returns>The unit-length <see cref="HexVector2"/></returns>
        /// <exception cref="ArgumentOutOfRangeException">If the given direction is invalid.</exception>
        public static HexVector2 ToUnitHexVector(this Direction6 direction)
        {
            return direction switch
            {
                Direction6.N => Directions[5],
                Direction6.NE => Directions[0],
                Direction6.SE => Directions[1],
                Direction6.S => Directions[2],
                Direction6.SW => Directions[3],
                Direction6.NW => Directions[4],
                _ => throw new ArgumentOutOfRangeException(nameof(direction))
            };
        }

        /// <summary>
        /// Gets the shortest <see cref="HexVector2"/> that corresponds
        /// the the given <see cref="Direction12"/>.
        /// </summary>
        /// <param name="direction"><see cref="Direction12"/></param>
        /// <returns>The <see cref="HexVector2"/></returns>
        /// <remarks>
        /// If the <paramref name="direction"/> input is along a hex face, the resultant vector will be unit-length,
        /// otherwise it will have a length of 2.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">If the given direction is invalid.</exception>
        public static HexVector2 GetShortestVector(this Direction12 direction) => direction switch
        {
            Direction12.D0 => Directions[5],
            Direction12.D30 => new(1, 1),
            Direction12.D60 => Directions[0],
            Direction12.D90 => new(2, -1),
            Direction12.D120 => Directions[1],
            Direction12.D150 => new(1, -2),
            Direction12.D180 => Directions[2],
            Direction12.D210 => new(-1, -1),
            Direction12.D240 => Directions[3],
            Direction12.D270 => new(-2, -1),
            Direction12.D300 => Directions[4],
            Direction12.D330 => new(-1, 2),
            _ => throw new ArgumentOutOfRangeException(nameof(direction))
        };

        /// <summary>
        /// Trys to get get the corresponding <see cref="Direction6"/> from a <see cref="Direction12"/>
        /// </summary>
        /// <param name="direction12"></param>
        /// <param name="direction6"></param>
        /// <returns>True if the <paramref name="direction12"/> has a corresponding <see cref="Direction6"/></returns>
        public static bool TryGetDirection6(this Direction12 direction12, out Direction6 direction6)
        {
            switch (direction12)
            {
                case Direction12.D0:
                    direction6 = Direction6.N;
                    return true;
                case Direction12.D60:
                    direction6 = Direction6.NE;
                    return true;
                case Direction12.D120:
                    direction6 = Direction6.SE;
                    return true;
                case Direction12.D180:
                    direction6 = Direction6.S;
                    return true;
                case Direction12.D240:
                    direction6 = Direction6.SW;
                    return true;
                case Direction12.D270:
                    direction6 = Direction6.NW;
                    return true;
                default:
                    direction6 = default;
                    return false;
            }
        }

        /// <summary>
        /// Rotates a <see cref="Direction12"/> by another
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public static Direction12 Rotate(this Direction12 direction, Direction12 rotation)
        {
            return (Direction12)((int)direction + (int)rotation % 12);
        }

        public static Direction12 Rotate(this Direction12 direction, Direction6 rotation)
        {
            return (Direction12)((int)direction + 2 * (int)rotation % 12);
        }

        /// <summary>
        /// Gets one or more vectors that correspond to a given
        /// direction and distance.
        /// </summary>
        /// <param name="direction"><see cref="Direction12"/></param>
        /// <param name="distance">The desired distance, in hexes.</param>
        /// <param name="strict">If true, will treat <paramref name="direction"/> as an exact angle,
        /// otherwise treats it as a range of angles (a 30-degree arc).</param>
        /// <returns>The collection of one or more hexes that satisfy the conditions.</returns>
        /// <remarks>
        /// If <paramref name="strict"/> is true, the only time the result will contain more than one hex is when
        /// <paramref name="distance"/> is an odd number <em>and</em> <paramref name="direction"/> is corner/vertex facing
        /// (i.e. D30, D90, D150, etc.).
        /// This is because with a vertex-facing direction, odd numbered distances will land in the middle of the edge
        /// and there are two possible hexes that are the same distance.
        /// </remarks>
        public static ICollection<HexVector2> GetVectors(this Direction12 direction, int distance, bool strict = true)
        {
            var result = new List<HexVector2>();

            if (strict)
            {
                var v = GetShortestVector(direction);
                if (v.Length() == 1)
                {
                    result.Add(v * distance);
                }
                else
                {
                    if (distance % 2 == 0)
                    {
                        result.Add(v * (distance / 2));
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                // TODO
                throw new NotImplementedException();
            }

            return result;
        }
    }
}
