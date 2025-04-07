using Godot;
using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Interfaces.Bridges;
using SpaceWarsHex.Entities;
using System;

namespace SpaceWarsHex.Bridges
{
    public class GodotEntityFactory : IWrapperFactory<IHexObject, GodotHexObject>
    {
        private readonly Node _root;
        private readonly PackedScene _shipScene;

        /// <summary>
        /// Constructor for <see cref="GodotEntityFactory"/>
        /// </summary>
        /// <param name="root">The root node for all entities. Usually a battle.</param>
        public GodotEntityFactory(Node root)
        {
            _root = root;
            _shipScene = GD.Load<PackedScene>("res://Entities/GodotShip.tscn");
        }

        public GodotHexObject CreateWrapper(IHexObject entity)
        {
            return CreateNode(entity);
        }

        public void Destroy(IHexObject hexObject)
        {
            throw new NotImplementedException();
        }

        private GodotHexObject CreateNode(IHexObject entity)
        {
            GodotHexObject node = entity switch
            {
                IShip ship => CreateShip(ship),
                _ => throw new NotImplementedException($"No factory method for {entity.GetType()} exists."),
            };

            _root.AddChild(node);

            return node;
        }

        internal GodotShip CreateShip(IShip entity)
        {
            var visual = _shipScene.Instantiate<GodotShip>();
            visual.Entity = entity;

            return visual;
        }
    }
}
