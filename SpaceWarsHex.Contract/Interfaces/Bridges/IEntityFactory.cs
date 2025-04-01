namespace SpaceWars.Interfaces.Bridges
{
    using SpaceWars.Interfaces.Prototypes;
    using SpaceWars.Model;

    /// <summary>
    /// Factory to create entities, dependent on engine and scene.
    /// </summary>
    public interface IEntityFactory
    {
        /// <summary>
        /// Create a new entity on the board from a prototype.
        /// </summary>
        /// <param name="prototype">The prototype to use</param>
        /// <param name="owner">The Player that controls/owns the entity.</param>
        /// <param name="Position">The hex the entitiy is created in or it's primary hex if it is a <see cref="IMultiHexObject"/></param>
        IHexObject CreateEntity(IHexObjectPrototype prototype, IPlayer owner, HexVector2 Position);

        /// <summary>
        /// Create a new entity that can have one of 6 possible orientations on the board from a prototype.
        /// </summary>
        /// <param name="prototype">The prototype to use</param>
        /// <param name="owner">The Player that controls/owns the entity.</param>
        /// <param name="Position">The hex the entitiy is created in or it's primary hex if it is a <see cref="IMultiHexObject"/></param>
        /// <param name="direction">The direction the entity is rotated.</param>
        IHexObject CreateEntity(IHexObjectPrototype prototype, IPlayer owner, HexVector2 Position, Direction6 direction);

        /// <summary>
        /// Create a new entity that can have one of 12 possible orientations on the board from a prototype.
        /// </summary>
        /// <param name="prototype">The prototype to use</param>
        /// <param name="owner">The Player that controls/owns the entity.</param>
        /// <param name="Position">The hex the entitiy is created in or it's primary hex if it is a <see cref="IMultiHexObject"/></param>
        /// <param name="direction">The direction the entity is rotated.</param>
        IHexObject CreateEntity(IHexObjectPrototype prototype, IPlayer owner, HexVector2 Position, Direction12 direction);

        /// <summary>
        /// Removes the entity from the underlying scene. Any other references must be freed by the director.
        /// </summary>
        /// <param name="hexObject">The entity to destroy</param>
        void Destroy(IHexObject hexObject);
    }
}
