using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Prototypes;
using System;

#nullable enable

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public partial class DriveViewModel : SystemViewModel, IViewModel<IDrivePrototype>
    {
        private IDrivePrototype _saved;

        [Reactive]
        private int _maxWarp;
        [Reactive]
        private int _accelerationClass;

        public DriveViewModel()
            : this(new DrivePrototype() { Name = "Drive", Id = Guid.NewGuid(), MaxWarp = 1, AccelerationClass = 1 })
        { }

        public DriveViewModel(DrivePrototype prototype)
            : base(prototype)
        {
            _saved = prototype ?? throw new ArgumentNullException(nameof(prototype));
            LoadFrom(_saved);
        }

        public IDrivePrototype GetPrototype()
        {
            return _saved;
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
    }
}