using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Interfaces.Orders;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Interfaces.Rules;
using SpaceWarsHex.Interfaces.Systems;
using SpaceWarsHex.Model;
using SpaceWarsHex.Systems;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace SpaceWarsHex.Entities
{
    /// <inheritdoc />
    public partial class Ship : MovingHexObject, IShip
    {
        protected const int ShieldSavePower = 4;
        protected const string Accepted = "Accepted";
        protected const string OldVelocityName = "VelArrow";
        protected const string AccelerationName = "AccArrow";
        protected const string NewVelocityName = "VelArrowNext";
        protected const string ValidStateMessage = "";
        protected const string TooMuchPower = "Too much power allocated.";

        //[Inject] private readonly IGodotHexMath HexMath;

        protected IRules _rules;

        #region IShip

        #region Overrides

        #endregion

        #region Systems
        protected List<ISystem> _allSystems;
        protected Reactor _reactor;
        protected Drive _drive;
        protected Shields _shields;
        protected Hull _hull;
        protected List<IEnergyWeapon> _energyWeapons;
        protected List<IOrdinance> _ordinances;

        public IReactor Reactor => _reactor;
        public IDrive Drive => _drive;
        public IShields Shields => _shields;
        public IHull Hull => _hull;
        public IReadOnlyList<IEnergyWeapon> EnergyWeapons => _energyWeapons;
        public IReadOnlyList<IOrdinance> Ordinances => _ordinances;

        #endregion // Systems

        #region Orders

        protected IOrdinanceOrder?[] _ordinanceOrders = [];

        private IEnergyWeaponOrder? m_CurrentEnergyWeaponOrder;
        public IEnergyWeaponOrder? CurrentEnergyWeaponOrder
        {
            get => m_CurrentEnergyWeaponOrder;
            private set => this.RaiseAndSetIfChanged(ref m_CurrentEnergyWeaponOrder, value);
        }
        public IReadOnlyCollection<IOrdinanceOrder?> CurrentOrdinanceOrders => _ordinanceOrders;

        #endregion // Orders

        /// <summary>
        /// DI endpoint since we can't call the constructor directly.
        /// </summary>
        /// <param name="shipPrototype">The prototype defining the ship and its systems.</param>
        /// <param name="rules"><see cref="IRules"/></param>
        /// <returns>this</returns>
        public Ship(IShipPrototype shipPrototype, IRules rules)
        {
            _rules = rules ?? throw new ArgumentNullException(nameof(rules));

            Name = shipPrototype.Name;

            _hull = SystemFactory.Create(shipPrototype.Hull);
            _hull.PropertyChanged += BubblePropertyChanged;

            _reactor = SystemFactory.Create(shipPrototype.Reactor);
            _drive = SystemFactory.Create(shipPrototype.Drive);
            _shields = SystemFactory.Create(shipPrototype.Shields);

            _allSystems = new List<ISystem>
            {
                _reactor, _drive, _shields
            };

            InitializeEnergyWeapons(shipPrototype.EnergyWeapons);
            InitializeOrdinances(shipPrototype.Ordinances);
            InitializeValidOrders();
            RecalcPowerAllocation();

            foreach (ISystem system in _allSystems)
            {
                if (system is INotifyPropertyChanged np)
                {
                    np.PropertyChanged += BubblePropertyChanged;
                }
            }
        }

        [MemberNotNull(nameof(_energyWeapons))]
        private void InitializeEnergyWeapons(IEnumerable<IEnergyWeaponPrototype> energyWeapons)
        {
            _energyWeapons = energyWeapons?
                .Select(proto => SystemFactory.Create(proto))
                .ToList()
                ?? new List<IEnergyWeapon>();

            _allSystems.AddRange(_energyWeapons);
        }

        [MemberNotNull(nameof(_ordinances))]
        [MemberNotNull(nameof(_ordinanceOrders))]
        private void InitializeOrdinances(IEnumerable<IOrdinancePrototype> ordinances)
        {
            _ordinances = ordinances?
                .Select(proto => SystemFactory.Create(proto))
                .ToList()
                ?? new List<IOrdinance>();

            _ordinanceOrders = new IOrdinanceOrder[_ordinances.Count];

            _allSystems.AddRange(_ordinances);
        }

        [MemberNotNull(nameof(ValidOrders))]
        private void InitializeValidOrders()
        {
            var set = new HashSet<OrderKind> { OrderKind.MoveOrder, OrderKind.ShieldsOrder, OrderKind.ReactorOrder };
            set.UnionWith(_energyWeapons.Select(w => w.FireMode.ToOrderKind()));
            set.UnionWith(_ordinances.Select(w =>
                w switch
                {
                    ITorpedoLauncher => OrderKind.TorpedoOrder,
                    IBombLauncher => OrderKind.BombOrder,
                    _ => throw new NotImplementedException()
                }));

            ValidOrders = set;
        }

        #region IDamageable

        public void ApplyDamage(DamageKind damageKind, int damageAmount, bool saveShields)
        {
            switch (damageKind)
            {
                case DamageKind.Energy:
                    ApplyPhasorDamage(damageAmount, saveShields);
                    break;
                case DamageKind.Physical:
                    ApplyConcussionDamage(damageAmount);
                    break;
                case DamageKind.Zap:
                    ApplyZapDamage(damageAmount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(damageKind), $"Unrecognized damage type: {damageKind}");
            }
        }

        private void ApplyPhasorDamage(int damageAmount, bool saveShields)
        {
            if (Shields.CurrentPower < 4)
            {
                saveShields = false;
            }

            var hullDamage = _rules.PhasorTable.GetHullDamage(damageAmount, Shields.CurrentPower, saveShields);
            var newShields = _rules.PhasorTable.GetNewShields(damageAmount, Shields.CurrentPower, saveShields);
            var energyPenalty = _rules.PhasorTable.GetEnergyPenalty(damageAmount, Shields.CurrentPower, saveShields);

            Hull.AssignDamage(hullDamage);
            Reactor.NextTurnPenalty += energyPenalty;
            Shields.CurrentPower = newShields;
        }

        private void ApplyConcussionDamage(int damageAmount)
        {
            var hullDamage = _rules.ConcussionTable.GetHullDamage(damageAmount, Shields.CurrentPower);
            Hull.AssignDamage(hullDamage);
        }

        private void ApplyZapDamage(int damageAmount)
        {
            throw new NotImplementedException();
        }

        #endregion // IDamageable

        #region ISelectable

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => RaiseAndSetIfChanged(ref _selected, value);
        }

        #endregion // ISelectable

        #region Order Handling

        public IReadOnlyCollection<OrderKind> ValidOrders { get; private set; } = [];
        public bool ValidState { get; private set; }
        public string StateMessage { get; private set; } = ValidStateMessage;

        protected readonly Dictionary<int, IReadOnlyCollection<IOrder>> _orderHistory = new();
        protected IMoveOrder? _currentMoveOrder;
        protected IReactorOrder? _currentReactorOrder;
        protected IShieldOrder? _currentShieldOrder;

        public IReadOnlyDictionary<int, IReadOnlyCollection<IOrder>> GetAllOrders()
        {
            return _orderHistory;
        }

        public IReadOnlyCollection<IOrder> GetOrders(int turnNumber)
        {
            return _orderHistory.TryGetValue(turnNumber, out var orders)
                ? orders
                : [];
        }

        public IReadOnlyCollection<TOrder> GetOrders<TOrder>(int turnNumber) where TOrder : IOrder
        {
            return GetOrders(turnNumber)
                .Where(o => o is TOrder)
                .Cast<TOrder>()
                .ToList();
        }

        public IReadOnlyCollection<IOrder> GetCurrentTurnOrders()
        {
            List<IOrder> result = [];

            if (CurrentEnergyWeaponOrder != null)
            {
                result.Add(CurrentEnergyWeaponOrder);
            }

#pragma warning disable CS8620 // Just look at it, the analyzer is stupod.
            result.AddRange(_ordinanceOrders.Where(o => o != null));
#pragma warning restore CS8620

            return result;
        }

        public OrderResult GiveOrder<TOrder>(TOrder order) where TOrder : IOrder
        {
            return order switch
            {
                IMoveOrder moveOrder => GiveOrder(moveOrder),
                IReactorOrder reactorOrder => GiveOrder(reactorOrder),
                IShieldOrder shieldOrder => GiveOrder(shieldOrder),
                IWeaponOrder weaponOrder => GiveOrder(weaponOrder),
                _ => new OrderResult { Message = $"Unsupported order type: {typeof(TOrder)}", Status = OrderStatus.NotAllowed }
            };
        }

        public OrderResult GiveOrder(IReactorOrder order)
        {
            // is unchanged?
            if (order.UseEmergencyPower && _reactor.UsingEmergencyPower
             && order.State == _reactor.CurrentState)
            {
                return OrderResult.NotModified(Accepted);
            }

            // validate
            if (order.UseEmergencyPower && _reactor.UsedEmergencyPowerLastTurn)
            {
                return OrderResult.NotValid("Cannot use emergency power two turns in a row.");
            }
            if (order.State == ReactorState.Attack && _reactor.TurnsSpentAtAttackPower >= _reactor.MaxTurnsAtAttackPower)
            {
                return OrderResult.NotValid("Ship has spent its maximum number of turns at attack power.");
            }

            _reactor.CurrentState = order.State;
            _reactor.UsingEmergencyPower = order.UseEmergencyPower;

            RecalcPowerAllocation();

            return new OrderResult { Message = Accepted, Status = OrderStatus.Ok };
        }

        public OrderResult GiveOrder(IMoveOrder order)
        {
            if (order.Acceleration == _drive.Acceleration)
            {
                return OrderResult.NotModified(Accepted);
            }

            // validate
            if (order.Acceleration.Length() > _drive.AccelerationClass)
            {
                return OrderResult.NotValid("Cannot exceed Accerlation Class");
            }

            var newVelocity = _drive.Velocity + order.Acceleration;
            if (newVelocity.Length() > _drive.MaxWarp)
            {
                return OrderResult.NotValid("Cannot exceed Max Marp");
            }

            _drive.Acceleration = order.Acceleration;
            RecalcPowerAllocation();

            return OrderResult.Ok(Accepted);
        }

        public OrderResult GiveOrder(IShieldOrder order)
        {
            if (order.Power == _shields.AllocatedPower)
            {
                return new OrderResult { Message = Accepted, Status = OrderStatus.NotModified };
            }

            if (order.Power > _shields.MaxPowerAvailable)
            {
                return new OrderResult { Message = "Cannot exceed curent max shield strength", Status = OrderStatus.NotValid };
            }

            if (order.Power < 0)
            {
                return OrderResult.NotValid("Cannot have negative shields.");
            }

            _shields.AllocatedPower = order.Power;
            RecalcPowerAllocation();

            return new OrderResult { Message = Accepted, Status = OrderStatus.Ok };
        }

        #region Private Order Handlers

        private OrderResult GiveOrder(IWeaponOrder weaponOrder)
        {
            return weaponOrder switch
            {
                IEnergyWeaponOrder energyWeaponOrder => GiveOrder(energyWeaponOrder),
                IOrdinanceOrder ordinanceOrder => GiveOrder(ordinanceOrder),
                _ => throw new NotImplementedException()
            };
        }

        private OrderResult GiveOrder(IEnergyWeaponOrder order)
        {
            // Negative index is the signal to clear the current order.
            if (order.WeaponIndex < 0)
            {
                CurrentEnergyWeaponOrder = null;
                RecalcPowerAllocation();
                return OrderResult.Ok();
            }

            IEnergyWeapon weapon = _energyWeapons[order.WeaponIndex];

            if (!ValidateEnergyWeaponOrder(weapon, order))
            {
                return OrderResult.NotValid();
            }

            var result = WeaponOrder(weapon, order);
            if (result.Success())
            {
                CurrentEnergyWeaponOrder = order;
                RecalcPowerAllocation();
            }

            return result;
        }

        public OrderResult GiveOrder(IOrdinanceOrder order)
        {
            var ordinance = _ordinances[order.WeaponIndex];
            if (!ValidateOrdinanceOrder(ordinance, order))
            {
                return OrderResult.NotValid("No uses remaining.");
            }

            var result = WeaponOrder(ordinance, order);
            if (result.Success())
            {
                AssignOrdinanceOrder(order);
            }

            return result;
        }

        private OrderResult WeaponOrder(IWeapon system, IWeaponOrder order)
        {
            return system switch
            {
                IEnergyWeapon => OrderResult.Ok(),
                ITorpedoLauncher torepdo => TorpedoOrder(torepdo, order),
                IBombLauncher bomb => BombOrder(bomb, order),
                _ => throw new NotImplementedException()
            };
        }

        private OrderResult TorpedoOrder(ITorpedoLauncher launcher, IWeaponOrder order)
        {
            if (order is IOrdinanceOrder fullOrder)
            {
                if (fullOrder.Load == OridinanceLoad.Load || fullOrder.Load == OridinanceLoad.Unload)
                {
                    if (launcher.LoadFire)
                    {
                        return OrderResult.NotValid("Torpedo Launcher does not need loading.");
                    }

                    launcher.Loading = !launcher.Loading;

                    return OrderResult.Ok();
                }
                else if (fullOrder.Load == OridinanceLoad.Fire)
                {
                    if (launcher.Loaded || launcher.LoadFire)
                    {
#pragma warning disable CS8629 // Nullable value type may be null.
                        var speed = fullOrder.Velocity.Value.Length();
#pragma warning restore CS8629 // Nullable value type may be null.
                        if (speed < launcher.MinWarp || speed > launcher.MaxWarp)
                        {
                            return OrderResult.NotValid("Invalid launch velocity.");
                        }

                        launcher.LaunchVelocity = fullOrder.Velocity;

                        return OrderResult.Ok();
                    }

                    return OrderResult.NotValid("Torpedo Launcher not ready to fire");
                }
            }

            return OrderResult.NotValid();
        }

        private OrderResult BombOrder(IBombLauncher bomb, IWeaponOrder order)
        {
            if (!order.TargetHex.HasValue)
            {
                return OrderResult.NotValid();
            }

            var distance = (order.TargetHex.Value - Position).Length();
            if (distance > bomb.MaxRange)
            {
                return OrderResult.NotValid();
            }

            bomb.TargetHex = order.TargetHex;

            return OrderResult.Ok();
        }

        private static bool ValidateEnergyWeaponOrder(IEnergyWeapon weapon, IEnergyWeaponOrder order)
        {
            return order.Power % weapon.EnergyPerDie == 0
                && order.Power / weapon.EnergyPerDie <= weapon.CurrentMaxDice;
        }

        private static bool ValidateOrdinanceOrder(IOrdinance ordinance, IOrdinanceOrder order)
        {
            return ordinance.UsesRemaining > 0;
        }

        private void AssignOrdinanceOrder(IOrdinanceOrder order)
        {
            _ordinanceOrders[order.WeaponIndex] = order;
        }

        private void RecalcPowerAllocation()
        {
            var allocated = _drive.Acceleration.Length();
            allocated += _shields.AllocatedPower;
            if (CurrentEnergyWeaponOrder != null)
            {
                allocated += CurrentEnergyWeaponOrder.Power;
            }
            _reactor.PowerAllocated = allocated;

            if (allocated > _reactor.CurrentAvailablePower)
            {
                ValidState = false;
                StateMessage = TooMuchPower;
            }
            else
            {
                ValidState = true;
                StateMessage = ValidStateMessage;
            }
        }

        #endregion // Private Order Handlers

        #region IFireWeapons

        public IReadOnlyCollection<(IWeapon, IWeaponOrder)> GetFiringWeapons(TurnPhase turnPhase)
        {
            var result = new List<(IWeapon, IWeaponOrder)> ();
            if (CurrentEnergyWeaponOrder != null)
            {
                var weapon = EnergyWeapons[CurrentEnergyWeaponOrder.WeaponIndex];
                result.Add((weapon, CurrentEnergyWeaponOrder));
            }

            for (int i = 0; i < _ordinanceOrders.Length; i++)
            {
                var order = _ordinanceOrders[i];
                if (order != null)
                {
                    result.Add((Ordinances[i], order));
                }
            }

            return result
                .Where(x => x.Item1.FirePhase == turnPhase)
                .ToList();
        }

        #endregion // IFireWeapons

        #endregion // Order Handling

        #region TurnPhase Handling

        public override bool HandleEndOfPhase(TurnPhase turnPhase)
        {
            switch (turnPhase)
            {
                case TurnPhase.TurnStart:
                    break;
                case TurnPhase.Orders:
                    OrdersEnd();
                    break;
                case TurnPhase.Weapons1:
                    break;
                case TurnPhase.Displacement:
                    break;
                case TurnPhase.Weapons2:
                    break;
                case TurnPhase.ApplyDamage1:
                    return ApplyDamage();
                case TurnPhase.Countermeasures:
                    break;
                case TurnPhase.Cloaking:
                    break;
                case TurnPhase.Movement:
                    Movement();
                    break;
                case TurnPhase.ApplyDamage2:
                    return ApplyDamage();
                case TurnPhase.TurnEnd:
                    break;
                default:
                    break;
            }

            return false;
        }

        public override bool HandleEndOfTurn(int turnNumber)
        {
            foreach (var system in _allSystems)
            {
                system.HandleEndOfTurn(turnNumber);
            }

            ArchiveAndClearOrders(turnNumber);
            SetArrows();
            ShowArrows();

            return _hull.CurrentIntegrity >= 0;
        }

        private void ArchiveAndClearOrders(int turnNumber)
        {
            var orders = new List<IOrder?>()
            {
                _currentMoveOrder,
                _currentReactorOrder,
                _currentShieldOrder,
                CurrentEnergyWeaponOrder
            };

            _orderHistory[turnNumber] = [.. orders.Where(o => o != null).Cast<IOrder>()];

            _currentMoveOrder = null;
            _currentReactorOrder = null;
            _currentShieldOrder = null;
            CurrentEnergyWeaponOrder = null;
            for (int i = 0; i < _ordinanceOrders.Length; i++)
            {
                _ordinanceOrders[i] = null;
            }
        }

        /// <summary>
        /// Finalize Orders
        /// </summary>
        private void OrdersEnd()
        {
            _shields.CurrentPower = _shields.AllocatedPower;
            _drive.Velocity += Drive.Acceleration;

            HideArrows();
        }

        private void Weapons1()
        {

        }

        private void Displacement()
        {

        }

        private void Movement()
        {
            _drive.Acceleration = HexVector2.Zero;
            //SetArrows();
        }

        private bool ApplyDamage()
        {
            Hull.ApplyDamage();

            if (Hull.CurrentIntegrity <= 0)
            {
                return true;
            }

            foreach (var system in _allSystems)
            {
                system.ApplyDamage(Hull);
            }

            return false;
        }

        #endregion // TurnPhase Handling

        #endregion // IShip

        #region TODO : Move to engine-specific wrapper
        private void HideArrows()
        {
            //_accArrow.gameObject.SetActive(false);
            //_oldVelArrow.gameObject.SetActive(false);
            //_newVelArrow.gameObject.SetActive(false);
        }
        private void ShowArrows()
        {
            //_accArrow.gameObject.SetActive(true);
            //_oldVelArrow.gameObject.SetActive(true);
            //_newVelArrow.gameObject.SetActive(true);
        }

        private void SetArrows()
        {
            //var newVel = Velocity + _drive.Acceleration;
            //_accArrow.Set(Velocity, newVel);
            //_oldVelArrow.TargetHex = Velocity;
            //_newVelArrow.TargetHex = newVel;
        }
        #endregion
    }
}
