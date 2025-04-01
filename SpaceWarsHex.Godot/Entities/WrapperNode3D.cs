using Godot;
using SpaceWars.Interfaces;
using System.ComponentModel;

namespace SpaceWarHex.Entities
{
    public abstract partial class WrapperNode3D<T> : Node3D, IWrapper<T>
        where T : INotifyPropertyChanged
    {
        private T _entity;
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
        private void OnEntityPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!Equals(_entity, sender))
            {
                GD.PushWarning("HexObject received change notification from unknown entity.");
                return;
            }

            OnEntityChanged(e.PropertyName);
        }

        protected abstract void OnEntityChanged(string propName);

        public override void _ExitTree()
        {
            if (_entity != null)
            {
                _entity.PropertyChanged -= OnEntityPropertyChanged;
            }
        }
    }
}
