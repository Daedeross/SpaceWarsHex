using Microsoft.Win32;
using SpaceWarsHex.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWarsHex.ShipBuilder
{
    public class FileSystem : IFileSystem
    {
        private readonly IPrototypeSerializer _serializer;

        private readonly ConcurrentBag<FileStream> _openStreams = [];

        private bool _disposedValue;

        public FileSystem(IPrototypeSerializer serializer)
        {
            _serializer = serializer;
        }

        public T Load<T>(string path)
        {
            return _serializer.Deserialize<T>(path);
        }

        public Stream Open(string path)
        {
            var file = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
            _openStreams.Add(file);
            return file;
        }

        public void Release(string path)
        {
            throw new NotImplementedException();
        }

        public void Save<T>(string path, T data)
        {
            _serializer.Serialize<T>(path, data);
        }

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                while (_openStreams.TryTake(out var file))
                {
                    try
                    {
                        file.Dispose();
                    }
                    catch
                    { }
                }

                _disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~FileSystem()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion // IDisposable
    }
}
