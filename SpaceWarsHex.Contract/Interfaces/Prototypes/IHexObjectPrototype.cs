namespace SpaceWarsHex.Interfaces.Prototypes
{
    /// <summary>
    /// Root interface for all entity prototypes.
    /// </summary>
    public interface IHexObjectPrototype: IPrototype, IHaveId
    {
        /// <summary>
        /// The name of the entity.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The Key for the entity to reference it's engine-specific visual representation.
        /// </summary>
        string VisualKey { get; set; }
    }
}
