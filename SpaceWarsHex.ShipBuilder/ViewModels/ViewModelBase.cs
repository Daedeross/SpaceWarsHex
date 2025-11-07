using ReactiveUI;
using System.Reactive.Disposables;

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject, IDisposable
    {
        protected readonly CompositeDisposable _disposables = new();

        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _disposables.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
