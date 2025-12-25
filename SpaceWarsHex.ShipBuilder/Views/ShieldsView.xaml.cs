using ReactiveUI;
using SpaceWarsHex.ShipBuilder.ViewModels;
using System;
using System.Reactive.Disposables.Fluent;

namespace SpaceWarsHex.ShipBuilder.Views
{
    /// <summary>
    /// Interaction logic for ShieldsView.xaml
    /// </summary>
    public partial class ShieldsView : ReactiveUserControl<ShieldsViewModel>
    {
        public ShieldsView()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.Bind(ViewModel,
                    vm => vm.MaxPower,
                    v => v.PowerSpin.Value,
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
