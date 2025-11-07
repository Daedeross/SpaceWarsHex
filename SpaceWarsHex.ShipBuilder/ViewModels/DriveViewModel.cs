using ReactiveUI;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Prototypes;
using System;

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public class DriveViewModel : SystemViewModel, IViewModel<IDrivePrototype>
    {
        private IDrivePrototype? _saved;

        private int _maxWarp;
        private int _accelerationClass;

        public DriveViewModel()
            : this(new DrivePrototype())
        { }

        public DriveViewModel(DrivePrototype prototype)
        {
            _saved = prototype ?? throw new ArgumentNullException(nameof(prototype));
            LoadFrom(_saved);
        }

        public override void LoadFrom(ISystemPrototype prototype)
        {
            base.LoadFrom(prototype);

            if (prototype is IDrivePrototype dp)
            {
                _saved = dp;
                MaxWarp = dp.MaxWarp;
                AccelerationClass = dp.AccelerationClass;
            }
        }

        public override void SaveTo(ISystemPrototype prototype)
        {
            base.SaveTo(prototype);

            if (prototype is DrivePrototype dp)
            {
                dp.MaxWarp = MaxWarp;
                dp.AccelerationClass = AccelerationClass;
            }
        }

        public int MaxWarp
        {
            get => _maxWarp;
            set => this.RaiseAndSetIfChanged(ref _maxWarp, value);
        }

        public int AccelerationClass
        {
            get => _accelerationClass;
            set => this.RaiseAndSetIfChanged(ref _accelerationClass, value);
        }
    }
}