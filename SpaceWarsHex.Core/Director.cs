﻿using SpaceWarsHex.Helpers;
using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Interfaces.Bridges;
using SpaceWarsHex.Interfaces.Orders;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Interfaces.Rules;
using SpaceWarsHex.Interfaces.Systems;
using SpaceWarsHex.Model;
using SpaceWarsHex.Rules;

namespace SpaceWarsHex
{
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

        private readonly HexMatrix<IHexObject> _allEntities = [];
        private readonly Dictionary<Guid, IHexObject> _idMap = [];

        private readonly IRules _rules = new BoardRules();
        private readonly List<ITeam> _teams = [];
        private readonly List<IPlayer> _players = [];
        private readonly List<IOrderable> _orderables = [];
        private readonly Dictionary<TurnPhase, List<IMovingHexObject>> _moveables = [];

        #region Damage Handling

        private readonly Dictionary<IDamageable, List<WeaponDamageInstance>> _pendingDamage = [];

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
        public IHexObject CreateEntity(IHexObjectPrototype prototype, IPlayer? owner, HexVector2 position)
        {
            var entity = _entityFactory.CreateEntity(prototype, owner, position);
            AddEntity(owner, entity);

            return entity;
        }

        /// <inheritdoc />
        public IHexObject CreateEntity(IHexObjectPrototype prototype, IPlayer? owner, HexVector2 position, Direction6 direction)
        {
            var entity = _entityFactory.CreateEntity(prototype, owner, position, direction);
            AddEntity(owner, entity);

            return entity;
        }

        /// <inheritdoc />
        public IHexObject CreateEntity(IHexObjectPrototype prototype, IPlayer? owner, HexVector2 position, Direction12 direction)
        {
            var entity = _entityFactory.CreateEntity(prototype, owner, position, direction);
            AddEntity(owner, entity);

            return entity;
        }

        public bool TryGetEntity(Guid id, out IHexObject? entity)
        {
            return _idMap.TryGetValue(id, out entity);
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

        private void AddEntity(IPlayer? player, IHexObject entity)
        {
            if (player != null)
            {
                entity.Player = player;
                player.AddEntity(entity);
                if (player.TeamNumber > 0)
                {
                    _teams[player.TeamNumber - 1].AddEntity(entity);
                    entity.TeamNumber = player.TeamNumber;
                }
            }
            _allEntities.Add(entity);
            _idMap.Add(entity.Id, entity);
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
            FireWeapons(CurrentPhase);
            QueueMovement(CurrentPhase, HandleEndOfPhase);
        }

        private void Displacement()
        {
            // Displacement not implemented yet.
            QueueMovement(CurrentPhase, HandleEndOfPhase);
        }

        private void Weapons2()
        {
            FireWeapons(CurrentPhase);
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
                _idMap.Remove(entity.Id);
            }
        }

        private void QueueMovement(TurnPhase phase, Action onComplete)
        {
            if (_moveables.TryGetValue(phase, out var entities))
            {
                //_ = CoroutineHelper.Start(entities, (e, a) => e.StartMovement(a), onComplete); // TODO: Change to GODOT version
            }
            else
            {
                onComplete();
            }
        }

        private void FireWeapons(TurnPhase phase)
        {
            var weaponOrders = _orderables
                .OfType<IFireWeapons>()
                .SelectMany(entity => entity.GetFiringWeapons(phase), (e, o) => new { Entity = e, Order = o })
                .GroupBy(a => a.Entity);

            foreach (var entity in weaponOrders)
            {
                
            }
        }

        #endregion // Turn Phase Handling

        #region Weapon Spawing

        private void SpawnTorpedo(IHexObject source, ITorpedoLauncher launcher, IWeaponOrder order)
        {

        }

        #endregion // Weapon Spawing

        public override int GetHashCode()
        {
            var hash = new HashCode();

            foreach (var entity in _allEntities)
            {
                hash.Add(entity.GetHashCode());
            }

            return hash.ToHashCode();
        }
    }
}
