using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public class CreateLinearViewModel : ReactiveObject
    {
        [Reactive]
        public double HullEnd { get; set; }
        [Reactive]
        public double MultiplierEnd { get; set; }
        [Reactive]
        public int Count { get; set; }
        public CreateLinearViewModel()
        {
            HullEnd = 0.25d;
            MultiplierEnd = 0.5d;
            Count = 4;
        }
    }
}
