using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Interfaces.Orders;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Model;
using System.Net.Http.Headers;

namespace SpaceWarsHex.Entities
{
    public class Torpedo : MovingHexObject, ITorpedo
    {
        private readonly IReadOnlyCollection<OrderKind> _validOrders;

        public Torpedo(ITorpedoPrototye prototye, HexVector2 velocity, IHexObject owner)
            : base()
        {
            Owner = owner;
            Player = owner.Player;
            Power = prototye.Strength;
            Homing = prototye.Homing;
            AccelerationClass = prototye.Acceleration;
            Velocity = velocity;
            ExplodePhase = prototye.FirePhase;

            _validOrders = Homing == HomingType.None
                ? [OrderKind.TorpedoOrder]
                : [OrderKind.MoveOrder, OrderKind.TorpedoOrder];
        }

        public TurnPhase ExplodePhase { get; }

        /// <inheritdoc />
        public int Power { get; }

        /// <inheritdoc />
        public HomingType Homing { get; }

        /// <inheritdoc />
        public int HomingLoss { get; }

        /// <inheritdoc />
        public bool ValidState => throw new NotImplementedException();

        /// <inheritdoc />
        public string StateMessage => throw new NotImplementedException();

        /// <inheritdoc />
        public IReadOnlyCollection<OrderKind> ValidOrders => _validOrders;

        /// <inheritdoc />
        public int AccelerationClass { get; }
        
        /// <inheritdoc />
        public HexVector2 Acceleration { get; protected set; }

        /// <inheritdoc />
        public IReadOnlyDictionary<int, IReadOnlyCollection<IOrder>> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IReadOnlyCollection<IOrder> GetCurrentTurnOrders()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IReadOnlyCollection<IOrder> GetOrders(int turnNumber)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IReadOnlyCollection<TOrder> GetOrders<TOrder>(int turnNumber) where TOrder : IOrder
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public OrderResult GiveOrder(ITorpedoExplodeOrder order)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public OrderResult GiveOrder(IMoveOrder order)
        {
            if (Homing == HomingType.None)
            {
                return OrderResult.NotAllowed();
            }

            if (Equals(Acceleration, order.Acceleration))
            {
                return OrderResult.NotModified();
            }

            if (order.Acceleration.Length() > AccelerationClass)
            {
                return OrderResult.NotValid();
            }

            var oldSpeed = Velocity.Length();
            var newVel = Velocity + order.Acceleration;
            if (newVel.Length() + HomingLoss != oldSpeed)
            {
                return OrderResult.NotValid();
            }

            Acceleration = order.Acceleration;

            return OrderResult.Ok();
        }

        /// <inheritdoc />
        public OrderResult GiveOrder<TOrder>(TOrder order) where TOrder : IOrder
        {
            return order switch
            {
                IMoveOrder mo => GiveOrder(mo),
                ITorpedoExplodeOrder torpOrd => GiveOrder(torpOrd),
                _ => OrderResult.NotAllowed(),
            };
        }

        /// <inheritdoc />
        public override bool HandleEndOfPhase(TurnPhase turnPhase)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override bool HandleEndOfTurn(int turnNumber)
        {
            throw new NotImplementedException();
        }
    }
}
