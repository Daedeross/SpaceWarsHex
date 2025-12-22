using ReactiveUI.SourceGenerators;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Prototypes;

#nullable enable

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public abstract partial class SystemViewModel<TPrototype> : ViewModelBase, IViewModel<TPrototype>
        where TPrototype : class, ISystemPrototype
    {
        protected TPrototype _saved;

        [Reactive]
        private Guid _id;
        [Reactive]
        private string _name = string.Empty;
        [Reactive]
        private ThresholdsViewModel _damageThresholds;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public SystemViewModel(TPrototype prototype)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
            ArgumentNullException.ThrowIfNull(prototype);
            _saved = prototype.GetOrThrow();
            LoadFrom(prototype);
        }

        /// <summary>
        /// Load VM properties from provided prototype. Implementations should call base.LoadFrom(...) first.
        /// </summary>
        public virtual void LoadFrom(TPrototype prototype)
        {
            ArgumentNullException.ThrowIfNull(prototype);

            _saved = prototype;
            Id = prototype.Id;
            Name = prototype.Name ?? string.Empty;
            _damageThresholds?.Dispose();
            _damageThresholds = new ThresholdsViewModel(prototype.DamageThresholds ?? []);
        }

        /// <summary>
        /// Save VM properties into provided prototype. Implementations should call base.SaveTo(...) first.
        /// </summary>
        public virtual void SaveTo(TPrototype prototype)
        {
            ArgumentNullException.ThrowIfNull(prototype);

            prototype.Id = Id;
            if (prototype is SystemPrototypeBase spb)
            {
                spb.Name = Name ?? string.Empty;
                spb._damageThresholds = _damageThresholds
                    .GetThresholds()
                    .ToList();
            }
        }

        [ReactiveCommand]
        public void Reset()
        {
            LoadFrom(_saved);
        }

        public TPrototype GetLast()
        {
            return _saved;
        }

        public TPrototype Commit()
        {
            SaveTo(_saved);

            return _saved;
        }
    }
}
