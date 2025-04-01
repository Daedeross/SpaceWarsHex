using SpaceWars.Model;
using System;
using System.Collections.Generic;

namespace SpaceWars.Interfaces
{
    /// <summary>
    /// Interface for an individual player.
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// Unique ID for the player.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// The index of the player's team.
        /// </summary>
        int TeamNumber { get; set; }

        /// <summary>
        /// The name/alias of the player.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// All ships controlled by the player.
        /// </summary>
        IList<IShip> Ships { get; }

        /// <summary>
        /// All entities controlled by the player that can reciever orders.
        /// </summary>
        ICollection<IOrderable> Orderables { get; }

        /// <summary>
        /// All entities controlled/owned by the player.
        /// </summary>
        ICollection<IHexObject> ControlledEntities { get; }

        /// <summary>
        /// Add an entity to the player's control
        /// </summary>
        /// <param name="entity"></param>
        void AddEntity(IHexObject entity);

        /// <summary>
        /// Remove an entity from the player's control.
        /// </summary>
        /// <param name="entity"></param>
        void RemoveEntity(IHexObject entity);

        /// <summary>
        /// Select any entities in a hex for the player.
        /// </summary>
        /// <param name="hex"></param>
        /// <returns>True if one or more entities are selected, otherwise False.</returns>
        bool SelectEntity(HexVector2 hex);

        /// <summary>
        /// Select the next enitity in the current selected stack, if any
        /// </summary>
        void SelectNext();

        /// <summary>
        /// Select the previous enitity in the current selected stack, if any
        /// </summary>
        void SelectPrevious();

        /// <summary>
        /// All currently selected entities.
        /// </summary>
        IEnumerable<ISelectable> Selected { get; }

        /// <summary>
        /// The primary selected entitiy.
        /// </summary>
        ISelectable Focused { get; }

        /// <summary>
        /// True if all the orders for the playres entities are valid.
        /// </summary>
        bool AllOrdersValid { get; }

        /// <summary>
        /// Call when the player is done with inputs for the current phase.
        /// </summary>
        void EndPhase();
    }
}
