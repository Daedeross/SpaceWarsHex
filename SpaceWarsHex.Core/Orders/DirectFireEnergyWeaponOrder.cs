using SpaceWars.Interfaces;
using SpaceWars.Interfaces.Orders;
using System;
using System.Runtime.Serialization;

namespace SpaceWars.Orders
{
    /// <inheritdoc />
    [Serializable]
    [DataContract]
    public class DirectFireEnergyWeaponOrder : IDirectFireOrder, IEnergyWeaponOrder
    {
        /// <inheritdoc />
        // TODO: maybe have this (and any entity reference) be a GUID for serialization.
        public Guid TargetId { get; set; }

        /// <inheritdoc />
        [DataMember]
        public int WeaponIndex { get; set; }

        /// <inheritdoc />
        [DataMember]
        public int Power { get; set; }
    }
}
