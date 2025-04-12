using SpaceWarsHex.Interfaces.Orders;
using SpaceWarsHex.Model;
using System;
using System.Collections.Generic;

namespace SpaceWarsHex.Interfaces
{
    /// <summary>
    /// Interface for an entity that can recieve orders.
    /// </summary>
    public interface IOrderable
    {
        /// <summary>
        /// True if the <see cref="IOrderable"/> is in a valid state to end the phase. False if not.
        /// </summary>
        bool ValidState { get; }

        /// <summary>
        /// Reason state is not valid, null or empty if <see cref="ValidState"/> is true.
        /// </summary>
        string StateMessage { get; }

        /// <summary>
        /// Collection of all types of orders that the entity can receive.
        /// </summary>
        IReadOnlyCollection<OrderKind> ValidOrders { get; }

        /// <summary>
        /// Gives the entiry an order.
        /// </summary>
        /// <typeparam name="TOrder">The <see cref="Type"/> of the order being given.</typeparam>
        /// <param name="order">The order.</param>
        /// <returns><see cref="OrderResult"/></returns>
        OrderResult GiveOrder<TOrder>(TOrder order) where TOrder : IOrder;

        /// <summary>
        /// Get all orders for a given turn number.
        /// </summary>
        /// <param name="turnNumber">The turn number to query.</param>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="IOrder"/>.</returns>
        IReadOnlyCollection<IOrder> GetOrders(int turnNumber);

        /// <summary>
        /// Get all orders of a specific type for a given turn number
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="turnNumber"></param>
        /// <returns></returns>
        IReadOnlyCollection<TOrder> GetOrders<TOrder>(int turnNumber) where TOrder : IOrder;

        /// <summary>
        /// Get all orders for all turns.
        /// </summary>
        /// <returns><see cref="IReadOnlyDictionary{TKey, TValue}"/> where the key is the turn number and the values
        /// are the orders given that turn.</returns>
        IReadOnlyDictionary<int, IReadOnlyCollection<IOrder>> GetAllOrders();

        /// <summary>
        /// Gets the orders for the current turn.
        /// </summary>
        /// <returns></returns>
        IReadOnlyCollection<IOrder> GetCurrentTurnOrders();
    }
}
