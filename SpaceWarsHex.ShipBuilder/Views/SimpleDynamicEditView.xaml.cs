using ReactiveUI;
using SpaceWarsHex.ShipBuilder.ViewModels;
using System.Reactive.Disposables.Fluent;

namespace SpaceWarsHex.ShipBuilder.Views
{
    /// <summary>
    /// Interaction logic for SimpleDynamicEditView.xaml
    /// </summary>
    public partial class SimpleDynamicEditView : ReactiveUserControl<SimpleDynamicViewModel>
    {
        public SimpleDynamicEditView()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.OneWayBind(ViewModel,
                                vm => vm.Properties,
                                v => v.PropertyGrid.ItemsSource)
                    .DisposeWith(d);
            });
        }
    }
}
