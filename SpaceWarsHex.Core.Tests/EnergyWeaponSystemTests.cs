using SpaceWarsHex.Interfaces.Systems;
using SpaceWarsHex.Model;
using SpaceWarsHex.Prototypes;
using SpaceWarsHex.Systems;
using Xunit;

namespace SpaceWarsHex.Core.Tests
{
    public class EnergyWeaponSystemTests
    {
        public static readonly IEnumerable<DamageThreshold> DamageThresholds =
            [
                new() { HullStrength = 1f   , SystemMultiplier = 1f    },
                new() { HullStrength = 0.75f, SystemMultiplier = 0.875f},
                new() { HullStrength = 0.5f , SystemMultiplier = 0.75f },
                new() { HullStrength = 0.25f, SystemMultiplier = 0.5f  }
            ];

        public static TheoryData<EnergyWeaponPrototype> DirectFireEnergyWeaponPrototypes =
            [
                new EnergyWeaponPrototype { Name = "Visual Phasor1", MaxDice = 12 , FireMode = FireMode.DirectFire, FirePhase = TurnPhase.Weapons1, Visual = true, EnergyPerDie = 1, MaxRange = null, _effects = [new WeaponEffect{ DamageKind = DamageKind.Energy, DamageValue = 1}] },
                new EnergyWeaponPrototype { Name = "Visual Phasor2", MaxDice = 12, FireMode = FireMode.DirectFire, FirePhase = TurnPhase.Weapons2, Visual = true, EnergyPerDie = 1, MaxRange = 20  , _effects = [new WeaponEffect{ DamageKind = DamageKind.Energy, DamageValue = 1}] },
                new EnergyWeaponPrototype { Name = "Gamma Phasor", MaxDice = 4 , FireMode = FireMode.DirectFire, FirePhase = TurnPhase.Weapons2, Visual = true, EnergyPerDie = 3, MaxRange = null, _effects = [new WeaponEffect { DamageKind = DamageKind.Energy, DamageValue = 2 }] },
                new EnergyWeaponPrototype { Name = "Visual Photon Torpedo", MaxDice = 6 , FireMode = FireMode.DirectFire, FirePhase = TurnPhase.Weapons1, Visual = true, EnergyPerDie = 2, MaxRange = null, _effects = [new WeaponEffect { DamageKind = DamageKind.Physical, DamageValue = 1 }, new WeaponEffect { DamageKind = DamageKind.Energy, DamageValue = 1 }] },
                new EnergyWeaponPrototype { Name = "Gamma Photon Torpedo", MaxDice = 4 , FireMode = FireMode.DirectFire, FirePhase = TurnPhase.Weapons1, Visual = true, EnergyPerDie = 3, MaxRange = null, _effects = [new WeaponEffect { DamageKind = DamageKind.Physical, DamageValue = 1 }, new WeaponEffect { DamageKind = DamageKind.Energy, DamageValue = 1 }] },
                new EnergyWeaponPrototype { Name = "Quantum Torpedo", MaxDice = 4 , FireMode = FireMode.DirectFire, FirePhase = TurnPhase.Weapons1, Visual = true, EnergyPerDie = 3, MaxRange = null, _effects = [new WeaponEffect{ DamageKind = DamageKind.Energy, DamageValue = 1}, new WeaponEffect { DamageKind = DamageKind.Physical, DamageValue = 1 }] },
                new EnergyWeaponPrototype { Name = "Mass Driver", MaxDice = 3 , FireMode = FireMode.DirectFire, FirePhase = TurnPhase.Weapons1, Visual = true, EnergyPerDie = 3, MaxRange = null, _effects = [new WeaponEffect{ DamageKind = DamageKind.Direct, DamageValue = 1}] },
            ];

        [Theory]
        [MemberData(nameof(DirectFireEnergyWeaponPrototypes))]
        public void DirectFireEnergyWeaponSetsCorrectValuesFromPrototype(EnergyWeaponPrototype prototype)
        {
            IEnergyWeapon weapon = new DirectFireEnergyWeapon(prototype);

            Assert.Equal(prototype.Name, weapon.Name);
            Assert.Equal(prototype.MaxDice, weapon.InitialMaxDice);
            Assert.Equal(prototype.MaxDice, weapon.CurrentMaxDice);
            Assert.Equal(FireMode.DirectFire, weapon.FireMode);
            Assert.Equal(prototype.FirePhase, weapon.FirePhase);
            Assert.Equal(prototype.Visual, weapon.Visual);
            Assert.Equal(prototype.EnergyPerDie, weapon.EnergyPerDie);
            Assert.Equal(prototype.Effects, weapon.Effects);
        }
    }
}
