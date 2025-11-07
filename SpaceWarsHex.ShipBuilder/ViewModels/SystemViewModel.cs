using ReactiveUI;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Prototypes;
using System;
using System.Reactive;

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public abstract class SystemViewModel : ViewModelBase
    {
        protected ISystemPrototype? _savedPrototype;

        private Guid _id;
        private string _name = string.Empty;

        public SystemViewModel()
        {
            SaveCommand = ReactiveCommand.Create(Save);
            ResetCommand = ReactiveCommand.Create(Reset);
        }

        /// <summary>
        /// Save changes from ViewModel into the saved prototype instance.
        /// </summary>
        public ReactiveCommand<Unit, Unit> SaveCommand { get; }

        /// <summary>
        /// Reset ViewModel properties to the last saved prototype state.
        /// </summary>
        public ReactiveCommand<Unit, Unit> ResetCommand { get; }

        public Guid Id
        {
            get => _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);
        }

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        /// <summary>
        /// Load VM properties from provided prototype. Implementations should call base.LoadFrom(...) first.
        /// </summary>
        public virtual void LoadFrom(ISystemPrototype prototype)
        {
            if (prototype is null) throw new ArgumentNullException(nameof(prototype));
            _savedPrototype = prototype;
            Id = prototype.Id;
            Name = prototype.Name ?? string.Empty;
        }

        /// <summary>
        /// Save VM properties into provided prototype. Implementations should call base.SaveTo(...) first.
        /// </summary>
        public virtual void SaveTo(ISystemPrototype prototype)
        {
            if (prototype is null) throw new ArgumentNullException(nameof(prototype));

            if (prototype is SystemPrototypeBase spb)
            {
                spb.Id = Id;
                spb.Name = Name ?? string.Empty;
            }
        }

        private void Save()
        {
            if (_savedPrototype != null)
            {
                SaveTo(_savedPrototype);
            }
        }

        private void Reset()
        {
            if (_savedPrototype != null)
            {
                LoadFrom(_savedPrototype);
            }
        }
    }
}
