using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Prototypes;
using System;
using System.Reactive;

#nullable enable

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public partial class HullViewModel : ViewModelBase, IViewModel<IHullPrototype>
    {
        private IHullPrototype? _saved;

        private int _maxIntegrity;

        public HullViewModel()
            : this(new HullPrototype() { MaxIntegrity = 1 })
        { }

        public HullViewModel(HullPrototype prototype)
        {
            _saved = prototype.GetOrThrow();

            LoadFrom(_saved);
        }

        public void LoadFrom(IHullPrototype prototype)
        {
            if (prototype is IHullPrototype hp)
            {
                _saved = hp;
                MaxIntegrity = hp.MaxIntegrity;
            }
        }

        public void SaveTo(IHullPrototype prototype)
        {
            if (prototype is HullPrototype hp)
            {
                hp.MaxIntegrity = MaxIntegrity;
            }
        }

        [ReactiveCommand]
        private void Save()
        {
            if (_saved != null)
            {
                SaveTo(_saved);
            }
        }

        [ReactiveCommand]
        private void Reset()
        {
            if (_saved != null)
            {
                LoadFrom(_saved);
            }
        }

        public int MaxIntegrity
        {
            get => _maxIntegrity;
            set => this.RaiseAndSetIfChanged(ref _maxIntegrity, value);
        }
    }
}