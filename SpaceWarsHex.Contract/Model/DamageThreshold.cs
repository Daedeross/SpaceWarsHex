using System;

namespace SpaceWars.Model
{
    /// <summary>
    /// Struct to define a system's damage thresholds.
    /// </summary>
    /// <remarks>
    /// These are intended to be sorted by <see cref="HullStrength"/> and only the smallest threshold that has been passed applies.
    /// Example of use:
    /// If a reactor the following threshold:
    /// { HullStrength = 0.75f, SystemMultiplier = 0.9f }
    /// then as soon as its Hull.CurrentIntegrity / Hull.MaxIntegrity is less than or equal to 0.75 (i.e. its hull is at or bellow 75% integrity),
    /// its reactor max power available is reduced to 90% (assumed round-down, but the specifics are up to the individual system)
    /// </remarks>
    [Serializable]
    public struct DamageThreshold : IComparable<DamageThreshold>
    {
        private int? _hash;

        /// <summary>
        /// The ratio of current hull to max hull at which this threshold applies.
        /// The threshold applies as soon as the ration equals or exceeds the value.
        /// </summary>
        public float HullStrength;

        /// <summary>
        /// Multiplier to the effecteveness of the system, usually reduces the max power allocatable.
        /// </summary>
        public float SystemMultiplier;

        /// <inheritdoc />
        public int CompareTo(DamageThreshold other)
        {
            return HullStrength.CompareTo(other.HullStrength);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            if (_hash is null)
            {
                _hash = HullStrength.GetHashCode() * (SystemMultiplier.GetHashCode() ^ 263);
            }

            return _hash.Value;
        }
    }
}