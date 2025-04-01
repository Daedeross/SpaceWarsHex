namespace SpaceWars.Interfaces
{
    /// <summary>
    /// Interface for an entity that is player selectable.
    /// </summary>
    public interface ISelectable : IHexObject
    {
        /// <summary>
        /// True if the entity is currently selected.
        /// </summary>
        bool Selected { get; set; }
    }
}
