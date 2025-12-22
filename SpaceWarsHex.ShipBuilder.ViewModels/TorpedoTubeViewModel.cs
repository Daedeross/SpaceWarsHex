using ReactiveUI.SourceGenerators;
using SpaceWarsHex.Prototypes;

#nullable enable

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public partial class TorpedoTubeViewModel : OrdinanceViewModel, IViewModel<TorpedoTubePrototype>
    {
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

        public override void LoadFrom(OrdinancePrototype prototype)
        {
            base.LoadFrom(prototype);

            if (prototype is TorpedoTubePrototype tp)
            {
                MinWarp = tp.MinWarp;
                MaxWarp = tp.MaxWarp;
                Homing = tp.Homing;
                LoadFire = tp.LoadFire;
            }
        }

        public override void SaveTo(OrdinancePrototype prototype)
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

        TorpedoTubePrototype IViewModel<TorpedoTubePrototype>.Commit()
        {
            SaveTo(_saved);

            return (TorpedoTubePrototype)_saved;
        }

        TorpedoTubePrototype IViewModel<TorpedoTubePrototype>.GetLast()
        {
            return (TorpedoTubePrototype)_saved;
        }
    }
}
