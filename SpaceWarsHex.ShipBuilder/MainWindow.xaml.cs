using ReactiveUI;
using SpaceWarsHex.ShipBuilder.ViewModels;
using System.Reactive.Disposables.Fluent;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpaceWarsHex.ShipBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ReactiveWindow<WorkspaceViewModel>
    {
        public MainWindow(WorkspaceViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;

            this.WhenActivated(disposables =>
            {
                // Interactions
                ViewInteractions.Initialize(this);

                this.OneWayBind(ViewModel,
                    vm => vm.Ships,
                    v => v.ShipsItemsControl.ItemsSource)
                    .DisposeWith(disposables);

                this.Bind(ViewModel,
                    vm => vm.CurrentShip,
                    v => v.ShipsItemsControl.SelectedItem)
                    .DisposeWith(disposables);

                //this.OneWayBind(ViewModel,
                //    vm => vm.CurrentShip,
                //    v => v.VMHost.ViewModel)
                //    .DisposeWith(disposables);

                this.BindCommand(ViewModel,
                    vm => vm.NewShipCommand,
                    v => v.NewShipBtn)
                    .DisposeWith(disposables);

                this.BindCommand(ViewModel,
                    vm => vm.SaveShipCommand,
                    v => v.SaveShipBtn)
                    .DisposeWith(disposables);

                this.BindCommand(ViewModel,
                    vm => vm.SaveShipAsCommand,
                    v => v.SaveShipAsBtn)
                    .DisposeWith(disposables);

                this.BindCommand(ViewModel,
                    vm => vm.CloseShipCommand,
                    v => v.CloseShipBtn)
                    .DisposeWith(disposables);

                this.BindCommand(ViewModel,
                    vm => vm.CloseAllCommand,
                    v => v.CloseAllBtn)
                    .DisposeWith(disposables);

                this.BindCommand(ViewModel,
                    vm => vm.ExitCommand,
                    v => v.ExitBtn)
                    .DisposeWith(disposables);
            });

            DataContext = viewModel;
        }
    }
}