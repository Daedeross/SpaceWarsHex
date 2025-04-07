using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Interfaces.Systems;

namespace SpaceWarsHex.Systems
{
    /// <summary>
    /// Class used for all <see cref="IDirectFire"/> energy weapons (e.g. Phasors, PhotonTorpedoes, etc).
    /// </summary>
    public class DirectFireEnergyWeapon : EnergyWeapon, IDirectFire
    {
        /// <summary>
        /// Public constructor for <see cref="DirectFireEnergyWeapon"/>
        /// </summary>
        /// <param name="prototype"></param>
        public DirectFireEnergyWeapon(IEnergyWeaponPrototype prototype)
            : base(prototype)
        {
            MaxRange = prototype.MaxRange;
        }

        /// <inheritdoc />
        public int? MaxRange { get; }
    }
}
