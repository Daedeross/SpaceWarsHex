using SpaceWars.Interfaces.Rules;
using System;

namespace SpaceWars.Rules
{
    /// <summary>
    /// Class to implement <see cref="IPhasorTable"/>.
    /// TODO: Currenly hard-coding, replace with CSV import or similar.
    /// </summary>
    public class PhasorTable : IPhasorTable
    {
        const int SaveAmount = 5;

        /// <inheritdoc />
        public int GetEnergyPenalty(int damageValue, int currentShields, bool saveShields = false)
        {
            var diff = damageValue - currentShields;
            if (saveShields && diff > -SaveAmount) { diff += Math.Max(-diff, SaveAmount); }
            return Math.Max(0, diff - GetHullDamage(damageValue, currentShields));
        }

        /// <inheritdoc />
        public int GetHullDamage(int damageValue, int currentShields, bool saveShields = false)
        {
            var diff = damageValue - currentShields;
            if (saveShields && diff > -SaveAmount) { diff += Math.Max(-diff, SaveAmount); }
            return Math.Max(0, (diff + 1) / 2 - 2);
        }

        /// <inheritdoc />
        public int GetNewShields(int damageValue, int currentShields, bool saveShields = false)
        {
            var diff = currentShields - damageValue;
            return saveShields && diff < SaveAmount
                ? SaveAmount
                : Math.Max(0, diff);
        }
    }
}