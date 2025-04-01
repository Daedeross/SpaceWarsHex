namespace SpaceWars.Interfaces.Orders
{
    /// <summary>
    /// Interface of orders for any ordinance.
    /// </summary>
    public interface IOrdinanceOrder : IWeaponOrder
    {
        /// <summary>
        /// The index of the energy weapon to fire.
        /// </summary>
        int WeaponIndex { get; }

        /// <summary>
        /// If true, clears any order for the given <see cref="WeaponIndex"/>
        /// </summary>
        bool Clear { get; }
    }
}
