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

#pragma warning disable CS8618 // These will be assigned to in _Ready(), if not then something went wrong and any resulting exceptions should be thrown.
        protected Sprite2D _velocitySprite;
        protected Line2D _velocityLine;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        #endregion // Child References

        public override void _Ready()
        {
            _velocitySprite = GetNode<Sprite2D>("VelocitySprite");
            _velocityLine = GetNode<Line2D>("VelocityLine");
            _scaledChildren.Add(_velocitySprite);
            base._Ready();
            UpdateVelocity();
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

        public void AlignToVelocity()
        {
            var angle = HexVelocity.ToVector2().Normalized().Angle();

            _mainSprite.Rotation = angle;
            _velocitySprite.Rotation = angle;
        }

        protected virtual void UpdateVelocity()
        {
            var v = HexVelocity.ToVector2();
            _velocitySprite.Position = v;
            _velocityLine.RemovePoint(1);
            _velocityLine.AddPoint(v);
            AlignToVelocity();
        }

        protected override void OnEntityChanged(string? propName)
        {
            base.OnEntityChanged(propName);
            if (propName == nameof(IMovingHexObject.Velocity))
            {
                UpdateVelocity();
            }
        }
    }
}
