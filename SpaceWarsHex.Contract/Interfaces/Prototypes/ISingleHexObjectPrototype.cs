using SpaceWarsHex.Model;

namespace SpaceWarsHex.Interfaces.Prototypes
{
    /// <summary>
    /// Base prototype for entities that only have a single location and do not take up entire (one or more) hexes.
    /// </summary>
    public interface ISingleHexObjectPrototype : IHexObjectPrototype
    {
        /// <summary>
        /// The visual representaion of the entity.
        /// </summary>
        RenderDefinition Visual { get; }
    }
}
