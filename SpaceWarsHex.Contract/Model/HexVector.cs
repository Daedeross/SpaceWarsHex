using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SpaceWarsHex.Model
{
    /// <summary>
    /// Struct for representing vectors and coordinates on a plane of discrete hexagons.
    /// </summary>
    [Serializable]
    [DataContract]
    public readonly struct HexVector2
    {
        #region Fields
        /// <summary>
        /// The x-component of the hex/hex-vector
        /// </summary>
        [DataMember]
        public readonly int X;

        /// <summary>
        /// The y-component of the hex/hex-vector
        /// </summary>
        [DataMember]
        public readonly int Y;

        #endregion // Fields

        #region Constructors / Destructors

        /// <summary>
        /// Create a HexVector with the given components.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        [JsonConstructor]
        public HexVector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion // Constructors / Destructors

        #region Public Methods

        /// <summary>
        /// The length of the vector, in hexes
        /// </summary>
        /// <returns>The integer length.</returns>
        public int Length()
        {
            if (X > 0)                  //  Hextant 1, 2 or 3
            {
                if (Y > 0)              // Hextant 1
                {
                    return X + Y;
                }
                else                    // hextant 2 or 3
                {
                    return Math.Max(X, -Y);
                }
            }
            else                        // Hextant 4, 5, or 6
            {
                if (Y < 0)              // Hextant 4
                {
                    return -X - Y;
                }
                else
                {
                    return Math.Max(-X, Y);
                }
            }
        }

        private void RotateCW(ref int x, ref int y)
        {
            int tmp = x;
            x += y;
            y = -tmp;
        }

        private void RotateCCW(ref int x, ref int y)
        {
            int tmp = x;
            x = -y;
            y += tmp;
        }

        /// <summary>
        /// Rorates the vector one face (60 degrees) <b>clockwise.</b>
        /// </summary>
        public HexVector2 RotateCW()
        {
            int x = X;
            int y = Y;
            RotateCW(ref x, ref y);
            return new HexVector2(x, y);
        }

        /// <summary>
        /// Rorates the vector count faces (60*count degrees) <b>clockwise.</b>
        /// </summary>
        public HexVector2 RotateCW(int count)
        {
            if (count < 0)
            {
                return RotateCCW(-count);
            }
            else
            {
                int x = X;
                int y = Y;

                count %= 6;
                while (count > 0)
                {
                    RotateCW(ref x, ref y);
                    count--;
                }
                return new HexVector2(x, y);
            }
        }

        /// <summary>
        /// Rotates the vector one face (60 degrees) <b>counter-clockwise.</b>
        /// </summary>
        public HexVector2 RotateCCW()
        {
            int x = X;
            int y = Y;
            RotateCCW(ref x, ref y);
            return new HexVector2(x, y);
        }

        /// <summary>
        /// Rorates the vector count faces (60*count degrees) <b>counter-clockwise.</b>
        /// </summary>
        public HexVector2 RotateCCW(int count)
        {
            if (count < 0)
            {
                return RotateCW(-count);
            }

            int x = X;
            int y = Y;

            count %= 6;
            while (count > 0)
            {
                RotateCCW(ref x, ref y);
                count--;
            }
            return new HexVector2(x, y);
        }

        /// <summary>
        /// Scales the Hex Vector by a scalar (int).
        /// </summary>
        /// <param name="factor">The amount to scale by.</param>
        /// <returns>A new <see cref="HexVector2"/> with the scaling applied.</returns>
        public HexVector2 Scale(int factor)
        {
            return new HexVector2(X * factor, Y * factor);
        }

        /// <summary>
        /// Scales the Hex Vector by a scalar (float). Result is truncated.
        /// </summary>
        /// <param name="factor">The amount to scale by.</param>
        /// <returns>A new <see cref="HexVector2"/> with the scaling applied.</returns>
        public HexVector2 Scale(float factor)
        {
            return new HexVector2((int)(X * factor), (int)(Y * factor));
        }

        /// <summary>
        /// Divides the components bu <b>factor</b> follwing the rules for integer division.
        /// </summary>
        /// <param name="factor"></param>
        /// <returns>A new <see cref="HexVector2"/> with the scaling applied.</returns>
        public HexVector2 Shrink(int factor)
        {
            return new HexVector2(X / factor, Y / factor);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"(X:{X}, Y:{Y})";
        }

        #endregion // Public Methods

        #region Static Members

        /// <summary>
        /// A zero component hex-vector.
        /// </summary>
        public static HexVector2 Zero
        {
            get { return new HexVector2(0, 0); }
        }

        /// <summary>
        /// A unit vector along the x-axis.
        /// </summary>
        public static HexVector2 UnitX
        {
            get { return new HexVector2(1, 0); }
        }

        /// <summary>
        /// A unit vector along the y-axis.
        /// </summary>
        public static HexVector2 UnitY
        {
            get { return new HexVector2(0, 1); }
        }

        /// <summary>
        /// The maximum (i.e. most positive) possible <see cref="HexVector2"/>
        /// </summary>
        public static HexVector2 Max => new HexVector2(int.MaxValue, int.MaxValue);

        /// <summary>
        /// The minimum (i.e. most negative) possible <see cref="HexVector2"/>
        /// </summary>
        public static HexVector2 Min => new HexVector2(int.MinValue, int.MinValue);

        #endregion // Static Methods

        #region Operator Overloads

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is HexVector2 hv)
            {
                return hv.X == X && hv.Y == Y;
            }
            else return false;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        /// <inheritdoc />
        public static bool operator ==(HexVector2 h1, HexVector2 h2)
        {
            return h1.Equals(h2);
        }

        /// <inheritdoc />
        public static bool operator !=(HexVector2 h1, HexVector2 h2)
        {
            return !h1.Equals(h2);
        }

        /// <inheritdoc />
        public static HexVector2 operator +(HexVector2 h1, HexVector2 h2)
        {
            return new HexVector2(h1.X + h2.X, h1.Y + h2.Y);
        }

        /// <inheritdoc />
        public static HexVector2 operator -(HexVector2 h1, HexVector2 h2)
        {
            return new HexVector2(h1.X - h2.X, h1.Y - h2.Y);
        }

        /// <inheritdoc />
        public static HexVector2 operator *(HexVector2 h1, int f)
        {
            return h1.Scale(f);
        }

        /// <inheritdoc />
        public static HexVector2 operator *(int f, HexVector2 h1)
        {
            return h1.Scale(f);
        }

        #endregion // Operators
    }
}
