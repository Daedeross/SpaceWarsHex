using ReactiveUI;
using SpaceWarsHex.ShipBuilder.ViewModels;
using System.Reactive.Disposables.Fluent;

namespace SpaceWarsHex.ShipBuilder.Views
{
    /// <summary>
    /// Interaction logic for ShipView.xaml
    /// </summary>
    public partial class ShipView : ReactiveUserControl<ShipViewModel>
    {
        public ShipView()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.Bind(ViewModel,
                    vm => vm.Name,
                    v => v.NameText.Text)
                .DisposeWith(disposables);

                this.OneWayBind(ViewModel,
                    vm => vm.Reactor,
                    v => v.ReactorHost.ViewModel)
                .DisposeWith(disposables);

                this.OneWayBind(ViewModel,
                    vm => vm.Drive,
                    v => v.DriveHost.ViewModel)
                .DisposeWith(disposables);

                this.OneWayBind(ViewModel,
                    vm => vm.Shields,
                    v => v.ShieldsHost.ViewModel)
                .DisposeWith(disposables);

                this.OneWayBind(ViewModel,
                    vm => vm.EnergyWeapons,
                    v => v.EnergyWeaponsHost.ViewModel)
                .DisposeWith(disposables);

                //this.OneWayBind(ViewModel,
                //    vm => vm.EnergyWeapons,
                //    v => v.EnergyWeapons.ItemsSource)
                //.DisposeWith(disposables);

                //this.Bind(ViewModel,
                //    vm => vm.SelectedEnergyWeapon,
                //    v => v.EnergyWeapons.SelectedItem)
                //.DisposeWith(disposables);

                //this.BindCommand(ViewModel,
                //    vm => vm.NewEnergyWeaponCommand,
                //    v => v.AddEnergyWeaponBtn)
                //.DisposeWith(disposables);

                //this.BindCommand(ViewModel,
                //    vm => vm.DeleteEnergyWeaponCommand,
                //    v => v.DelEnergyWeaponBtn)
                //.DisposeWith(disposables);
            });
        }
    }
}
