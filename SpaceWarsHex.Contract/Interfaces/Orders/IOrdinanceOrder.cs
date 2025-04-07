using SpaceWars.Model;

namespace SpaceWars.Interfaces.Orders
{
    /// <summary>
    /// Interface of orders for any ordinance.
    /// </summary>
    public interface IOrdinanceOrder : IWeaponOrder
    {
        /// <summary>
        /// If true, clears any order for the given <see cref="IWeaponOrder.WeaponIndex"/>
        /// </summary>
        bool Clear { get; }

        /// <summary>
        /// <see cref="OridinanceLoad"/>
        /// </summary>
        OridinanceLoad Load { get; }
    }
}
