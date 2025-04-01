using SpaceWars.Interfaces.Prototypes;
using SpaceWars.Model;
using SpaceWars.Prototypes;
using System;
using System.Collections.Generic;

namespace SpaceWars.Mock
{
    public static class Prototypes
    {
        public static IShipPrototype Ship1()
        {
            var ship = new ShipPrototype
            {
                Name = "I'm a Ship!",
                _hull = new HullPrototype
                {
                    Name = "Hull",
                    MaxIntegrity = 23
                },
                _drive = new DrivePrototype
                {
                    Name = "Drive",
                    AccelerationClass = 3,
                    MaxWarp = 7,
                    _damageThresholds = new List<DamageThreshold>
                    {
                        new() { HullStrength = 1f   , SystemMultiplier = 1f     },
                        new() { HullStrength = 0.75f, SystemMultiplier = 0.875f },
                        new() { HullStrength = 0.5f , SystemMultiplier = 0.75f  },
                        new() { HullStrength = 0.25f, SystemMultiplier = 0.5f   }
                    }
                },
                _shields = new ShieldsPrototype
                {
                    MaxPower = 15,
                    _damageThresholds = new List<DamageThreshold>
                    {
                        new() { HullStrength = 1f   , SystemMultiplier = 1f     },
                        new() { HullStrength = 0.75f, SystemMultiplier = 0.875f },
                        new() { HullStrength = 0.5f , SystemMultiplier = 0.75f  },
                        new() { HullStrength = 0.25f, SystemMultiplier = 0.5f   }
                    }
                },
                _reactor = new ReactorPrototype
                {
                    AttackPower = 18,
                    CruisePower = 12,
                    MaxTurnsAtAttackPower = 4,
                    EmergencyPower = 4,
                    _damageThresholds = new List<DamageThreshold>
                    {
                        new() { HullStrength = 1f   , SystemMultiplier = 1f     },
                        new() { HullStrength = 0.75f, SystemMultiplier = 0.875f },
                        new() { HullStrength = 0.5f , SystemMultiplier = 0.75f  },
                        new() { HullStrength = 0.25f, SystemMultiplier = 0.5f   }
                    }
                },
                Visual = new RenderDefinition
                {
                    Kind = SingleRenderKind.Sprite,
                    Path = "Assets/Sprites/SimpleShip.png"
                },
                _energyWeapons = new List<EnergyWeaponPrototype>
                {
                    new EnergyWeaponPrototype
                    {
                        Name = "Visual Phasor",
                        FireMode = FireMode.DirectFire,
                        FirePhase = TurnPhase.Weapons1,
                        MaxDice = 12,
                        EnergyPerDie = 1,
                        _effects = new List<WeaponEffect>
                        {
                            new WeaponEffect { DamageKind = DamageKind.Energy, DamageValue = 1 }
                        },
                        _damageThresholds = new List<DamageThreshold>
                        {
                            new() { HullStrength = 1f   , SystemMultiplier = 1f     },
                            new() { HullStrength = 0.75f, SystemMultiplier = 0.875f },
                            new() { HullStrength = 0.5f , SystemMultiplier = 0.75f  },
                            new() { HullStrength = 0.25f, SystemMultiplier = 0.5f   }
                        }
                    },
                    new EnergyWeaponPrototype
                    {
                        Name = "Visual Photon Torpedo",
                        FireMode = FireMode.DirectFire,
                        FirePhase = TurnPhase.Weapons1,
                        MaxDice = 6,
                        EnergyPerDie = 2,
                        _effects = new List<WeaponEffect>
                        {
                            new WeaponEffect { DamageKind = DamageKind.Physical, DamageValue = 1 },
                            new WeaponEffect { DamageKind = DamageKind.Energy, DamageValue = 1 }
                        },
                        _damageThresholds = new List<DamageThreshold>
                        {
                            new() { HullStrength = 1f   , SystemMultiplier = 1f     },
                            new() { HullStrength = 0.75f, SystemMultiplier = 0.875f },
                            new() { HullStrength = 0.5f , SystemMultiplier = 0.75f  },
                            new() { HullStrength = 0.25f, SystemMultiplier = 0.5f   }
                        }
                    },
                }
            };

            return ship;
        }

