using Godot;
using SpaceWars.Interfaces;
using SpaceWars.Interfaces.Orders;
using SpaceWars.Interfaces.Prototypes;
using SpaceWars.Model;
using System;
using System.Collections.Generic;

namespace SpaceWarsHex
{
    public partial class Battle : Node
    {
        public int CurrentTurn => throw new NotImplementedException();

        public IReadOnlyList<ITeam> Teams => throw new NotImplementedException();

        public IReadOnlyList<IPlayer> Players => throw new NotImplementedException();

        public IReadOnlyList<IOrderable> Orderables => throw new NotImplementedException();

        public IReadOnlyCollection<IHexObject> AllEntities => throw new NotImplementedException();

        public TurnPhase CurrentPhase => throw new NotImplementedException();

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
        }

        public OrderResult GiveOrder(IOrderable entity, IOrder order)
        {
            throw new NotImplementedException();
        }

        public OrderResult GiveOrder<TOrder>(IOrderable<TOrder> entity, TOrder order) where TOrder : IOrder
        {
            throw new NotImplementedException();
        }

        public void CreateEntity(IHexObjectPrototype prototype, IPlayer owner, HexVector2 position)
        {
            throw new NotImplementedException();
        }

        public void CreateEntity(IHexObjectPrototype prototype, IPlayer owner, HexVector2 position, Direction6 direction)
        {
            throw new NotImplementedException();
        }

        public void CreateEntity(IHexObjectPrototype prototype, IPlayer owner, HexVector2 position, Direction12 direction)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IHexObject> GetEntitiesInHex(HexVector2 hex)
        {
            throw new NotImplementedException();
        }

        //IEnumerable<TEntity> IDirector.GetEntitiesInHex<TEntity>(HexVector2 hex)
        //{
        //    throw new NotImplementedException();
        //}

        public void PlayerEndPhase(IPlayer player)
        {
            throw new NotImplementedException();
        }
    }

}