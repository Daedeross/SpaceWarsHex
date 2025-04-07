namespace SpaceWarsHex.Interfaces.Rules
{
    /// <summary>
    /// Interface for getting the results of concussion/physical damage.
    /// </summary>
    public interface IConcussionTable
    {
        /// <summary>
        /// Get the hull points lost by a target hit by physical damage.
        /// </summary>
        /// <param name="damageValue">The amount of concussion damage received.</param>
        /// <param name="currentShields">The current shields strength of the target.</param>
        /// <returns>The points of hull damage from this hit.</returns>
        int GetHullDamage(int damageValue, int currentShields);
    }
}
