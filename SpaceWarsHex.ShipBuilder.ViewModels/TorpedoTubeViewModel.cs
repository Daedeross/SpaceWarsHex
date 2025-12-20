using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Prototypes;
using System;

#nullable enable

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public partial class TorpedoTubeViewModel : OrdinanceViewModel, IViewModel<ITorpedoTubePrototype>
    {
        private ITorpedoTubePrototype _saved;

        [Reactive]
        private int _minWarp;
        [Reactive]
        private int _maxWarp;
        [Reactive]
        private bool _homing;
        [Reactive]
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

        public override ITorpedoTubePrototype GetPrototype()
        {
            return _saved;
        }
    }
}
