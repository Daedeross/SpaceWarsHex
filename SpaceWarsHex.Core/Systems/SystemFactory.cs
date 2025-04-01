using SpaceWars.Interfaces.Prototypes;
using SpaceWars.Interfaces.Systems;
using SpaceWars.Model;
using System;

namespace SpaceWars.Systems
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
            return prototype.FireMode switch
            {
                FireMode.DirectFire => new DirectFireEnergyWeapon(prototype),
                FireMode.Beam       => throw new NotImplementedException("Beam Weapons"),
                FireMode.Bomb       => throw new NotImplementedException("Bombs"),
                FireMode.Torpedo    => throw new NotImplementedException("Energy Torpedo Tube"),
                FireMode.Wall       => throw new NotImplementedException("Wall"),
                FireMode.Pulse      => throw new NotImplementedException("Pulse"),
                FireMode.Wave       => throw new NotImplementedException("Wave"),
                _ => throw new NotImplementedException($"Fire Mode {prototype.FireMode} not yet implemented for Energy Weapons" )
            };
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
