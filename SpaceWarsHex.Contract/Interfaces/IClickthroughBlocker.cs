namespace SpaceWars.Interfaces
{
    /// <summary>
    /// Component that can block clicks for certaion screen positions.
    /// Used by UI frameworks.
    /// </summary>
    /// <typeparam name="TVector"></typeparam>
    public interface IClickthroughBlocker<TVector>
    {
        /// <summary>
        /// Prevents selection from occurring for certain points.
        /// </summary>
        /// <param name="point">The point th check.</param>
        /// <returns>True if selection should be blocked for the given point. False otherwise.</returns>
        bool BlockSelect(TVector point);
    }
}
