using Microsoft.Win32;
using ReactiveUI;
using SpaceWarsHex.ShipBuilder.ViewModels;
using System.Reactive.Disposables.Fluent;

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

                Interactions.ShowSaveDialog.RegisterHandler(
                    interaction =>
                    {
                        var path = ShowDialog(new SaveFileDialog());
                        interaction.SetOutput(path);
                    })
                .DisposeWith(disposables);

                Interactions.ShowOpenDialog.RegisterHandler(
                    interaction =>
                    {
                        var path = ShowDialog(new OpenFileDialog());
                        interaction.SetOutput(path);
                    })
                .DisposeWith(disposables);

                this.OneWayBind(ViewModel,
                    vm => vm.Ships,
                    v => v.ShipsItemsControl.ItemsSource)
                    .DisposeWith(disposables);

                this.Bind(ViewModel,
                    vm => vm.CurrentShip,
                    v => v.ShipsItemsControl.SelectedItem)
                    .DisposeWith(disposables);

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

        private static string? ShowDialog(FileDialog dialog)
        {
            dialog.DefaultExt = ".swp";
            dialog.Filter = "SpaceWars Prototype|*.swp";
            var result = dialog.ShowDialog();

            return result == true
                ? dialog.FileName
                : null;
        }
    }
}