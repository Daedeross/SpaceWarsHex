using ReactiveUI.SourceGenerators;
using SpaceWarsHex.Prototypes;

#nullable enable

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public partial class ShieldsViewModel : SystemViewModel<ShieldsPrototype>
    {
        [Reactive]
        private int _maxPower;

        public ShieldsViewModel()
            : this(new ShieldsPrototype())
        { }

        public ShieldsViewModel(ShieldsPrototype prototype)
            : base(prototype)
        { }

        public override void LoadFrom(ShieldsPrototype prototype)
        {
            base.LoadFrom(prototype);

            MaxPower = prototype.MaxPower;
        }

        public override void SaveTo(ShieldsPrototype prototype)
        {
            base.SaveTo(prototype);

            prototype.MaxPower = MaxPower;
        }
    }
}