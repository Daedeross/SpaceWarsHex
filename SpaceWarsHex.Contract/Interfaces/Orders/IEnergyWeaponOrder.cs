namespace SpaceWars.Interfaces.Orders
{
    /// <summary>
    /// Interface of orders for any energy weapon.
    /// </summary>
    public interface IEnergyWeaponOrder : IWeaponOrder
    {
        /// <summary>
        /// The index of the energy weapon to fire.
        /// </summary>
        int WeaponIndex { get; set; }

        /// <summary>
        /// The power to allocate (or maybe # of dice) for the weapon.
        /// </summary>
        int Power { get; set; }
    }
}
