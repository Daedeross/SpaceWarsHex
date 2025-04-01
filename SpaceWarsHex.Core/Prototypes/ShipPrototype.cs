using SpaceWars.Interfaces.Prototypes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SpaceWars.Prototypes
{
#pragma warning disable CS8618 // Null-checking will be done uing comprehensive Validators
    /// <inheritdoc />
    [Serializable]
    [DataContract]
    public class ShipPrototype : SingleHexObjectPrototypeBase, IShipPrototype
    {
        [DataMember(Name = "Reactor")]
        internal ReactorPrototype _reactor;

        [DataMember(Name = "Drive")]
        internal DrivePrototype _drive;

        [DataMember(Name = "Shields")]
        internal ShieldsPrototype _shields;

        [DataMember(Name = "Hull")]
        internal HullPrototype _hull;

        [DataMember(Name = "EnergyWeapons")]
        internal List<EnergyWeaponPrototype> _energyWeapons;

        [DataMember(Name = "Ordinances")]
        internal List<IOrdinancePrototype> _ordinances;

        /// <inheritdoc />
        [IgnoreDataMember]
        public IReadOnlyList<IEnergyWeaponPrototype> EnergyWeapons => _energyWeapons;

        /// <inheritdoc />
        [IgnoreDataMember]
        public IReadOnlyList<IOrdinancePrototype> Ordinances { get; set; }

        /// <inheritdoc />
        [IgnoreDataMember]
        public IReactorPrototype Reactor => _reactor;

        /// <inheritdoc />
        [IgnoreDataMember]
        public IDrivePrototype Drive => _drive;

        /// <inheritdoc />
        [IgnoreDataMember]
        public IShieldsPrototype Shields => _shields;

        /// <inheritdoc />
        [IgnoreDataMember]
        public IHullPrototype Hull => _hull;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
