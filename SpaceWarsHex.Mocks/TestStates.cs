using SpaceWarsHex.Model;
using SpaceWarsHex.States.Entities;
using SpaceWarsHex.States.Systems;
using System;

namespace SpaceWarsHex.Mocks
{
    public static class TestStates
    {
        public static HullState Hull1 => new()
        {
            Id = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C5941100044"),
            PrototypeId = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C5941100045"),
            Hash = 44,
            Name = "Hull",
            PendingDamage = 1,
            CurrentIntegrity = 20
        };

        public static HullState Hull2 => new()
        {
            Id = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C5941100144"),
            PrototypeId = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C5941100145"),
            Hash = 144,
            Name = "Hull2",
            PendingDamage = 0,
            CurrentIntegrity = 2
        };

        public static States.Systems.ReactorState Reactor1 => new()
        {
            Id = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C5941100046"),
            PrototypeId = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C5941100047"),
            Hash = 46,
            Name = "I am a Reactor",
            CurrentState = Model.ReactorState.Attack,
            PreviousState = Model.ReactorState.Cruise,
            UsingEmergencyPower = false,
            UsedEmergencyPowerLastTurn = false,
            CurrentTurnPenalty = 0,
            NextTurnPenalty = 1,
            TurnsSpentAtAttackPower = 1,
        };

        public static States.Systems.ReactorState Reactor2 => new()
        {
            Id = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C5941100146"),
            PrototypeId = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C5941100147"),
            Hash = 146,
            Name = "I am a Reactor 2",
            CurrentState = Model.ReactorState.Cruise,
            PreviousState = Model.ReactorState.Cruise,
            UsingEmergencyPower = true,
            UsedEmergencyPowerLastTurn = false,
            CurrentTurnPenalty = 2,
            NextTurnPenalty = 0,
            TurnsSpentAtAttackPower = 0,
        };

        public static ShieldsState Shield1 => new()
        {
            Id = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C5941100048"),
            PrototypeId = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C5941100049"),
            Hash = 48,
            Name = "Shields",
            MaxPowerAvailable = 14,
            AllocatedPower = 10,
            CurrentPower = 5,
        };

        public static ShieldsState Shield2 => new()
        {
            Id = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C5941100148"),
            PrototypeId = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C5941100149"),
            Hash = 148,
            Name = "Shields2",
            MaxPowerAvailable = 20,
            AllocatedPower = 15,
            CurrentPower = 15,
        };

        public static SystemStateBase Drive1 => new()
        {
            Id = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C594110004A"),
            PrototypeId = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C594110004B"),
            Hash = 50,
            Name = "Drive",
        };

        public static SystemStateBase Drive2 => new()
        {
            Id = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C594110014A"),
            PrototypeId = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C594110014B"),
            Hash = 150,
            Name = "Drive2",
        };

        public static EnergyWeaponState EnergyWeapon1 => new()
        {
            Id = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C594110004C"),
            PrototypeId = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C594110004D"),
            Hash = 51,
            Name = "Phasor",
            CurrentMaxDice = 12,
        };

        public static EnergyWeaponState EnergyWeapon2 => new()
        {
            Id = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C594110004E"),
            PrototypeId = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C594110004F"),
            Hash = 52,
            Name = "Photon Torpedo",
            CurrentMaxDice = 5,
        };
        public static EnergyWeaponState EnergyWeapon3 => new()
        {
            Id = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C594110014E"),
            PrototypeId = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C594110014F"),
            Hash = 53,
            Name = "Beam",
            CurrentMaxDice = 3,
        };

        public static OrdinanceState Ordinance1 => new()
        {
            Id = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C5941100050"),
            PrototypeId = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C5941100051"),
            Hash = 53,
            Name = "Torpedo 1",
            LaunchVelocity = new HexVector2(1, 4),
            Loaded = true,
            Loading = false,
            UsesRemaining = -1
        };

        public static OrdinanceState Ordinance2 => new()
        {
            Id = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C5941100150"),
            PrototypeId = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C5941100151"),
            Hash = 53,
            Name = "Bomb",
            LaunchVelocity = default,
            Loaded = true,
            Loading = false,
            UsesRemaining = 2
        };

        public static ShipState Ship1 => new()
        {
            Id          = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C5941100042"),
            PrototypeId = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C5941100043"),
            Hash        = 42,
            Name        = "I am a Ship",
            TeamNumber  = 1,
            Position    = new HexVector2(0, 1),
            Velocity    = new HexVector2(-2, -1),
            Hull        = Hull1,
            Reactor     = Reactor1,
            Shields     = Shield1,
            Drive       = Drive1,
            EnergyWeapons =
            [
                EnergyWeapon1,
                EnergyWeapon2,
            ],
            Ordinances =
            [
                Ordinance1
            ],
        };

        public static ShipState Ship2 => new()
        {
            Id          = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C5941100142"),
            PrototypeId = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C5941100143"),
            Hash        = 142,
            Name        = "I am a Ship too",
            TeamNumber  = 2,
            Position    = new HexVector2(52, 100),
            Velocity    = new HexVector2(-8, 5),
            Hull        = Hull2,
            Reactor     = Reactor2,
            Shields     = Shield2,
            Drive       = Drive2,
            EnergyWeapons =
            [
                EnergyWeapon3,
                EnergyWeapon2,
            ],
            Ordinances =
            [
                Ordinance1,
                Ordinance2
            ],
        };
    }
}
