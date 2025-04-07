using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceWarsHex.Model
{
    /// <summary>
    /// Compares <see cref="WeaponDamageInstance"/>s by the criteria in Issue #7
    /// </summary>
    public class WeaponDamageInstanceComparer : IComparer<WeaponDamageInstance>
    {
        /// <summary>
        /// Default singleton for comparer.
        /// </summary>
        public static IComparer<WeaponDamageInstance> Default { get; } = new WeaponDamageInstanceComparer();

        private bool IsPureEnergy(WeaponDamageInstance instance)
        {
            return instance.DamageInstances
                .All(i => i.DamageKind == DamageKind.Energy);
        }

        private bool IsEnergyFirst(WeaponDamageInstance instance)
        {
            return instance.DamageInstances
                .First().DamageKind == DamageKind.Energy;
        }

        private int EnergyDamage(WeaponDamageInstance instance)
        {
            return instance.DamageInstances
                .FirstOrDefault(i => i.DamageKind == DamageKind.Energy)?.DamageValue ?? 0;
        }

        private int PhysicalDamage(WeaponDamageInstance instance)
        {
            return instance.DamageInstances
                .FirstOrDefault(i => i.DamageKind == DamageKind.Physical)?.DamageValue ?? 0;
        }

        private int ZapDamage(WeaponDamageInstance instance)
        {
            return instance.DamageInstances
                .FirstOrDefault(i => i.DamageKind == DamageKind.Zap)?.DamageValue ?? 0;
        }

        /// <summary>
        /// Pure-Energy -> Energy-First -> Energy-Damage -> Physical-Damage -> Zap-Damage
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public int Compare(WeaponDamageInstance x, WeaponDamageInstance y)
        {
            if (x is null)
            {
                throw new ArgumentNullException(nameof(x));
            }
            if (y is null)
            {
                throw new ArgumentNullException(nameof(y));
            }

            if (x.DamageInstances.Count == 0)
            {
                throw new ArgumentException(nameof(x.DamageInstances), "Cannot have empty damage");
            }
            if (y.DamageInstances.Count == 0)
            {
                throw new ArgumentException(nameof(y.DamageInstances), "Cannot have empty damage");
            }

            if (IsPureEnergy(x))
            {
                if (!IsPureEnergy(y))
                {
                    return 1;
                }
            }
            else if (IsPureEnergy(y))
            {
                return -1;
            }

            if (IsEnergyFirst(x))
            {
                if (!IsEnergyFirst(y))
                {
                    return 1;
                }
            }
            else if (IsEnergyFirst(y))
            {
                return -1;
            }

            int xDamage = EnergyDamage(x);
            int yDamage = EnergyDamage(y);
            if (xDamage > yDamage)
            {
                return 1;
            }
            else if (yDamage > xDamage)
            {
                return -1;
            }

            xDamage = PhysicalDamage(x);
            yDamage = PhysicalDamage(y);
            if (xDamage > yDamage)
            {
                return 1;
            }
            else if (yDamage > xDamage)
            {
                return -1;
            }

            return ZapDamage(x).CompareTo(ZapDamage(y));
        }
    }
}
