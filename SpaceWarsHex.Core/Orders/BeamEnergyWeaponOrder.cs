using SpaceWars.Interfaces.Orders;
using SpaceWars.Model;
using System;
using System.Runtime.Serialization;

namespace SpaceWars.Orders
{
    /// <inheritdoc />
    [Serializable]
    [DataContract]
    public class BeamEnergyWeaponOrder : IEnergyWeaponOrder, IBeamFireOrder
    {
        /// <inheritdoc />
        [DataMember]
        public int WeaponIndex { get; set; }

        /// <inheritdoc />
        [DataMember]
        public int Power { get; set; }

        /// <inheritdoc />
        [DataMember]
        public Direction12 Orientation { get; set; }

    }
}
