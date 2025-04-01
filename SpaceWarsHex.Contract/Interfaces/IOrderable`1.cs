using SpaceWars.Interfaces.Orders;
using SpaceWars.Model;

namespace SpaceWars.Interfaces
{
    /// <summary>
    /// Interface for an entity that can recieve orders.
    /// </summary>
    /// <typeparam name="TOrder"></typeparam>
    public interface IOrderable<TOrder> : IOrderable
        where TOrder : IOrder
    {
        /// <summary>
        /// Give an entity an order of type <typeparamref name="TOrder"/>.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <returns><see cref="OrderResult"/></returns>
        OrderResult GiveOrder(TOrder order);
    }
}
