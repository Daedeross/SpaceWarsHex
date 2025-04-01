using SpaceWars.Model;

namespace SpaceWars.Interfaces.Orders
{
    /// <summary>
    /// Interface for ordering an entity to use a beam weapon.
    /// </summary>
    public interface IBeamFireOrder : IWeaponOrder, IHaveDirection12
    {
    }
}
