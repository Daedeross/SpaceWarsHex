using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Model;
using System.Runtime.Serialization;

namespace SpaceWarsHex.Prototypes
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
        public int MaxRange { get; set; }

        /// <inheritdoc />
        [IgnoreDataMember]
        public IReadOnlyList<WeaponEffect> Effects => _effects;

        /// <inheritdoc />
        [DataMember(Name = "Effects")]
#pragma warning disable CS8618
        internal List<WeaponEffect> _effects;
#pragma warning restore CS8618

        public override object Clone()
        {
            var clone = new EnergyWeaponPrototype()
            {
                MaxDice = MaxDice,
                FireMode = FireMode,
                FirePhase = FirePhase,
                Visual = Visual,
                EnergyPerDie = EnergyPerDie,
                MaxRange = MaxRange,
                _effects = [.. _effects.Select(effect => new WeaponEffect
                {
                    DamageKind = effect.DamageKind,
                    DamageValue = effect.DamageValue
                })]
            };
            CopyTo(clone);
            return clone;
        }
    }
}
