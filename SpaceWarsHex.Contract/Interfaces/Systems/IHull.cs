namespace SpaceWarsHex.Interfaces.Systems
{
    /// <summary>
    /// Interface for a ship's hull.
    /// </summary>
    public interface IHull
    {
        /// <summary>
        /// The Max HP (Hull Points?) for a ship.
        /// </summary>
        int MaxIntegrity { get; }

        /// <summary>
        /// The current HP for a ship.
        /// </summary>
        int CurrentIntegrity { get; set; }

        /// <summary>
        /// Assigns pending damage to the hull.
        /// This does not update <see cref="CurrentIntegrity"/> until <see cref="ApplyDamage"/> is called.
        /// </summary>
        /// <param name="damage"></param>
        void AssignDamage(int damage);

        /// <summary>
        /// Applies pending damage to the hull and updates <see cref="CurrentIntegrity"/>
        /// </summary>
        void ApplyDamage();
    }
}
