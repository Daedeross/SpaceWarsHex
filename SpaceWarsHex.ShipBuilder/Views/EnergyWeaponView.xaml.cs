using ReactiveUI;
using SpaceWarsHex.ShipBuilder.Configuration;
using SpaceWarsHex.ShipBuilder.ViewModels;
using System;
using System.Reactive.Disposables.Fluent;

namespace SpaceWarsHex.ShipBuilder.Views
{
    /// <summary>
    /// Interaction logic for EnergyWeaponView.xaml
    /// </summary>
    [Component(Name = "full")]
    public partial class EnergyWeaponView : ReactiveUserControl<EnergyWeaponViewModel>
    {
        public EnergyWeaponView()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.Bind(ViewModel,
                    vm => vm.FireMode,
                    v => v.FireModeSelect.SelectedItem)
                    .DisposeWith(disposables);

                this.Bind(ViewModel,
                        vm => vm.MaxDice,
                        v => v.MaxDiceSpin.Value,
                        vmProperty => Convert.ToDouble(vmProperty),
                        viewProperty => Convert.ToInt32(viewProperty))
                    .DisposeWith(disposables);
                this.Bind(ViewModel,
                        vm => vm.EnergyPerDie,
                        v => v.EnergyPerDieSpin.Value,
                        vmProperty => Convert.ToDouble(vmProperty),
                        viewProperty => Convert.ToInt32(viewProperty))
                    .DisposeWith(disposables);
                this.Bind(ViewModel,
                        vm => vm.MaxRange,
                        v => v.MaxRangeSpin.Value,
                        vmProperty => Convert.ToDouble(vmProperty),
                        viewProperty => Convert.ToInt32(viewProperty))
                    .DisposeWith(disposables);
                this.Bind(ViewModel,
                        vm => vm.FirePhase,
                        v => v.FirePhaseSelect.SelectedItem)
                    .DisposeWith(disposables);

                this.OneWayBind(ViewModel,
                        vm => vm.MaxEnergy,
                        v => v.MaxEnergyText.Text)
                    .DisposeWith(disposables);

                this.Bind(ViewModel,
                        vm => vm.Visual,
                        v => v.VisualCheckBox.IsChecked)
                    .DisposeWith(disposables);

                this.Bind(ViewModel,
                        vm => vm.Effects,
                        v => v.EffectsView.ViewModel)
                    .DisposeWith(disposables);

                this.Bind(ViewModel,
                        vm => vm.DamageThresholds,
                        v => v.ThresholdsView.ViewModel)
                    .DisposeWith(disposables);
            });
        }
    }
}
