using ReactiveUI.SourceGenerators;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Reflection;

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public partial class SimpleDynamicViewModel : ViewModelBase
    {
        private static readonly ConcurrentDictionary<Type, List<Accessor>> _accessorsCache = new();

        [Reactive]
        private object _model;

        [Reactive]
        private ObservableCollection<PropertyViewModel> _properties;

        public Type ModelType { get; }

        public SimpleDynamicViewModel(object model)
        {
            _model = model.GetOrThrow();
            ModelType = model.GetType();
            _properties = new ObservableCollection<PropertyViewModel>(
                GetOrCreateAccessors(ModelType)
                .Select(acc => new PropertyViewModel(_model, acc))
                );
        }

        private static List<Accessor> GetOrCreateAccessors(Type type)
        {
            return _accessorsCache.GetOrAdd(type, t =>
            {
                return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(pi => pi.GetMethod != null && pi.SetMethod != null)
                    .Select(Accessor.Create)
                    .ToList();
            });
        }
    }
}
