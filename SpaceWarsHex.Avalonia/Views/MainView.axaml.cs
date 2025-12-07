using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using ReactiveUI.Avalonia;
using SpaceWarsHex.Avalonia.ViewModels;

namespace SpaceWarsHex.Avalonia.Views;

public partial class MainView : ReactiveUserControl<MainViewModel>
{


    public MainView()
    {
        this.WhenActivated(d =>
        {
            //this.Bind(ViewModel, vm => vm.Ships, view => view.ShipTabs.ItemsSource)
            //    .DisposeWith(d);
        });

        AvaloniaXamlLoader.Load(this);
    }
}
