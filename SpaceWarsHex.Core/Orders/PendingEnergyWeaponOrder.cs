using SpaceWars.Entities;
using SpaceWars.Interfaces;
using SpaceWars.Interfaces.Orders;
using SpaceWars.Interfaces.Systems;
using SpaceWars.Model;
using System;

namespace SpaceWars.Orders
{
    /// <summary>
    /// Class to hold any parial weapon order.
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    public class PendingEnergyWeaponOrder : NotificationObject
    {
        public IShip Ship { get; init; }

        private int m_WeaponIndex;
        public int WeaponIndex
        {
            get => m_WeaponIndex;
        }

        private IEnergyWeapon? m_EnergyWeapon;
        public IEnergyWeapon? EnergyWeapon
        {
            get => m_EnergyWeapon;
            set => RaiseAndSetIfChanged(ref m_EnergyWeapon, value);
        }

        public FireMode? TargetMode => EnergyWeapon?.FireMode;

        private int? m_Power;
        public int? Power
        {
            get => m_Power;
            set => RaiseAndSetIfChanged(ref m_Power, value);
        }

        private Direction12? m_Orientation12;
        public Direction12? Orientation12
        {
            get => m_Orientation12;
            set => RaiseAndSetIfChanged(ref m_Orientation12, value);
        }

        private Direction6? m_Orientation6;
        public Direction6? Orientation6
        {
            get => m_Orientation6;
            set => RaiseAndSetIfChanged(ref m_Orientation6, value);
        }

        private HexVector2? m_Velocity;
        public HexVector2? Velocity
        {
            get => m_Velocity;
            set => RaiseAndSetIfChanged(ref m_Velocity, value);
        }

        private HexVector2? m_TargetHex;
        public HexVector2? TargetHex
        {
            get => m_TargetHex;
            set => RaiseAndSetIfChanged(ref m_TargetHex, value);
        }

        private Guid? m_TargetId;
        public Guid? TargetId
        {
            get => m_TargetId;
            set => RaiseAndSetIfChanged(ref m_TargetId, value);
        }

        private bool m_Valid;
        public bool Valid
        {
            get => m_Valid;
            set => RaiseAndSetIfChanged(ref m_Valid, value);
        }

        public PendingEnergyWeaponOrder(IShip ship, int index)
        {
            Ship = ship;
            ChangeWeapon(index);
        }

        public PendingEnergyWeaponOrder(IShip ship, IEnergyWeaponOrder order)
        {
            Ship = ship;
            if (order is null)
            {
                ChangeWeapon(-1);
            }
            else
            {
                Power = order.Power;
                Orientation12 = order.Orientation12;
                Orientation6 = order.Orientation6;
                Velocity = order.Velocity;
                TargetHex = order.TargetHex;
                TargetId = order.TargetId;
                ChangeWeapon(order.WeaponIndex);
            }
        }

        public void Reset()
        {
            Power = null;
            Orientation12 = null;
            Orientation6 = null;
            Velocity = null;
            TargetHex = null;
            TargetId = null;
            EnergyWeapon = null;
            m_WeaponIndex = -1;
            Valid = false;
        }

        public IOrder? CreateOrder()
        {
            if (!Valid)
            {
                return null;
            }

            return TargetMode switch
            {
#pragma warning disable CS8629 // Nullable value type may be null.
                FireMode.DirectFire => EnergyWeaponOrder.DirectFire(WeaponIndex, Power.Value, TargetId.Value),
                FireMode.Bomb       => EnergyWeaponOrder.Bomb(WeaponIndex, Power.Value, TargetHex.Value),
                FireMode.Beam       => EnergyWeaponOrder.Beam(WeaponIndex, Power.Value, Orientation12.Value),
                FireMode.Pulse      => EnergyWeaponOrder.Pulse(WeaponIndex, Power.Value),
                FireMode.Torpedo    => EnergyWeaponOrder.Torpedo(WeaponIndex, Power.Value, Velocity.Value),
                FireMode.Wall       => EnergyWeaponOrder.Wall(WeaponIndex, Power.Value, Velocity.Value, Orientation6.Value),
                FireMode.Wave       => EnergyWeaponOrder.Wave(WeaponIndex, Power.Value),
#pragma warning restore CS8629 // Nullable value type may be null.
                _ => throw new NotImplementedException()
            };
        }

        public OrderResult GiveOrder()
        {
            if (Valid)
            {
                return Ship.GiveOrder(CreateOrder());
            }
            else
            {
                if (WeaponIndex < 0)
                {
                    return Ship.GiveOrder(EnergyWeaponOrder.Clear());
                }
                return OrderResult.NotValid();
            }
        }

        public void ChangeWeapon(int index)
        {
            if (index >= 0 && index < Ship.EnergyWeapons.Count)
            {
                m_WeaponIndex = index;
                EnergyWeapon = Ship.EnergyWeapons[index];
                var power = Power.GetValueOrDefault(0);
                power = power / EnergyWeapon.EnergyPerDie * EnergyWeapon.EnergyPerDie;              // power must be a multiple of EnergyPerDie
                power = Math.Max(power, EnergyWeapon.EnergyPerDie);                                 // must be at least one die.
                power = Math.Min(power, EnergyWeapon.EnergyPerDie * EnergyWeapon.CurrentMaxDice);   // clamp power to max capable
                Power = power; ;
                Validate();
                return;
            }

            m_WeaponIndex = -1;
            EnergyWeapon = null;
            Valid = false;
        }

        private void Validate()
        {
            var valid = IsPowerValid() && IsValidForMode();

            Valid = valid;
        }

        private bool IsPowerValid()
        {
            return EnergyWeapon != null
                && Power.HasValue
                && Power.Value > 0
                && Power.Value % EnergyWeapon.EnergyPerDie == 0;
        }

        private bool IsValidForMode()
        {
            if (TargetMode is null)
            {
                return false;
            }
            switch (TargetMode.Value)
            {
                case FireMode.DirectFire:
                    return TargetId != null;
                case FireMode.Beam:
                    return Orientation12 != null;
                case FireMode.Bomb:
                    if (EnergyWeapon is IBombLauncher bomb)
                    {
                        if (TargetHex is null)
                        {
                            return false;
                        }
                        else
                        {
                            int max = bomb.MaxRange <= 0 ? int.MaxValue : bomb.MaxRange;
                            var distance = (TargetHex.Value - Ship.Position).Length();
                            return distance <= max;
                        }
                    }
                    return false;
                case FireMode.Torpedo:
                    return Velocity != null;
                case FireMode.Wall:
                    return !(TargetHex is null || Orientation6 is null);
                case FireMode.Pulse:
                    return true;
                case FireMode.Wave:
                    return true;
                default:
                    throw new NotSupportedException("Non-supported FireMode");
            }
        }

        #region OrderGenerators

#pragma warning disable CS8629 // Nullable value type may be null.
        private EnergyWeaponOrder DirectFire()
        {
            return new EnergyWeaponOrder
            {
                Power = Power.Value,
                TargetId = TargetId.Value,
                WeaponIndex = WeaponIndex
            };
        }

        private EnergyWeaponOrder Beam()
        {
            return new EnergyWeaponOrder
            {
                Power = Power.Value,
                Orientation12 = Orientation12.Value,
                WeaponIndex = WeaponIndex,
            };
        }

        private EnergyWeaponOrder Bomb()
        {
            return new EnergyWeaponOrder
            {
                Power = Power.Value,
                TargetHex = TargetHex.Value,
                WeaponIndex = WeaponIndex
            };
        }
#pragma warning restore CS8629 // Nullable value type may be null.
        #endregion
    }
}
