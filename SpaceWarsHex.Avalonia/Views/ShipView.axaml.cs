using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using ReactiveUI.Avalonia;
using SpaceWarsHex.ShipBuilder.ViewModels;

namespace SpaceWarsHex.Avalonia;

public partial class ShipView : ReactiveUserControl<ShipViewModel>
{
    public ShipView()
    {
        AvaloniaXamlLoader.Load(this);
    }
}