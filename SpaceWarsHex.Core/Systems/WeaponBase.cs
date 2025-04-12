using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Interfaces.Systems;
using SpaceWarsHex.Model;

namespace SpaceWarsHex.Systems
{
    public abstract class WeaponBase : SystemBase, IWeapon
    {
        /// <inheritdoc />
        public FireMode FireMode { get; }

        /// <inheritdoc />
        public TurnPhase FirePhase { get; }

        public int MaxRange { get; }

        protected WeaponBase(IWeaponPrototype prototype) : base(prototype)
        {
            FireMode = prototype.FireMode;
            FirePhase = prototype.FirePhase;
            MaxRange = prototype.MaxRange;
        }
    }
}
