using DynamicData.Binding;

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public class WorkspaceViewModel : ViewModelBase
    {
        public WorkspaceViewModel()
        {
        }

        public IObservableCollection<ShipViewModel> Ships { get; } = new ObservableCollectionExtended<ShipViewModel>();
    }
}
