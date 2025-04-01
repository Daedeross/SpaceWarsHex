using SpaceWars.Interfaces.Systems;

namespace SpaceWars.Interfaces.Orders
{
    /// <summary>
    /// Root type for orders targeting a <see cref="IWeapon"/> system (Inlcudes countermeasures).
    /// </summary>
    public interface IWeaponOrder : IOrder
    {
    }
}
