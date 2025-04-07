using System;
using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Model;

namespace SpaceWarsHex.Helpers
{
    /// <inheritdoc />
    public abstract class HexMathCore : IHexMath
    {
        #region Fields

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected readonly float r = 1;
        protected readonly float r2 = 0;
        protected readonly float r3 = 0;
        protected readonly float h1 = 0;
        protected readonly float h2 = 0;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        #endregion // Fields

        /// <summary>
        /// Create the math library with Radius of 1.
        /// </summary>
        public HexMathCore()
            : this(1f)
        {

        }

        /// <summary>
        /// Create the math library witht the given radius.
        /// </summary>
        /// <param name="radius">The radius (center to corner) of one hex.</param>
        /// <param name="epsilon">The epsilon for float calulations. If non-positive defaults to <paramref name="radius"/> * 10e-6.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public HexMathCore(float radius, float epsilon = default)
        {
            if (radius <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(radius));
            }

            Epsilon = epsilon > 0
                ? radius * 1.0e-6F
                : epsilon;

            r = radius;
            r2 = r / 2;
            r3 = r * 1.5f;
            h1 = r2 * (float)Math.Sqrt(3);
            h2 = h1 * 2;
        }

        #region Properties

        /// <summary>
        /// The radius (from center to any vertex) of a unit hexagon.
        /// </summary>
        public float R => r;

        /// <summary>
        /// Half a unit hexagon's height. (from center to middle of any edge)
        /// </summary>
        public float H1 => h1;

        /// <summary>
        /// A unit hexagon's height. (2 * H1)
        /// </summary>
        public float H2 => h2;

        /// <summary>
        /// Half a unit hexagon's radius. (R / 2)
        /// </summary>
        public float R2 => r2;

        /// <summary>
        /// One and one-half a unit hexagon's radius. (1.5 * R)
        /// </summary>
        public float R3 => r3;

        public float Epsilon { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Takes in a square (Euclidian) vector and returns the oblique
        /// hexagonal coordinates containing the vector (v).
        /// </summary>
        /// <param name="x">The x-coordinate of the euclidian vector.</param>
        /// <param name="y">The y-coordinate of the euclidian vector.</param>
        /// <returns>The oblique hexagonal x,y coordinates of the hex conaining v.</returns>
        public HexVector2 RectangularToHexVector2(float x, float y)
        {
            // transform Origin to make calculations easier
            x += r;
            y += h1;

            // Break the xy plane into (r/2 by h sized) rectangles
            // then find which rectangle the (square)vector is in.
            int Ix = Convert.ToInt32(Math.Floor(x / r2));
            int Iy = Convert.ToInt32(Math.Floor(y / h1));

            // Hx is the possible x coordinate for the Hex vector
            // if x is in the part of the hex containing a diagonal line (i.e. Ix % 3 == 0)
            // we must test which side for the diagonal it is on (this is done farther down)
            int Hx = Convert.ToInt32(Math.Floor(Ix / 3d));
            int Hy = 0;

            // Hexes are offset by h (half their height)
            // 
            if (Hx % 2 == 0)
            {
                Hy = Convert.ToInt32(Math.Floor(Iy / 2d));
                float x1 = x - Hx * r3;

                // Test if point is in the rectangle shared by 2 hexes
                // If so we must test which and correct Hx and Hy
                if (Ix % 3 == 0)
                {
                    float y1 = y - Iy * h1;
                    if (Iy % 2 == 0)
                    {
                        if ((-x1 * h1 / r2 + h1) > y1)
                        {
                            Hx--;
                            Hy--;
                        }
                    }
                    else
                    {
                        if ((x1 * h1 / r2) < y1)
                        {
                            Hx--;
                        }
                    }
                }
            }
            else
            {
                Hy = Convert.ToInt32(Math.Floor((Iy - 1) / 2d));
                float x1 = x - Hx * r3;
                if (Ix % 3 == 0)
                {
                    float y1 = y - Iy * h1;
                    if ((Ix - 1) % 2 == 0)
                    {
                        if ((-x1 * h1 / r2 + h1) > y1)
                        {
                            Hx--;
                        }
                    }
                    else
                    {
                        if ((x1 * h1 / r2) < y1)
                        {
                            Hx--;
                            Hy++;
                        }
                    }
                }
            }

            // convert to Oblique corrordinates
            Hy -= (int)Math.Floor(Hx / 2d);
            //Hx += Hy;

            return new HexVector2(Hx, Hy);
        }

        /// <summary>
        /// Returns a copy of the input HexVector rotated clockwise one facing (60⁰)
        /// </summary>
        /// <param name="hv">The vector to rotate</param>
        /// <returns>The new, rotated, vector.</returns>
        public HexVector2 RotateHexVectorCW(HexVector2 hv)
        {
            return hv.RotateCW();
        }

        #endregion
    }
}
