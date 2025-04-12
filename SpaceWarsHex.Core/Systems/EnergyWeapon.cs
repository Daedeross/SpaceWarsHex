using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Interfaces.Systems;
using SpaceWarsHex.Helpers;
using SpaceWarsHex.Model;
using System;
using System.Collections.Generic;

namespace SpaceWarsHex.Systems
{
    /// <summary>
    /// Abstract base class for all energy weapons.
    /// </summary>
    public class EnergyWeapon : WeaponBase, IEnergyWeapon
    {
        /// <inheritdoc />
        public int InitialMaxDice { get; private set; }

        /// <inheritdoc />
        public int CurrentMaxDice { get; private set; }

        /// <inheritdoc />
        public bool Visual { get; private set; }

        /// <inheritdoc />
        public int EnergyPerDie { get; private set; }

        /// <inheritdoc />
        public IReadOnlyList<WeaponEffect> Effects { get; private set; }

        /// <summary>
        /// Public root constructor for all <see cref="EnergyWeapon"/>s.
        /// </summary>
        /// <param name="prototype">The <see cref="IEnergyWeaponPrototype"/> used to create the system.</param>
        public EnergyWeapon(IEnergyWeaponPrototype prototype)
            : base(prototype)
        {
            Name = prototype.Name;
            InitialMaxDice = prototype.MaxDice;
            CurrentMaxDice = prototype.MaxDice;
            Visual = prototype.Visual;
            EnergyPerDie = prototype.EnergyPerDie;
            Effects = prototype.Effects;
        }

        /// <inheritdoc />
        public override void ApplyDamage(int currentHull, int maxHull)
        {
            var multiplier = _damageThresholds.GetThresholdMultiplier(currentHull, maxHull);
            CurrentMaxDice = Convert.ToInt32(Math.Floor(multiplier * InitialMaxDice));
        }

        /// <inheritdoc />
        public override void HandleEndOfTurn(int turnNumber)
        {

        }
    }
}
