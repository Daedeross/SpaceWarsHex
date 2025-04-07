namespace SpaceWarsHex.Interfaces.Bridges
{
    /// <summary>
    /// Helper to manage a collection of entities that are each executing something concurrently.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IConcurencyManager<TEntity>
    {
        /// <summary>
        /// Start the concurrent execution of the contained entities.
        /// </summary>
        public void Start();
    }
}
