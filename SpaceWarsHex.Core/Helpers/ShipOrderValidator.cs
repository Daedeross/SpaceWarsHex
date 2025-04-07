using SpaceWars.Interfaces;
using SpaceWars.Interfaces.Orders;
using SpaceWars.Interfaces.Systems;
using SpaceWars.Model;

namespace SpaceWars.Helpers
{
    public class ShipOrderValidator
    {
        protected const string Accepted = "Accepted";

        public static ShipOrderValidator Instance { get; private set; } = new ShipOrderValidator();

        public OrderResult Validate(IShip ship, IOrder order)
        {
            return order switch
            {
                IWeaponOrder weaponOrder => Validate(ship, weaponOrder),
                //IReactorOrder reactorOrder => Validate(ship, reactorOrder),
                _ => OrderResult.Ok(Accepted),
            };
        }

        public OrderResult Validate(IShip ship, IWeaponOrder order)
        {
            if (order is IEnergyWeaponOrder ewo)
            {
                // Only check upper bounds of index. Negative index is signal
                // to clear current order.
                if (order.WeaponIndex >= ship.EnergyWeapons.Count)
                {
                    return OrderResult.NotValid("Invalid WeaponIndex");
                }

                var weapon = ship.EnergyWeapons[order.WeaponIndex];
                return ValidateEnergyWeapon(weapon, ewo);

            }
            else if (order is IOrdinanceOrder ord)
            {
                if (order.WeaponIndex < 0 || order.WeaponIndex >= ship.Ordinances.Count)
                {
                    return OrderResult.NotValid("Invalid WeaponIndex");
                }

                var weapon = ship.Ordinances[order.WeaponIndex];
                return ValidateOrdinance(weapon, ord);
            }
            else
            {
                throw new NotSupportedException("Unknown order type received.");
            }
        }

        #region Private Helpers

        private OrderResult ValidateEnergyWeapon(IEnergyWeapon weapon, IEnergyWeaponOrder order)
        {
            if (weapon.CurrentMaxEnergy() < order.Power
                || order.Power % weapon.EnergyPerDie != 0)
            {
                return OrderResult.NotValid("Invalid energy allocated.");
            }
                
            return ValidateFireMode(weapon, order);
        }

        private OrderResult ValidateOrdinance(IOrdinance ordinance, IOrdinanceOrder order)
        {
            if (ordinance.UsesRemaining == 0) // TODO: negative uses means infinite?
            {
                return OrderResult.NotValid("Out of uses.");
            } 
            return ValidateFireMode(ordinance, order);
        }

        private OrderResult ValidateFireMode(IWeapon weapon, IWeaponOrder order)
        {
            return weapon.FireMode switch
            {
                FireMode.DirectFire => ValidateDirectFire(order),
                FireMode.Beam => ValidateBeam(order),
                FireMode.Bomb => ValidateBomb(weapon, order),
                FireMode.Pulse => OrderResult.Ok(Accepted),
                FireMode.Torpedo => ValidateTorpedo(weapon, order),
                FireMode.Wall => ValidateWall(order),
                FireMode.Wave => throw new NotImplementedException("Wave FireMode is not implemented yet"),
                _ => OrderResult.NotAllowed("Unknown fire mode.")
            };
        }

        private OrderResult ValidateDirectFire(IWeaponOrder order)
        {
            return order.TargetId.HasValue
                ? OrderResult.Ok(Accepted)
                : OrderResult.NotValid("Missing TargetId");
        }

        private OrderResult ValidateBeam(IWeaponOrder order)
        {
            return order.Orientation12.HasValue
                ? OrderResult.Ok(Accepted)
                : OrderResult.NotValid("Missing beam direction");
        }

        private OrderResult ValidateBomb(IWeapon weapon, IWeaponOrder order)
        {
            if (weapon is IBombLauncher bomb)
            {
                return order.TargetHex.HasValue && (bomb.MaxRange <= 0 || bomb.MaxRange >= order.TargetHex.Value.Length())
                    ? OrderResult.Ok(Accepted)
                    : OrderResult.NotValid("Invalid target hex");
            }

            return OrderResult.NotAllowed();
        }

        private OrderResult ValidateTorpedo(IWeapon weapon, IWeaponOrder order)
        {
            if (weapon is ITorpedoLauncher torpedo && order.Velocity.HasValue)
            {
                var speed = order.Velocity.Value.Length();
                if (   order.Velocity.HasValue
                    && speed >= torpedo.MinWarp
                    && speed <= torpedo.MaxWarp)
                {
                    return ValidateTorpedoLoadState(torpedo, order);
                }
                else
                {
                    return OrderResult.NotValid("Invalid velocity");
                }
            }

            return OrderResult.NotAllowed();
        }

        private OrderResult ValidateTorpedoLoadState(ITorpedoLauncher weapon, IWeaponOrder order)
        {
            if (order is IOrdinanceOrder ordinanceOrder)
            {
                return weapon.LoadFire
                       || (weapon.Loaded && (ordinanceOrder.Load == OridinanceLoad.Fire || ordinanceOrder.Load == OridinanceLoad.Unload))
                       || (!weapon.Loaded && ordinanceOrder.Load == OridinanceLoad.Load)
                    ? OrderResult.Ok(Accepted)
                    : OrderResult.NotValid("Invalid Load-state");
            }

            return OrderResult.NotAllowed();
        }

        private OrderResult ValidateWall(IWeaponOrder order)
        {
            return order.Velocity.HasValue && order.Orientation6.HasValue
                ? OrderResult.Ok(Accepted)
                : OrderResult.NotValid("Incomplete");
        }

        #endregion // Private Helpers
    }
}
