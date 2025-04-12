using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Interfaces.Systems;

namespace SpaceWarsHex.Systems
{
    /// <summary>
    /// System Factory
    /// </summary>
    public static class SystemFactory
    {
        /// <inheritdoc />
        public static Drive Create(IDrivePrototype prototype)
        {
            return new Drive(prototype);
        }

        /// <inheritdoc />
        public static Reactor Create(IReactorPrototype prototype)
        {
            return new Reactor(prototype);
        }

        /// <inheritdoc />
        public static Hull Create(IHullPrototype prototype)
        {
            return new Hull(prototype);
        }

        /// <inheritdoc />
        public static Shields Create(IShieldsPrototype prototype)
        {
            return new Shields(prototype);
        }

        /// <inheritdoc />
        public static IEnergyWeapon Create(IEnergyWeaponPrototype prototype)
        {
            return new EnergyWeapon(prototype);
        }

        /// <inheritdoc />
        public static IOrdinance Create(IOrdinancePrototype prototype)
        {
            return prototype switch
            {
                ITorpedoTubePrototype torp => new TorpedoTube(torp),
                ITransporterBombPrototype bomb => new TransporterBomb(bomb),
                _ => throw new NotImplementedException($"{prototype.GetType()} not yet implemented for Ordinance")
            };
        }
    }
}
