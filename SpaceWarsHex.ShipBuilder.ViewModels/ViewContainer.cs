using ReactiveUI;
using System;
using System.ComponentModel;

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public class ViewContainer : ReactiveObject, IViewContainer, IDisposable
    {
        private bool disposedValue;

        public ViewContainer(string title, IDocumentViewModel content, bool ownsContent = false)
            : this(title, content, ownsContent, Guid.NewGuid())
        {

        }

        public ViewContainer(string title, IDocumentViewModel content, bool ownsContent, Guid id)
        {
            _title = title ?? throw new ArgumentNullException(nameof(title));
            _content = content ?? throw new ArgumentNullException(nameof(content));
            Id = id;
            OwnsContent = ownsContent;

#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            _content.PropertyChanged += OnContentChanged;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
        }

        public Guid Id { get; }

        public bool OwnsContent { get; }

        private string _title;
        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        public IDocumentViewModel _content;
        public IViewModel Content => _content;

        private void OnContentChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender == _content && (e.PropertyName == "Name" || e.PropertyName == "Title"))
            {
                Title = _content.Name;
            }
        }

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
                    _content.PropertyChanged -= OnContentChanged;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
                    if (OwnsContent && Content is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}
