using System;

namespace SpaceWars.Interfaces.Orders
{
    /// <summary>
    /// Interface for ordering an entity to use a direct-fire weapon.
    /// </summary>
    public interface IDirectFireOrder : IWeaponOrder
    {
        /// <summary>
        /// The weapon's target.
        /// </summary>
        Guid TargetId { get; set; }
    }
}
