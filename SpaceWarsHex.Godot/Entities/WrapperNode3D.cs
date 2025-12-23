using Godot;
using SpaceWarsHex.Interfaces;
using System.ComponentModel;

namespace SpaceWarsHex.Entities
{
    public abstract partial class WrapperNode3D<T> : Node3D, IWrapper<T>
        where T : INotifyPropertyChanged
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private T _entity;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public T Entity
        {
            get => _entity;
            set
            {
                if (!Equals(_entity, value))
                {
                    if (_entity != null)
                    {
                        _entity.PropertyChanged -= OnEntityPropertyChanged;
                    }

                    _entity = value;

                    if (_entity != null)
                    {
                        _entity.PropertyChanged += OnEntityPropertyChanged;
                    }
                }
            }
        }
        private void OnEntityPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (!Equals(_entity, sender))
            {
                GD.PushWarning("HexObject received change notification from unknown entity.");
                return;
            }

            OnEntityChanged(e.PropertyName);
        }

        protected abstract void OnEntityChanged(string? propName);

        public override void _ExitTree()
        {
            if (_entity != null)
            {
                _entity.PropertyChanged -= OnEntityPropertyChanged;
            }
        }
    }
}
