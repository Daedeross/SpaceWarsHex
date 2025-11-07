using ReactiveUI;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Prototypes;
using System;

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public class ShieldsViewModel : SystemViewModel, IViewModel<IShieldsPrototype>
    {
        private IShieldsPrototype? _saved;

        private int _maxPower;

        public ShieldsViewModel()
            : this(new ShieldsPrototype())
        { }

        public ShieldsViewModel(ShieldsPrototype prototype)
        {
            _saved = prototype ?? throw new ArgumentNullException(nameof(prototype));
            LoadFrom(_saved);
        }

        public override void LoadFrom(ISystemPrototype prototype)
        {
            base.LoadFrom(prototype);

            if (prototype is IShieldsPrototype sp)
            {
                _saved = sp;
                MaxPower = sp.MaxPower;
            }
        }

        public override void SaveTo(ISystemPrototype prototype)
        {
            base.SaveTo(prototype);

            if (prototype is ShieldsPrototype sp)
            {
                sp.MaxPower = MaxPower;
            }
        }

        public int MaxPower
        {
            get => _maxPower;
            set => this.RaiseAndSetIfChanged(ref _maxPower, value);
        }
    }
}