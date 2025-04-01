using SpaceWars.Interfaces.Prototypes;
using SpaceWars.Model;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SpaceWars.Prototypes
{
    /// <inheritdoc />
    [Serializable]
    [DataContract]
    public class EnergyWeaponPrototype : SystemPrototypeBase, IEnergyWeaponPrototype
    {
        /// <inheritdoc />
        [DataMember]
        public int MaxDice { get; set; }

        /// <inheritdoc />
        [DataMember]
        public FireMode FireMode { get; set; }

        /// <inheritdoc />
        [DataMember]
        public TurnPhase FirePhase { get; set; } = TurnPhase.Weapons1;

        /// <inheritdoc />
        [DataMember]
        public bool Visual { get; set; }

        /// <inheritdoc />
        [DataMember]
        public int EnergyPerDie { get; set; }

        /// <inheritdoc />
        [DataMember]
        public int? MaxRange { get; set; }

        /// <inheritdoc />
        [IgnoreDataMember]
        public IReadOnlyList<WeaponEffect> Effects => _effects;

        /// <inheritdoc />
        [DataMember(Name = "Effects")]
#pragma warning disable CS8618
        internal List<WeaponEffect> _effects;
#pragma warning restore CS8618
    }
}
