namespace SpaceWars.Interfaces.Prototypes
{
    /// <summary>
    /// Interface for a ship prototype's hull.
    /// </summary>
    public interface IHullPrototype
    {
        /// <summary>
        /// The Max HP (Hull Points?) for a ship.
        /// </summary>
        public int MaxIntegrity { get; }
    }
}
