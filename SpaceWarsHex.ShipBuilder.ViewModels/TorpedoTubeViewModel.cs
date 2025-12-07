using ReactiveUI;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Prototypes;
using System;

#nullable enable

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public class TorpedoTubeViewModel : OrdinanceViewModel, IViewModel<ITorpedoTubePrototype>
    {
        private ITorpedoTubePrototype? _saved;

        private int _minWarp;
        private int _maxWarp;
        private bool _homing;
        private bool _loadFire;

        public TorpedoTubeViewModel()
            : this(new TorpedoTubePrototype())
        { }

        public TorpedoTubeViewModel(TorpedoTubePrototype prototype)
        {
            _saved = prototype ?? throw new ArgumentNullException(nameof(prototype));
            LoadFrom(_saved);
        }

        public override void LoadFrom(ISystemPrototype prototype)
        {
            base.LoadFrom(prototype);

            if (prototype is ITorpedoTubePrototype tp)
            {
                _saved = tp;
                MinWarp = tp.MinWarp;
                MaxWarp = tp.MaxWarp;
                Homing = tp.Homing;
                LoadFire = tp.LoadFire;
            }
        }

        public override void SaveTo(ISystemPrototype prototype)
        {
            base.SaveTo(prototype);

            if (prototype is TorpedoTubePrototype tp)
            {
                tp.MinWarp = MinWarp;
                tp.MaxWarp = MaxWarp;
                tp.Homing = Homing;
                tp.LoadFire = LoadFire;
            }
        }

        public int MinWarp
        {
            get => _minWarp;
            set => this.RaiseAndSetIfChanged(ref _minWarp, value);
        }

        public int MaxWarp
        {
            get => _maxWarp;
            set => this.RaiseAndSetIfChanged(ref _maxWarp, value);
        }

        public bool Homing
        {
            get => _homing;
            set => this.RaiseAndSetIfChanged(ref _homing, value);
        }

        public bool LoadFire
        {
            get => _loadFire;
            set => this.RaiseAndSetIfChanged(ref _loadFire, value);
        }
    }
}
