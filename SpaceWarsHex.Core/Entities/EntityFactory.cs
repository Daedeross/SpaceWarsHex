using SpaceWars.Interfaces;
using SpaceWars.Interfaces.Bridges;
using SpaceWars.Interfaces.Prototypes;
using SpaceWars.Interfaces.Rules;
using SpaceWars.Model;
using System;

namespace SpaceWars.Entities
{
    public class EntityFactory<TWrapper> : IEntityFactory
         where TWrapper : IWrapper<IHexObject>
    {
        private readonly IWrapperFactory<IHexObject, TWrapper> _wrapperFactory;
        private readonly IRules _rules;

        /// <summary>
        /// Constructor for <see cref="EntityFactory{TWrapper}"/>
        /// </summary>
        /// <param name="wrapperFactory"></param>
        /// <param name="rules">The games's rules.</param>
        public EntityFactory(IWrapperFactory<IHexObject, TWrapper> wrapperFactory, IRules rules)
        {
            _wrapperFactory = wrapperFactory;
            _rules = rules;
        }

        public IHexObject CreateEntity(IHexObjectPrototype prototype, IPlayer owner, HexVector2 Position)
        {
            IHexObject entity = prototype switch
            {
                IShipPrototype ship => CreateShip(ship),
                _ => throw new NotImplementedException($"No factory method for {prototype.GetType()} exists."),
            };

            entity.Id = Guid.NewGuid();
            entity.Player = owner;
            entity.Position = Position;

            _ = _wrapperFactory.CreateWrapper(entity);

            return entity;
        }

        public IHexObject CreateEntity(IHexObjectPrototype prototype, IPlayer owner, HexVector2 Position, Direction6 direction)
        {
            IHexObject entity = CreateEntity(prototype, owner, Position);
            if (entity is IHaveDirection6 d6)
            {
                d6.Orientation = direction;
            }

            return entity;
        }

        public IHexObject CreateEntity(IHexObjectPrototype prototype, IPlayer owner, HexVector2 Position, Direction12 direction)
        {

            IHexObject entity = CreateEntity(prototype, owner, Position);
            if (entity is IHaveDirection12 d12)
            {
                d12.Orientation = direction;
            }

            return entity;
        }

        public void Destroy(IHexObject hexObject)
        {
            throw new NotImplementedException();
        }

        internal IShip CreateShip(IShipPrototype prototype)
        {
            return new Ship(prototype, _rules);
        }
    }
}
