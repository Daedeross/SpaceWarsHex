namespace SpaceWarsHex.Interfaces.Prototypes
{
    /// <summary>
    /// Public interface for all ordinance-based systems' prototypes.
    /// </summary>
    public interface IOrdinancePrototype : IWeaponPrototype
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
        /// The maximum number of uses the system has.
        /// </summary>
        int MaxUses { get; }
    }
}
