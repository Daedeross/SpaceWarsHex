using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SpaceWarsHex.Prototypes;

#nullable enable

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public partial class HullViewModel : ViewModelBase, IViewModel<HullPrototype>
    {
        private HullPrototype _saved;

        [Reactive]
        private int _maxIntegrity;

        public HullViewModel()
            : this(new HullPrototype() { MaxIntegrity = 1 })
        { }

        public HullViewModel(HullPrototype prototype)
        {
            _saved = prototype.GetOrThrow();

            LoadFrom(_saved);
        }

        public void LoadFrom(HullPrototype prototype)
        {
            _saved = prototype;
            MaxIntegrity = prototype.MaxIntegrity;
        }

        public void SaveTo(HullPrototype prototype)
        {
            prototype.MaxIntegrity = MaxIntegrity;
        }

        [ReactiveCommand]
        private void Save()
        {
            if (_saved != null)
            {
                SaveTo(_saved);
            }
        }

        [ReactiveCommand]
        private void Reset()
        {
            if (_saved != null)
            {
                LoadFrom(_saved);
            }
        }

        public HullPrototype GetLast()
        {
            return _saved;
        }

        public HullPrototype Commit()
        {
            SaveTo(_saved);

            return _saved;
        }
    }
}