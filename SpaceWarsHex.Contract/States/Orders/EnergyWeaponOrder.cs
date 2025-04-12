using ProtoBuf;
using SpaceWarsHex.Model;
using System;

namespace SpaceWarsHex.States.Orders
{
    [ProtoContract]
    public class EnergyWeaponOrder : WeaponOrder
    {
        [ProtoMember(1)]
        public int Power { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(TurnNumber, WeaponIndex, TargetId, TargetHex,
                Velocity, Orientation6, Orientation12, Power);
        }

        #region Factories

        public static EnergyWeaponOrder DirectFire(int index, int power, Guid target)
        {
            return new EnergyWeaponOrder
            {
                WeaponIndex = index,
                Power = power,
                TargetId = target
            };
        }

        public static EnergyWeaponOrder Beam(int index, int power, Direction12 direction)
        {
            return new EnergyWeaponOrder
            {
                WeaponIndex = index,
                Power = power,
                Orientation12 = direction
            };
        }

        public static EnergyWeaponOrder Bomb(int index, int power, HexVector2 target)
        {
            return new EnergyWeaponOrder
            {
                WeaponIndex = index,
                Power = power,
                TargetHex = target
            };
        }

        public static EnergyWeaponOrder Pulse(int index, int power)
        {
            return new EnergyWeaponOrder
            {
                WeaponIndex = index,
                Power = power,
            };
        }

        public static EnergyWeaponOrder Torpedo(int index, int power, HexVector2 velocity)
        {
            return new EnergyWeaponOrder
            {
                WeaponIndex = index,
                Power = power,
                Velocity = velocity
            };
        }

        public static EnergyWeaponOrder Wall(int index, int power, HexVector2 velocity, Direction6 direction)
        {
            return new EnergyWeaponOrder
            {
                WeaponIndex = index,
                Power = power,
                Velocity = velocity,
                Orientation6 = direction
            };
        }

        public static EnergyWeaponOrder Wave(int index, int power)
        {
            return new EnergyWeaponOrder
            {
                WeaponIndex = index,
                Power = power,
            };
        }

        public static EnergyWeaponOrder Clear()
        {
            return new EnergyWeaponOrder
            {
                WeaponIndex = -1
            };
        }

        #endregion
    }
}
