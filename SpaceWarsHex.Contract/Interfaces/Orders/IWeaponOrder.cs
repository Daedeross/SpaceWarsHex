using SpaceWarsHex.Interfaces.Systems;
using SpaceWarsHex.Model;
using System;

namespace SpaceWarsHex.Interfaces.Orders
{
    /// <summary>
    /// Root type for orders targeting a <see cref="IWeapon"/> system (Inlcudes countermeasures).
    /// </summary>
    public interface IWeaponOrder : IOrder
    {
        /// <summary>
        /// The index of the weapon to fire.
        /// </summary>
        int WeaponIndex { get; set; }

        /// <summary>
        /// For <see cref="FireMode.DirectFire"/>. The weapon's target.
        /// </summary>
        Guid? TargetId { get; set; }

        /// <summary>
        /// For <see cref="FireMode.Beam"/>. The 12-direction orientation
        /// </summary>
        Direction12? Orientation12 { get; set; }

        /// <summary>
        /// For <see cref="FireMode.Wall"/>. The 6-direction orientation
        /// </summary>
        Direction6? Orientation6 { get; set; }

        /// <summary>
        /// For <see cref="FireMode.Bomb"/>. The hex the bomb will explode in.
        /// </summary>
        HexVector2? TargetHex { get; set; }

        /// <summary>
        /// For <see cref="FireMode.Torpedo"/> and <see cref="FireMode.Wall"/>. The desired velocity of the torpedo.
        /// </summary>
        HexVector2? Velocity { get; }
    }
}
