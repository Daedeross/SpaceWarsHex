namespace SpaceWars
{
    using SpaceWars.Helpers;
    using SpaceWars.Interfaces;
    using SpaceWars.Interfaces.Bridges;
    using SpaceWars.Interfaces.Orders;
    using SpaceWars.Interfaces.Prototypes;
    using SpaceWars.Interfaces.Rules;
    using SpaceWars.Model;
    using SpaceWars.Rules;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    public class Director : IDirector
    {
        #region Dependencies

        private readonly IEntityFactory _entityFactory;

        #endregion

        #region Private Fields

        private const string ShipPrefabKey = @"Assets/Sprites/BlankArrow.png";

        private readonly HexMatrix<IHexObject> _allEntities = new();

        private readonly IRules _rules = new BoardRules();
        private readonly List<ITeam> _teams = new();
        private readonly List<IPlayer> _players = new();
        private readonly List<IOrderable> _orderables = new();
        private readonly Dictionary<TurnPhase, List<IMovingHexObject>> _moveables = new();

        #region Damage Handling

        private Dictionary<IDamageable, List<WeaponDamageInstance>> _pendingDamage = new Dictionary<IDamageable, List<WeaponDamageInstance>>();

        #endregion

        /// <summary>
        /// Dicitonary of all players, value represents if they are  "done" with their turn and waiting for other players.
        /// </summary>
        private readonly Dictionary<IPlayer, bool> _waitingPlayers = new();
        private Action OnDoneWaiting;

        #endregion // Private Fields

        #region IDirector

        /// <summary>
        /// Constructor for 
        /// </summary>
        /// <param name="entityFactory"></param>
        public Director(IEntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
            OnDoneWaiting = () => { };
        }

        /// <inheritdoc />
        public int CurrentTurn { get; private set; }

        /// <inheritdoc />
        public IReadOnlyList<ITeam> Teams => _teams;

        /// <inheritdoc />
        public IReadOnlyList<IPlayer> Players => _players;

        /// <inheritdoc />
        public IReadOnlyList<IOrderable> Orderables => _orderables;

        /// <inheritdoc />
        public IReadOnlyCollection<IHexObject> AllEntities => _allEntities;

        /// <inheritdoc />
        public TurnPhase CurrentPhase { get; private set; }

        /// <inheritdoc />
        public IHexObject CreateEntity(IHexObjectPrototype prototype, IPlayer owner, HexVector2 position)
        {
            var entity = _entityFactory.CreateEntity(prototype, owner, position);
            AddEntity(entity);

            return entity;
        }

        /// <inheritdoc />
        public IHexObject CreateEntity(IHexObjectPrototype prototype, IPlayer owner, HexVector2 position, Direction6 direction)
        {
            var entity = _entityFactory.CreateEntity(prototype, owner, position, direction);
            AddEntity(entity);

            return entity;
        }

        /// <inheritdoc />
        public IHexObject CreateEntity(IHexObjectPrototype prototype, IPlayer owner, HexVector2 position, Direction12 direction)
        {
            var entity = _entityFactory.CreateEntity(prototype, owner, position, direction);
            AddEntity(entity);

            return entity;
        }

        /// <inheritdoc />
        public IEnumerable<IHexObject> GetEntitiesInHex(HexVector2 hex)
        {
            return _allEntities.GetCollidingEntities(hex);
        }

        /// <inheritdoc />
        public IEnumerable<TEntity> GetEntitiesInHex<TEntity>(HexVector2 hex)
            where TEntity : class, IHexObject
        {
            var e = _allEntities.GetCollidingEntities(hex);
            return e
                .OfType<TEntity>();
        }

        /// <inheritdoc />
        public OrderResult GiveOrder(IOrderable entity, IOrder order)
        {
            switch (order)
            {
                case IMoveOrder move:
                    if (entity is IOrderable<IMoveOrder> mo)
                    {
                        return GiveOrder(mo, move);
                    }
                    break;
                case IReactorOrder reactor:
                    if (entity is IOrderable<IReactorOrder> ro)
                    {
                        return GiveOrder(ro, reactor);
                    }
                    break;
                case IShieldOrder Shield:
                    if (entity is IOrderable<IShieldOrder> so)
                    {
                        return GiveOrder(so, Shield);
                    }
                    break;
                default:
                    break;
            }

            return new OrderResult { Status = OrderStatus.NotAllowed, Message = "Un-supported order type." };
        }

        /// <inheritdoc />
        public OrderResult GiveOrder<TOrder>(IOrderable<TOrder> entity, TOrder order) where TOrder : IOrder
        {
            return entity.GiveOrder(order);
        }

        /// <inheritdoc />
        public void PlayerEndPhase(IPlayer player)
        {
            throw new NotImplementedException();
        }

        public event EntityCreatedEventHandler? EntityCreated;

        private void AddEntity(IHexObject entity)
        {
            _allEntities.Add(entity);
            if (entity is IMovingHexObject moveable)
            {
                _moveables.AddOrAppend(moveable.MovementPhase, moveable);
            }
            if (entity is IOrderable orderable)
            {
                _orderables.Add(orderable);
            }

            EntityCreated?.Invoke(this, new EntityCreatedEventArgs(entity));
        }

        #endregion // IDirector

        #region Internal Methods

        internal void AddPlayer(IPlayer player)
        {
            if (_waitingPlayers.ContainsKey(player))
            {
                return;
            }
            _players.Add(player);
            _waitingPlayers[player] = false;
        }

        private void CheckWaitingPlayers()
        {
            if (_waitingPlayers.Values.All(v => v))
            {
                OnDoneWaiting();
            }
        }

        private void AddEntity(IPlayer player, IHexObject entity)
        {
            if (player != null)
            {
                entity.Player = player;
                player.AddEntity(entity);
                if (player.TeamNumber > 0)
                {
                    _teams[player.TeamNumber - 1].AddEntity(entity);
                }
            }
            _allEntities.Add(entity);
            if (entity is IMovingHexObject moveable)
            {
                _moveables.AddOrAppend(moveable.MovementPhase, moveable);
            }
            if (entity is IOrderable orderable)
            {
                _orderables.Add(orderable);
            }

            EntityCreated?.Invoke(this, new EntityCreatedEventArgs(entity));
        }

        #endregion // Internal Methods

        #region Turn Phase Handling

        private void HandleStartOfPhase(TurnPhase phase)
        {
            CurrentPhase = phase;

            switch (CurrentPhase)
            {
                case TurnPhase.TurnStart:
                    TurnStart();
                    break;
                case TurnPhase.Orders:
                    Orders();
                    break;
                case TurnPhase.Weapons1:
                    Weapons1();
                    break;
                case TurnPhase.Displacement:
                    Displacement();
                    break;
                case TurnPhase.Weapons2:
                    Weapons2();
                    break;
                case TurnPhase.ApplyDamage1:
                    ApplyDamage();
                    break;
                case TurnPhase.Countermeasures:
                    Countermeasures();
                    break;
                case TurnPhase.Cloaking:
                    Cloaking();
                    break;
                case TurnPhase.Movement:
                    Movement();
                    break;
                case TurnPhase.ApplyDamage2:
                    ApplyDamage();
                    break;
                case TurnPhase.TurnEnd:
                    TurnEnd();
                    break;
                default:
                    break;
            }
        }

        private void HandleEndOfPhase()
        {
            foreach (var entity in _allEntities)
            {
                entity.HandleEndOfPhase(CurrentPhase);
            }

            switch (CurrentPhase)
            {
                case TurnPhase.ApplyDamage1:
                case TurnPhase.ApplyDamage2:
                    CleanupEntities();
                    break;
            }

            HandleStartOfPhase(CurrentPhase.Next());
        }

        private void TurnStart()
        {
            CurrentTurn++;
            HandleEndOfPhase();
        }

        private void Orders()
        {
            foreach (var player in _waitingPlayers.Keys.ToList())
            {
                _waitingPlayers[player] = false;
            }

#if UNITY_EDITOR
            StartTestOrders();
#endif
            OnDoneWaiting = HandleEndOfPhase;
            CheckWaitingPlayers();
        }

        private void Weapons1()
        {
            // TODO: Fire weapons!
            QueueMovement(CurrentPhase, HandleEndOfPhase);
        }

        private void Displacement()
        {
            // Displacement not implemented yet.
            QueueMovement(CurrentPhase, HandleEndOfPhase);
        }

        private void Weapons2()
        {
            // TODO: Fire weapons!
            QueueMovement(CurrentPhase, HandleEndOfPhase);
        }

        private void ApplyDamage()
        {
            foreach (var kvp in _pendingDamage)
            {
                var entity = kvp.Key;
                var damages = kvp.Value
                    .OrderBy(i => i, WeaponDamageInstanceComparer.Default)
                    .SelectMany(i => i.DamageInstances);

                foreach (var damage in damages)
                {
                    entity.ApplyDamage(damage);
                }
            }
            CleanupEntities();
            QueueMovement(CurrentPhase, HandleEndOfPhase);
        }

        private void Countermeasures()
        {
            QueueMovement(CurrentPhase, HandleEndOfPhase);
        }

        private void Cloaking()
        {
            QueueMovement(CurrentPhase, HandleEndOfPhase);
        }

        private void Movement()
        {
            QueueMovement(CurrentPhase, HandleEndOfPhase);
        }

        private void TurnEnd()
        {
            // TODO: Archive, turn summary, maybe more?
            foreach (var entity in _allEntities)
            {
                entity.HandleEndOfTurn(CurrentTurn);
            }
            HandleEndOfPhase();
        }

        private void CleanupEntities()
        {
            var removed = new HashSet<IHexObject>();
            foreach (var entity in _allEntities)
            {
                if (entity.Flags.HasFlag(HexObjectFlags.Remove))
                {
                    removed.Add(entity);
                }
            }
            foreach (var entity in removed)
            {
                foreach (var player in _players)
                {
                    player.RemoveEntity(entity);    // TODO: maybe add a reference to the owner for all entities.
                }
                if (entity is IOrderable orderable)
                {
                    _orderables.Remove(orderable);
                }
                _allEntities.Remove(entity);
            }
        }

        private void QueueMovement(TurnPhase phase, Action onComplete)
        {
            if (_moveables.TryGetValue(phase, out var entities))
            {
                //_ = CoroutineHelper.Start(entities, (e, a) => e.StartMovement(a), onComplete); // TODO: Change do GODOT version
            }
            else
            {
                onComplete();
            }
        }

        private void FireWeapons(TurnPhase phase)
        {
            //var weaponOrders  = _orderables.SelectMany(orderable => orderable.GetOrders<IWeaponOrder>(CurrentTurn))
            //    .Where(order => order.phas;
        }

        #endregion // Turn Phase Handling
    }
}
