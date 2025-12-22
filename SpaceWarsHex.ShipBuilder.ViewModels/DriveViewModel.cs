using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Prototypes;
using System;

#nullable enable

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public partial class DriveViewModel : SystemViewModel<DrivePrototype>
    {
        [Reactive]
        private int _maxWarp;
        [Reactive]
        private int _accelerationClass;

        public DriveViewModel()
            : this(new DrivePrototype() { Name = "Drive", Id = Guid.NewGuid(), MaxWarp = 1, AccelerationClass = 1 })
        { }

        public DriveViewModel(DrivePrototype prototype)
            : base(prototype)
        { }

        public override void LoadFrom(DrivePrototype prototype)
        {
            base.LoadFrom(prototype);

            MaxWarp = prototype.MaxWarp;
            AccelerationClass = prototype.AccelerationClass;
        }

        public override void SaveTo(DrivePrototype prototype)
        {
            base.SaveTo(prototype);

            prototype.MaxWarp = MaxWarp;
            prototype.AccelerationClass = AccelerationClass;
        }
    }
}