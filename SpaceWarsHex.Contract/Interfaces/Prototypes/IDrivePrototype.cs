namespace SpaceWarsHex.Interfaces.Prototypes
{
    /// <summary>
    /// Interface for Drive system prototypes.
    /// </summary>
    public interface IDrivePrototype : ISystemPrototype
    {
        /// <summary>
        /// The maximum speed available, in hexes per turn.
        /// </summary>
        public int MaxWarp { get; }

        /// <summary>
        /// The maximum distance, in hexes, the entity can change velocity in one turn.
        /// </summary>
        public int AccelerationClass { get; }
    }
}
