using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SpaceWarsHex.Model;
using SpaceWarsHex.Prototypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceWarsHex.ShipBuilder.Configuration
{
    public class DefaultsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IDefaultValueProvider>()
                    .ImplementedBy<DefaultValueProvider>()
                    .OnCreate(provider =>
                    {
                        RegisterDefaultDamageThresholds(provider);
                        RegisterDefaultHull(provider);
                        RegisterDefaultDrive(provider);
                        RegisterDefaultReactor(provider);
                        RegisterDefaultShield(provider);
                        RegisterDefaultEnergyWeapon(provider);
                        RegisterDefaultTorpedo(provider);
                        RegisterDefaultShip(provider);
                    })
                    .LifestyleSingleton()
                );
        }

        private static void RegisterDefaultDamageThresholds(IDefaultValueProvider provider)
        {
            var defaultThresholds = new List<DamageThreshold>
            {
                new() {
                    HullStrength = 1.0f,
                    SystemMultiplier = 1.0f
                },
                new() {
                    HullStrength = 0.75f,
                    SystemMultiplier = 0.875f
                },
                new() {
                    HullStrength = 0.5f,
                    SystemMultiplier = 0.75f
                },
                new() {
                    HullStrength = 0.25f,
                    SystemMultiplier = 0.5f
                }
            };
            provider.SetDefaultValues(defaultThresholds);
        }

        private static void RegisterDefaultDrive(IDefaultValueProvider provider)
        {
            var defaultDrive = new DrivePrototype
            {
                Id = Guid.NewGuid(),
                Name = "Drive",
                MaxWarp = 5,
                AccelerationClass = 1,
                _damageThresholds = provider.GetDefaultValues<DamageThreshold>()
            };

            provider.SetDefaultValue(defaultDrive);
        }

        private static void RegisterDefaultReactor(IDefaultValueProvider provider)
        {
            var defaultReactor = new ReactorPrototype
            {
                Id = Guid.NewGuid(),
                Name = "M/AM Reactor",
                CruisePower = 10,
                AttackPower = 10,
                EmergencyPower = 5,
                MaxTurnsAtAttackPower = 5,
                _damageThresholds = provider.GetDefaultValues<DamageThreshold>()
            };
            provider.SetDefaultValue(defaultReactor);
        }

        private static void RegisterDefaultShield(IDefaultValueProvider provider)
        {
            var defaultShield = new ShieldsPrototype
            {
                Id = Guid.NewGuid(),
                Name = "Shields",
                MaxPower = 10,
                _damageThresholds = provider.GetDefaultValues<DamageThreshold>()
            };
            provider.SetDefaultValue(defaultShield);
        }

        private static void RegisterDefaultHull(IDefaultValueProvider provider)
        {
            var defaultHull = new HullPrototype
            {
                Id = Guid.NewGuid(),
                Name = "Hull",
                MaxIntegrity = 40
            };
            provider.SetDefaultValue(defaultHull);
        }

        private static void RegisterDefaultEnergyWeapon(IDefaultValueProvider provider)
        {
            var defaultEnergyWeapon = new EnergyWeaponPrototype
            {
                Id = Guid.NewGuid(),
                Name = "Visual Photon Torpedo",
                FireMode = FireMode.DirectFire,
                FirePhase = TurnPhase.Weapons1,
                MaxDice = 5,
                EnergyPerDie = 2,
                Visual = true,
                _effects =
                [
                    new() {
                        DamageKind = DamageKind.Physical,
                        DamageValue = 1,
                    },
                    new() {
                        DamageKind = DamageKind.Energy,
                        DamageValue = 1,
                    },
                ],
                _damageThresholds = provider.GetDefaultValues<DamageThreshold>()
            };
            provider.SetDefaultValue(defaultEnergyWeapon);
        }

        private static void RegisterDefaultTorpedo(IDefaultValueProvider provider)
        {
            var defaultTorpedo = new OrdinancePrototype
            {
                Id = Guid.NewGuid(),
                Name = "Torpedo",
                FireMode = FireMode.Torpedo,
                FirePhase = TurnPhase.Weapons2,
                Strength = 10,
                MaxRange = 0,
                MaxUses = 10,
                _damageThresholds = provider.GetDefaultValues<DamageThreshold>()
            };
            provider.SetDefaultValue(defaultTorpedo);
        }

        private static void RegisterDefaultShip(IDefaultValueProvider provider)
        {
            var defaultShip = new ShipPrototype
            {
                Name = "New Ship",
                _hull = provider.GetDefaultValue<HullPrototype>(),
                _reactor = provider.GetDefaultValue<ReactorPrototype>(),
                _drive = provider.GetDefaultValue<DrivePrototype>(),
                _shields = provider.GetDefaultValue<ShieldsPrototype>(),
            };
            provider.SetDefaultValue(defaultShip);
        }
    }
}
