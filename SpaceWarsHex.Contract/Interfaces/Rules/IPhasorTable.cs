namespace SpaceWarsHex.Interfaces.Rules
{
    /// <summary>
    /// Interface for getting the results of phasor damage.
    /// </summary>
    public interface IPhasorTable
    {
        /// <summary>
        /// Get the new shield strength after being hit by energy/phasor damage.
        /// </summary>
        /// <param name="damageValue">The amount of phasor damage received.</param>
        /// <param name="currentShields">The current shields strength of the target.</param>
        /// <param name="saveShields">True if the target wishes to keep shields at (4?) by taking extra energy penalty and damage.</param>
        /// <returns>The new current shields strength after the attack.</returns>
        int GetNewShields(int damageValue, int currentShields, bool saveShields = false);

        /// <summary>
        /// Gets the points of hull damage a target hit by energy/phasor damage.
        /// </summary>
        /// <param name="damageValue">The amount of phasor damage received.</param>
        /// <param name="currentShields">The current shields strength of the target.</param>
        /// <param name="saveShields">True if the target wishes to keep shields at (4?) by taking extra energy penalty and damage.</param>
        /// <returns>The points of hull damage from this hit.</returns>
        int GetHullDamage(int damageValue, int currentShields, bool saveShields = false);

        /// <summary>
        /// Gets the next-turn energy penalty for a target hit by energy/phasor damage.
        /// </summary>
        /// <param name="damageValue">The amount of phasor damage received.</param>
        /// <param name="currentShields">The current shields strength of the target.</param>
        /// <param name="saveShields">True if the target wishes to keep shields at (4?) by taking extra energy penalty and damage.</param>
        /// <returns>The energy point penalty off next turn for the target.</returns>
        int GetEnergyPenalty(int damageValue, int currentShields, bool saveShields = false);
    }
}
