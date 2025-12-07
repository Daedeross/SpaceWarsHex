using ReactiveUI;
using System.Reactive.Disposables;
using SpaceWarsHex.ShipBuilder.ViewModels;

namespace SpaceWarsHex.ShipBuilder.Views
{
    /// <summary>
    /// Interaction logic for WorkspaceView.xaml
    /// </summary>
    public partial class WorkspaceView : ReactiveUserControl<WorkspaceViewModel>
    {
        public WorkspaceView()
        {
            InitializeComponent();

            //this.WhenActivated(disposables =>
            //{
            //    this.OneWayBind(ViewModel,
            //        vm => vm.Ships,
            //        v => v.ShipsItemsControl.ItemsSource)
            //    .DisposeWith(disposables);
            //} );
        }
    }
}
