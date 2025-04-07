using Godot;
using SpaceWarsHex.Bridges;
using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Model;
using System;

namespace SpaceWarsHex.Entities
{
    public abstract partial class GodotMovingHexObject : GodotHexObject
    {
        #region GodotStuff

        #region Child References

        protected Sprite2D _velocitySprite;

        #endregion // Child References

        public override void _Ready()
        {
            _velocitySprite = GetNode<Sprite2D>("VelocitySprite");
            _scaledChildren.Add(_velocitySprite);
            UpdateVelocity();
            base._Ready();
        }
        #endregion // GodotStuff

        public new IMovingHexObject Entity { get => (IMovingHexObject)base.Entity; set => base.Entity = value; }

        /// <inheritdoc />
        public virtual HexVector2 HexVelocity { get => Entity.Velocity; set => Entity.Velocity = value; }

        /// <inheritdoc />
        public virtual TurnPhase MovementPhase { get; protected set; } = TurnPhase.Movement;

        public void StartMovement(Action onDone)
        {
            throw new NotImplementedException();
        }

        protected virtual void UpdateVelocity()
        {
            _velocitySprite.Position = HexVelocity.ToVector2();
        }

        protected override void OnEntityChanged(string propName)
        {
            base.OnEntityChanged(propName);
            if (propName == nameof(IMovingHexObject.Velocity))
            {
                UpdateVelocity();
            }
        }
    }
}
