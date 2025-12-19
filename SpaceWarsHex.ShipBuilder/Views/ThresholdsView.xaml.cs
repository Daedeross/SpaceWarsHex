using ReactiveUI;
using SpaceWarsHex.ShipBuilder.ViewModels;
using System;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;

namespace SpaceWarsHex.ShipBuilder.Views
{
    /// <summary>
    /// Interaction logic for ThresholdsView.xaml
    /// </summary>
    public partial class ThresholdsView : ReactiveUserControl<ThresholdsViewModel>
    {
        public ThresholdsView()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel,
                        vm => vm.Thresholds,
                        v => v.ThresholdsListView.ItemsSource)
                    .DisposeWith(disposables);
            });
        }

        private void CreateLinearBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewInteractions.ShowEditDialog
                .Handle(new SimpleDynamicViewModel(new CreateLinearViewModel()))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(result =>
                {
                    if (result is SimpleDynamicViewModel sdvm)
                    if (sdvm.Model is CreateLinearViewModel vm)
                    {
                        ViewModel?.SetLinear((float)vm.HullEnd, (float)vm.MultiplierEnd, vm.Count);
                    }
                });
        }

        private void CreateDefaultBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel?.SetStandard();
        }
    }
}
