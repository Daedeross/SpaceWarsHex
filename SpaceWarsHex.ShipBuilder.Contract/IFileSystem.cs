using System;
using System.IO;

namespace SpaceWarsHex.ShipBuilder
{
    public interface IFileSystem : IDisposable
    {
        Stream Open(string path);

        void Save<T>(string path, T data);

        T Load<T>(string path);

        void Release(string path);
    }
}
