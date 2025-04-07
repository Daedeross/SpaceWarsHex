using SpaceWars.Model;
using System;

namespace SpaceWars.Interfaces.Orders
{
    /// <summary>
    /// Interface of orders for any energy weapon.
    /// </summary>
    public interface IEnergyWeaponOrder : IWeaponOrder
    {
        /// <summary>
        /// The power to allocate (or maybe # of dice) for the weapon.
        /// </summary>
        int Power { get; set; }
    }
}
