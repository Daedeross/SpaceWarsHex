using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Interfaces.Systems;
using System;

namespace SpaceWarsHex.Systems
{
    /// <summary>
    /// Implementation of <see cref="IShields"/>
    /// </summary>
    public class Shields : SystemBase, IShields
    {
        /// <inheritdoc />
        public int MaxPower { get; protected set; }

        private int _maxPowerAvailable;
        /// <inheritdoc />
        public int MaxPowerAvailable => _maxPowerAvailable;

        /// <inheritdoc />
        public int AllocatedPower { get; set; }

        /// <inheritdoc />
        public int CurrentPower { get; set; }

        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="prototype">The shield prototype to use.</param>
        public Shields(IShieldsPrototype prototype)
            : base(prototype)
        {
            MaxPower = prototype.MaxPower;
            _maxPowerAvailable = MaxPower;

            AllocatedPower = 0;
            CurrentPower = 0;
        }

        /// <inheritdoc />
        public override void ApplyDamage(int currentHull, int maxHull)
        {
            var multiplier = _damageThresholds.GetThresholdMultiplier(currentHull, maxHull);
            _maxPowerAvailable = Convert.ToInt32(Math.Floor(multiplier * MaxPower));
        }

        /// <inheritdoc />
        public override void HandleEndOfTurn(int turnNumber)
        {
            AllocatedPower = 0;
            CurrentPower = 0;
        }
    }
}
