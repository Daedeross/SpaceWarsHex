using SpaceWars.Interfaces.Orders;
using SpaceWars.Interfaces.Prototypes;
using SpaceWars.Interfaces.Systems;
using SpaceWars.Model;
using System.Collections.Generic;

namespace SpaceWars.Systems
{
    /// <summary>
    /// Implementation of standard ammo-based torpedo launcher
    /// </summary>
    public class TorpedoTube : WeaponBase, IOrdinance, ITorpedoLauncher
    {
        private static OrderKind[] _validOrders = new[] { OrderKind.TorpedoOrder };

        private readonly Dictionary<int, IReadOnlyCollection<IOrder>> _allOrders = new Dictionary<int, IReadOnlyCollection<IOrder>>();
        private IOrder? _currentOrder;

        #region IOrdinance
        /// <inheritdoc />
        public int Strength { get; private set; }
        /// <inheritdoc />
        public int MaxUses { get; private set; }
        /// <inheritdoc />
        public int UsesRemaining { get; set; }

        #endregion // IOrdinance

        #region ITorpedoLauncher
        /// <inheritdoc />
        public int MinWarp { get; private set; }
        /// <inheritdoc />
        public int MaxWarp { get; private set; }
        /// <inheritdoc />
        public bool Homing { get; private set; }
        /// <inheritdoc />
        public bool LoadFire { get; private set; }
        /// <inheritdoc />
        public bool Loaded { get; set; }

        #endregion // ITorpedoLauncher

        #region Order State

        private bool _loading;
        /// <inheritdoc />
        public bool Loading
        {
            get => _loading;
            set
            {
                if (!LoadFire)
                {
                    _loading = value;
                }
            }
        }

        private HexVector2? _launchVelocity;
        /// <inheritdoc />
        public HexVector2? LaunchVelocity
        {
            get => _launchVelocity;
            set
            {
                if (Loaded || LoadFire)
                {
                    _launchVelocity = value;    // must be validated by the ship
                }
            }
        }

        #endregion // Order State

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="prototype"></param>
        public TorpedoTube(ITorpedoTubePrototype prototype)
            : base(prototype)
        {
            Strength = prototype.Strength;
            MaxUses = prototype.MaxUses;
            UsesRemaining = MaxUses;
            MinWarp = prototype.MinWarp;
            MaxWarp = prototype.MaxWarp;
            Homing = prototype.Homing;
            LoadFire = prototype.LoadFire;
            Loaded = false;
        }

        #region ISystem
        /// <inheritdoc />
        public override void ApplyDamage(int currentHull, int maxHull)
        {
            var multiplier = _damageThresholds.GetThresholdMultiplier(currentHull, maxHull);
            if (multiplier < 1)
            {
                UsesRemaining = 0;
            }
        }
        /// <inheritdoc />
        public override void HandleEndOfTurn(int turnNumber)
        {
            if (Loading)
            {
                Loading = false;
                Loaded = !Loaded;
            }
            if (LaunchVelocity.HasValue)
            {
                UsesRemaining--;
                LaunchVelocity = default;
            }

            if (_currentOrder != null)
            {
                _allOrders[turnNumber] = new[] { _currentOrder };
            }
            else
            {
                _allOrders[turnNumber] = new IOrder[0];
            }
        }

        #endregion // ISystem
    }
}
