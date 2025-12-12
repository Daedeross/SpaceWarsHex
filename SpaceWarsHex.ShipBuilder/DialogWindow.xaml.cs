using ReactiveUI;
using System.Reactive.Disposables.Fluent;
using System.Windows;

namespace SpaceWarsHex.ShipBuilder
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class DialogWindow : Window, IActivatableView, IViewFor
    {
        private readonly object _viewModel;

        public DialogWindow(object viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;

            this.WhenActivated(disposables =>
            {
                DataContext = _viewModel;

                //this.OneWayBind(ViewModel,
                //    vm => vm,
                //    v => v.VMHost.ViewModel)
                //.DisposeWith(disposables);

                VMHost.ViewModel = _viewModel;
            });
        }

        

        public object ViewModel => _viewModel;

        object? IViewFor.ViewModel { get => ViewModel; set => throw new System.NotImplementedException(); }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
