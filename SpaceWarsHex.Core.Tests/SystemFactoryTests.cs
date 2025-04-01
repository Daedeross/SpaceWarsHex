using SpaceWars.Interfaces.Prototypes;
using SpaceWars.Model;
using SpaceWars.Prototypes;
using SpaceWars.Systems;
using Xunit;

namespace SpaceWars.Core.Tests
{
    public class SystemFactoryTests
    {

        [Fact(Skip = "Not yet implemented")]
        public void FactoryCreatesBeamEnergyWeaponsTest()
        {
            var prototype = new EnergyWeaponPrototype { Name = "Beam", FireMode = FireMode.Beam };

            var system = SystemFactory.Create(prototype);

            //Assert.IsAssignableFrom<BeamEnergyWeapon>(system);
        }

        [Fact(Skip = "Not yet implemented")]
        public void FactoryCreatesBombEnergyWeaponsTest()
        {
            var prototype = new EnergyWeaponPrototype { Name = "Bomb", FireMode = FireMode.Bomb };

            var system = SystemFactory.Create(prototype);

            //Assert.IsAssignableFrom<BombEnergyWeapon>(system);
        }

        [Fact]
        public void FactoryCreatesDirectFireEnergyWeaponsTest()
        {
            var prototype = new EnergyWeaponPrototype { Name = "DirectFire", FireMode = FireMode.DirectFire };

            var system = SystemFactory.Create(prototype);

            Assert.IsAssignableFrom<DirectFireEnergyWeapon>(system);
        }

        [Fact(Skip = "Not yet implemented")]
        public void FactoryCreatesPulseEnergyWeaponsTest()
        {
            var prototype = new EnergyWeaponPrototype { Name = "Pulse", FireMode = FireMode.Pulse };

            var system = SystemFactory.Create(prototype);

            //Assert.IsAssignableFrom<PulseEnergyWeapon>(system);
        }

        [Fact(Skip = "Not yet implemented")]
        public void FactoryCreatesTorpedoEnergyWeaponsTest()
        {
            var prototype = new EnergyWeaponPrototype { Name = "Torpedo", FireMode = FireMode.Torpedo };

            var system = SystemFactory.Create(prototype);

            //Assert.IsAssignableFrom<TorpedoEnergyWeapon>(system);
        }

        [Fact(Skip = "Not yet implemented")]
        public void FactoryCreatesWallEnergyWeaponsTest()
        {
            var prototype = new EnergyWeaponPrototype { Name = "Wall", FireMode = FireMode.Wall };

            var system = SystemFactory.Create(prototype);

            //Assert.IsAssignableFrom<WallEnergyWeapon>(system);
        }


        [Fact(Skip = "Not yet implemented")]
        public void FactoryCreatesWaveEnergyWeaponsTest()
        {
            var prototype = new EnergyWeaponPrototype { Name = "Wave", FireMode = FireMode.Wave };

            var system = SystemFactory.Create(prototype);

            //Assert.IsAssignableFrom<WaveEnergyWeapon>(system);
        }

    }
}
