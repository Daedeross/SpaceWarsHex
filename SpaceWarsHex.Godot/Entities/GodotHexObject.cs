using Godot;
using SpaceWarsHex.Bridges;
using SpaceWarsHex.Model;
using System.Collections.Generic;
using System.ComponentModel;
using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Entities;

namespace SpaceWarsHex.Entities
{
    public abstract partial class GodotHexObject : WrapperNode2D<IHexObject>
    {
        #region Godot Stuff

        protected Vector2 _scale;

        protected List<Node2D> _scaledChildren = [];

#pragma warning disable CS8618 // These will be assigned to in _Ready(), if not then something went wrong and any resulting exceptions should be thrown.
        protected Sprite2D _mainSprite;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Texture2D MainSprite
        {
            get => _mainSprite.Texture;
            set
            {
                _mainSprite.Texture = value;
                ScaleSprites();
            }
        }

        public override void _Ready()
        {
            _mainSprite = GetNode<Sprite2D>("MainSprite");
            _scaledChildren.Add(_mainSprite);
            ScaleSprites();
            UpdatePosition();
        }

        protected void ScaleSprites()
        {
            if (_mainSprite is null) { return; }
            // scale sprite so longest dimension is equal to ship size
            var texSize = _mainSprite.Texture.GetSize();
            var max = Mathf.Max(texSize.X, texSize.Y);
            var factor = 2 * HexMath.Default.R / max;
            _scale = new Vector2(factor, factor);

            foreach (var child in _scaledChildren)
            {
                GD.Print(child.Name);
                child.Scale = _scale;
            }
        }

        //public override void _ExitTree()
        //{
        //    if (_entity != null)
        //    {
        //        _entity.PropertyChanged -= OnEntityPropertyChanged;
        //    }
        //}

        #endregion // Godot Stuff

        #region IHexObject

        public HexVector2 HexPosition
        {
            get => Entity.Position;
            set => Entity.Position = value;
        }

        protected virtual void UpdatePosition()
        {
            Position = HexPosition.ToVector2();
        }

        #endregion // IHexObject

        /// <summary>
        /// Calback to notify wrapper that a property on the underlying entity has changed.
        /// </summary>
        /// <param name="propName"></param>
        protected override void OnEntityChanged(string? propName)
        {
            if (propName == nameof(IHexObject.Position))
            {
                UpdatePosition();
            }
        }
    }
}