        public static IShipPrototype Ship2()
        {
            var ship = new ShipPrototype
            {
                Name = "I'm also Ship!",
                _hull = new HullPrototype
                {
                    Name = "Hull",
                    MaxIntegrity = 25
                },
                _drive = new DrivePrototype
                {
                    Name = "Drive",
                    AccelerationClass = 2,
                    MaxWarp = 6,
                    _damageThresholds = new List<DamageThreshold>
                    {
                        new() { HullStrength = 1f   , SystemMultiplier = 1f     },
                        new() { HullStrength = 0.75f, SystemMultiplier = 0.875f },
                        new() { HullStrength = 0.5f , SystemMultiplier = 0.75f  },
                        new() { HullStrength = 0.25f, SystemMultiplier = 0.5f   }
                    }
                },
                _shields = new ShieldsPrototype
                {
                    MaxPower = 20,
                    _damageThresholds = new List<DamageThreshold>
                    {
                        new() { HullStrength = 1f   , SystemMultiplier = 1f     },
                        new() { HullStrength = 0.75f, SystemMultiplier = 0.875f },
                        new() { HullStrength = 0.5f , SystemMultiplier = 0.75f  },
                        new() { HullStrength = 0.25f, SystemMultiplier = 0.5f   }
                    }
                },
                _reactor = new ReactorPrototype
                {
                    AttackPower = 20,
                    CruisePower = 20,
                    MaxTurnsAtAttackPower = 0,
                    EmergencyPower = 4,
                    _damageThresholds = new List<DamageThreshold>
                    {
                        new() { HullStrength = 1f   , SystemMultiplier = 1f     },
                        new() { HullStrength = 0.75f, SystemMultiplier = 0.875f },
                        new() { HullStrength = 0.5f , SystemMultiplier = 0.75f  },
                        new() { HullStrength = 0.25f, SystemMultiplier = 0.5f   }
                    }
                },
                Visual = new RenderDefinition
                {
                    Kind = SingleRenderKind.Sprite,
                    Path = "Assets/Sprites/SimpleShip.png"
                },
                _energyWeapons = new List<EnergyWeaponPrototype>
                {
                    new EnergyWeaponPrototype
                    {
                        Name = "Visual Quantum Torpedo",
                        FireMode = FireMode.DirectFire,
                        FirePhase = TurnPhase.Weapons1,
                        MaxDice = 9,
                        EnergyPerDie = 3,
                        _effects = new List<WeaponEffect>
                        {
                            new WeaponEffect { DamageKind = DamageKind.Energy, DamageValue = 1 },
                            new WeaponEffect { DamageKind = DamageKind.Physical, DamageValue = 1 }
                        },
                        _damageThresholds = new List<DamageThreshold>
                        {
                            new() { HullStrength = 1f   , SystemMultiplier = 1f     },
                            new() { HullStrength = 0.75f, SystemMultiplier = 0.875f },
                            new() { HullStrength = 0.5f , SystemMultiplier = 0.75f  },
                            new() { HullStrength = 0.25f, SystemMultiplier = 0.5f   }
                        }
                    },
                    new EnergyWeaponPrototype
                    {
                        Name = "Visual Photon Torpedo",
                        FireMode = FireMode.DirectFire,
                        FirePhase = TurnPhase.Weapons1,
                        MaxDice = 10,
                        EnergyPerDie = 2,
                        _effects = new List<WeaponEffect>
                        {
                            new WeaponEffect { DamageKind = DamageKind.Physical, DamageValue = 1 },
                            new WeaponEffect { DamageKind = DamageKind.Energy, DamageValue = 1 }
                        },
                        _damageThresholds = new List<DamageThreshold>
                        {
                            new() { HullStrength = 1f   , SystemMultiplier = 1f     },
                            new() { HullStrength = 0.75f, SystemMultiplier = 0.875f },
                            new() { HullStrength = 0.5f , SystemMultiplier = 0.75f  },
                            new() { HullStrength = 0.25f, SystemMultiplier = 0.5f   }
                        }
                    },
                }
            };

            return ship;
        }
    }
}
