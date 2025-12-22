using ReactiveUI.SourceGenerators;
using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Interfaces.Prototypes;
using System.Reactive;

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public abstract partial class DocumentViewModelBase<T> : ViewModelBase, IViewModel<T>
        where T : class, IPrototype, new()
    {
        protected readonly IPrototypeSerializer _serializer;

        protected string? _filePath = null;

        protected T _saved;

        public DocumentViewModelBase(T prototype, IPrototypeSerializer serializer)
        {
            _saved = prototype;
            _serializer = serializer;
        }

        [ReactiveCommand]
        private void Save()
        {
            if (_filePath is null)
            {
                SaveAs();
            } 
            else
            {
                SavePrototype();
            }
        }

        [ReactiveCommand]
        private void SaveAs()
        {
            Interactions.ShowSaveDialog.Handle(Unit.Default)
                .Subscribe(SaveToFile);
        }

        private void SaveToFile(string? path)
        {
            if (path is null) { return; }

            _filePath = path;
            SavePrototype();
        }

        private void SavePrototype()
        {
            if (_filePath is null)
            {
                throw new NullReferenceException("_filePath is null, should not be here...");
            }
            _serializer.Serialize(_filePath, _saved);
        }

        public T GetLast()
        {
            return _saved;
        }

        public T Commit()
        {
            SaveTo(_saved);

            return _saved;
        }

        public abstract void SaveTo(T prototype);

        public abstract void LoadFrom(T prototype);
    }
}
