using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Prototypes;
using System.Runtime.Serialization;

namespace SpaceWarsHex.ShipBuilder
{
    [Serializable]
    [DataContract]
    public class PregenWeapons : IPregenWeapons
    {
        /// <inheritdoc />
        [DataMember(Name = "EnergyWeapons")]
#pragma warning disable CS8618
        internal List<EnergyWeaponPrototype> _energyWeapons;
#pragma warning restore CS8618

        [IgnoreDataMember]
        public IReadOnlyList<IEnergyWeaponPrototype> EnergyWeapons => _energyWeapons;

        [DataMember]
        public Guid Id { get; set; }
    }
}
