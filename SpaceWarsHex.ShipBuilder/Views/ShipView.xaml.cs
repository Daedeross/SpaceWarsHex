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
            });
        }
    }
}
