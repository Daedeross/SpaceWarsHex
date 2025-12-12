using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Model;
using SpaceWarsHex.Prototypes;
using System;
using System.Reactive;

#nullable enable

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public abstract partial class SystemViewModel : ViewModelBase
    {
        protected ISystemPrototype? _savedPrototype;

        [Reactive]
        private Guid _id;
        [Reactive]
        private string _name = string.Empty;
        [Reactive]
        private ThresholdsViewModel _damageThresholds;

        public SystemViewModel(ISystemPrototype systemPrototype)
        {
            _savedPrototype = systemPrototype.GetOrThrow();
            _id = systemPrototype.Id;
            _name = systemPrototype.Name ?? string.Empty;
            _damageThresholds = new ThresholdsViewModel(systemPrototype.DamageThresholds ?? []);
        }

        /// <summary>
        /// Load VM properties from provided prototype. Implementations should call base.LoadFrom(...) first.
        /// </summary>
        public virtual void LoadFrom(ISystemPrototype prototype)
        {
            ArgumentNullException.ThrowIfNull(prototype);

            _savedPrototype = prototype;
            Id = prototype.Id;
            Name = prototype.Name ?? string.Empty;
            _damageThresholds.Dispose();
            _damageThresholds = new ThresholdsViewModel(prototype.DamageThresholds ?? []);
        }

        /// <summary>
        /// Save VM properties into provided prototype. Implementations should call base.SaveTo(...) first.
        /// </summary>
        public virtual void SaveTo(ISystemPrototype prototype)
        {
            ArgumentNullException.ThrowIfNull(prototype);

            if (prototype is SystemPrototypeBase spb)
            {
                spb.Id = Id;
                spb.Name = Name ?? string.Empty;
            }
        }

        [ReactiveCommand]
        private void Save()
        {
            if (_savedPrototype != null)
            {
                SaveTo(_savedPrototype);
            }
        }

        [ReactiveCommand]
        private void Reset()
        {
            if (_savedPrototype != null)
            {
                LoadFrom(_savedPrototype);
            }
        }
    }
}
