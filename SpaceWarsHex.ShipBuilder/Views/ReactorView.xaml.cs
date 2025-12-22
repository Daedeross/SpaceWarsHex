using ReactiveUI;
using SpaceWarsHex.ShipBuilder.ViewModels;
using System;
using System.Reactive.Disposables.Fluent;

namespace SpaceWarsHex.ShipBuilder.Views
{
    /// <summary>
    /// Interaction logic for ReactorView.xaml
    /// </summary>
    public partial class ReactorView : ReactiveUserControl<ReactorViewModel>
    {
        public ReactorView()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.Bind(ViewModel,
                    vm => vm.CruisePower,
                    v => v.CruiseSpin.Value,
                    vmProperty => Convert.ToDouble(vmProperty),
                    viewProperty => Convert.ToInt32(viewProperty))
                .DisposeWith(d);

                this.Bind(ViewModel,
                    vm => vm.AttackPower,
                    v => v.AttackSpin.Value,
                    vmProperty => Convert.ToDouble(vmProperty),
                    viewProperty => Convert.ToInt32(viewProperty))
                .DisposeWith(d);

                this.Bind(ViewModel,
                    vm => vm.EmergencyPower,
                    v => v.EmergencySpin.Value,
                    vmProperty => Convert.ToDouble(vmProperty),
                    viewProperty => Convert.ToInt32(viewProperty))
                .DisposeWith(d);

                this.Bind(ViewModel,
                    vm => vm.MaxTurnsAtAttackPower,
                    v => v.TurnsSpin.Value,
                    vmProperty => Convert.ToDouble(vmProperty),
                    viewProperty => Convert.ToInt32(viewProperty))
                .DisposeWith(d);

                this.OneWayBind(ViewModel,
                    vm => vm.DamageThresholds,
                    v => v.ThresholdsView.ViewModel)
                .DisposeWith(d);
            });
        }
    }
}
