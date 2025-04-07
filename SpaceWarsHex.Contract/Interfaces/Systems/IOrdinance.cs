namespace SpaceWarsHex.Interfaces.Systems
{
    /// <summary>
    /// Public interface for all ordinance-based systems.
    /// </summary>
    public interface IOrdinance : IWeapon
    {
        /// <summary>
        /// The numerical power/strength of the system.
        /// </summary>
        /// <remarks>
        /// Means different things to different systems.
        /// For bombs and torpedoes, this is the direct explosive power of the weapon.
        /// For Smoke and spray, this is the number of turns they persist for.
        /// </remarks>
        int Strength { get; }

        /// <summary>
        /// The maximum numver of uses the system has.
        /// </summary>
        int MaxUses { get; }

        /// <summary>
        /// The number of uses the system has left.
        /// </summary>
        int UsesRemaining { get; }
    }
}
