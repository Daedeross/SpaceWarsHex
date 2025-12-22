using ReactiveUI;
using SpaceWarsHex.ShipBuilder.ViewModels;
using System.Reactive.Disposables.Fluent;

namespace SpaceWarsHex.ShipBuilder.Views
{
    /// <summary>
    /// Interaction logic for WeaponEffectsView.xaml
    /// </summary>
    public partial class WeaponEffectsView : ReactiveUserControl<WeaponEffectsViewModel>
    {
        public WeaponEffectsView()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel,
                    vm => vm.Effects,
                    v => v.EffectsGrid.ItemsSource)
                .DisposeWith(disposables);

                this.Bind(ViewModel,
                    vm => vm.SelectedEffect,
                    v => v.EffectsGrid.SelectedItem)
                .DisposeWith(disposables);

                this.BindCommand(ViewModel,
                    vm => vm.NewEffectCommand,
                    vm => vm.AddBtn)
                .DisposeWith(disposables);

                this.BindCommand(ViewModel,
                    vm => vm.DeleteEffectCommand,
                    vm => vm.RemoveBtn)
                .DisposeWith(disposables);

                this.BindCommand(ViewModel,
                    vm => vm.MoveEffectUpCommand,
                    vm => vm.UpBtn)
                .DisposeWith(disposables);

                this.BindCommand(ViewModel,
                    vm => vm.MoveEffectDownCommand,
                    vm => vm.DownBtn)
                .DisposeWith(disposables);
            });
        }
    }
}
