using SpaceWars.Interfaces.Orders;
using SpaceWars.Interfaces.Prototypes;
using SpaceWars.Model;
using System.Collections.Generic;

namespace SpaceWars.Interfaces
{
    /// <summary>
    /// Public interface for the manager of the entire battle.
    /// The arbitrator of the game board.
    /// </summary>
    public interface IDirector
    {
        /// <summary>
        /// The current scenario's game turn number.
        /// </summary>
        int CurrentTurn { get; }

        /// <summary>
        /// All the teams in the scenario.
        /// </summary>
        IReadOnlyList<ITeam> Teams { get; }

        /// <summary>
        /// All the players in the scenario
        /// </summary>
        IReadOnlyList<IPlayer> Players { get; }

        /// <summary>
        /// All entities on the board that can receive orders
        /// </summary>
        IReadOnlyList<IOrderable> Orderables { get; }

        /// <summary>
        /// All entities on the board.
        /// </summary>
        IReadOnlyCollection<IHexObject> AllEntities { get; }

        /// <summary>
        /// Give an order to an <see cref="IOrderable"/>
        /// </summary>
        /// <param name="entity">The entity to give the order to.</param>
        /// <param name="order">The order to try and give</param>
        /// <returns><see cref="OrderResult"/></returns>
        OrderResult GiveOrder(IOrderable entity, IOrder order);

        /// <summary>
        /// Give an order to an <see cref="IOrderable{TOrder}"/>
        /// </summary>
        /// <typeparam name="TOrder">The type of the order</typeparam>
        /// <param name="entity">The entity to give the order to.</param>
        /// <param name="order">The order to try and give</param>
        /// <returns><see cref="OrderResult"/></returns>
        OrderResult GiveOrder<TOrder>(IOrderable<TOrder> entity, TOrder order) where TOrder : IOrder;

        /// <summary>
        /// The current <see cref="TurnPhase"/>
        /// </summary>
        TurnPhase CurrentPhase { get; }

        /// <summary>
        /// Create a new entity on the board from a prototype.
        /// </summary>
        /// <param name="prototype">The prototype to use</param>
        /// <param name="owner">The Player that controls/owns the entity.</param>
        /// <param name="position">The hex the entitiy is created in or it's primary hex if it is a <see cref="IMultiHexObject"/></param>
        IHexObject CreateEntity(IHexObjectPrototype prototype, IPlayer owner, HexVector2 position);

        /// <summary>
        /// Create a new entity that can have one of 6 possible orientations on the board from a prototype.
        /// </summary>
        /// <param name="prototype">The prototype to use</param>
        /// <param name="owner">The Player that controls/owns the entity.</param>
        /// <param name="position">The hex the entitiy is created in or it's primary hex if it is a <see cref="IMultiHexObject"/></param>
        /// <param name="direction">The direction the entity is rotated.</param>
        IHexObject CreateEntity(IHexObjectPrototype prototype, IPlayer owner, HexVector2 position, Direction6 direction);

        /// <summary>
        /// Create a new entity that can have one of 12 possible orientations on the board from a prototype.
        /// </summary>
        /// <param name="prototype">The prototype to use</param>
        /// <param name="owner">The Player that controls/owns the entity.</param>
        /// <param name="position">The hex the entitiy is created in or it's primary hex if it is a <see cref="IMultiHexObject"/></param>
        /// <param name="direction">The direction the entity is rotated.</param>
        IHexObject CreateEntity(IHexObjectPrototype prototype, IPlayer owner, HexVector2 position, Direction12 direction);

        /// <summary>
        /// Gets all entities that are in the target hex.
        /// </summary>
        /// <param name="hex"><see cref="HexVector2"/> of the coordinates for the target hex.</param>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="IHexObject"/></returns>
        IEnumerable<IHexObject> GetEntitiesInHex(HexVector2 hex);

        /// <summary>
        /// Gets all entities in the target hex of a specific type.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to get</typeparam>
        /// <param name="hex"><see cref="HexVector2"/> of the coordinates for the target hex.</param>
        /// <returns><see cref="IEnumerable{T}"/> of <typeparamref name="TEntity"/></returns>
        IEnumerable<TEntity> GetEntitiesInHex<TEntity>(HexVector2 hex) where TEntity : class, IHexObject;

        /// <summary>
        /// Called by the player when they are done with input for the current phase.
        /// </summary>
        /// <param name="player">The player that is done.</param>
        void PlayerEndPhase(IPlayer player);

        /// <summary>
        /// Event fired when the director creates an entity.
        /// Intended for the game engine to listen to.
        /// </summary>
        event EntityCreatedEventHandler? EntityCreated;
    }
}
