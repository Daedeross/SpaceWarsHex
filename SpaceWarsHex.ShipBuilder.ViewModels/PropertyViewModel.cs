using ReactiveUI;

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public partial class PropertyViewModel: ReactiveObject
    {
        private readonly Accessor _accessor;
        private readonly object _obj;

        public string Name => _accessor.Name;

        public Type Type => _accessor.Type;

        public object? Value
        {
            get => _accessor.GetValue(_obj);
            set
            {
                _accessor.SetValue(_obj, value);
                this.RaisePropertyChanged(nameof(Value));
            }
        }

        public PropertyViewModel(object obj, Accessor accessor)
        {
            _obj = obj;
            _accessor = accessor;
        }

        internal static PropertyViewModel Create(object obj, Accessor accessor)
        {
            return new PropertyViewModel(obj, accessor);
        }
    }
}
