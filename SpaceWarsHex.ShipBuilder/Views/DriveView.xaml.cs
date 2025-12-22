using ReactiveUI;
using SpaceWarsHex.ShipBuilder.ViewModels;
using System;
using System.Reactive.Disposables.Fluent;

namespace SpaceWarsHex.ShipBuilder.Views
{
    /// <summary>
    /// Interaction logic for DriveView.xaml
    /// </summary>
    public partial class DriveView : ReactiveUserControl<DriveViewModel>
    {
        public DriveView()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.Bind(ViewModel,
                        vm => vm.MaxWarp,
                        v => v.WarpSpin.Value,
                        vmProperty => Convert.ToDouble(vmProperty),
                        viewProperty => Convert.ToInt32(viewProperty))
                    .DisposeWith(disposables);

                this.Bind(ViewModel,
                        vm => vm.AccelerationClass,
                        v => v.AccelerationSpin.Value,
                        vmProperty => Convert.ToDouble(vmProperty),
                        viewProperty => Convert.ToInt32(viewProperty))
                    .DisposeWith(disposables);

                this.Bind(ViewModel,
                        vm => vm.DamageThresholds,
                        v => v.ThresholdsView.ViewModel)
                    .DisposeWith(disposables);
            });
        }
    }
}
