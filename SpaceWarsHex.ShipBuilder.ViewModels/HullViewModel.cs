using ReactiveUI;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Prototypes;
using System;
using System.Reactive;

#nullable enable

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public class HullViewModel : ViewModelBase, IViewModel<IHullPrototype>
    {
        private IHullPrototype? _saved;

        private int _maxIntegrity;

        public HullViewModel()
            : this(new HullPrototype())
        { }

        public HullViewModel(HullPrototype prototype)
        {
            _saved = prototype ?? throw new ArgumentNullException(nameof(prototype));

            SaveCommand = ReactiveCommand.Create(Save);
            ResetCommand = ReactiveCommand.Create(Reset);
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

        private void Save()
        {
            if (_saved != null)
            {
                SaveTo(_saved);
            }
        }

        private void Reset()
        {
            if (_saved != null)
            {
                LoadFrom(_saved);
            }
        }

        /// <summary>
        /// Save changes from ViewModel into the saved prototype instance.
        /// </summary>
        public ReactiveCommand<Unit, Unit> SaveCommand { get; }

        /// <summary>
        /// Reset ViewModel properties to the last saved prototype state.
        /// </summary>
        public ReactiveCommand<Unit, Unit> ResetCommand { get; }

        public int MaxIntegrity
        {
            get => _maxIntegrity;
            set => this.RaiseAndSetIfChanged(ref _maxIntegrity, value);
        }
    }
}