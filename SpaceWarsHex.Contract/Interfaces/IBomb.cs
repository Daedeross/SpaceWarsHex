namespace SpaceWarsHex.Interfaces
{
    /// <summary>
    /// public interface for Bombs and G-Bomb entities.
    /// </summary>
    public interface IBomb : IHexObject
    {
        /// <summary>
        /// The explosive power of the bomb.
        /// </summary>
        int Power { get; }

        /// <summary>
        /// Is the bomb currently visible to all teams
        /// </summary>
        bool IsVisibleToAll { get; }

        /// <summary>
        /// Number of turns until the bomb becomes visible to all teams.
        /// </summary>
        int TurnsUntilVisible { get; }

        /// <summary>
        /// Number of turns until the bomb explodes.
        /// </summary>
        int TurnsUntilExplode { get; }
    }
}
