using ReactiveUI;
using System.Reactive;

namespace SpaceWarsHex.ShipBuilder
{
    public static class Interactions
    {
        public static readonly Interaction<Unit, string?> ShowSaveDialog = new();

        public static readonly Interaction<Unit, string?> ShowOpenDialog = new();
    }
}
