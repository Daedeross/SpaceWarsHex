using Newtonsoft.Json;
using SpaceWarsHex.Interfaces;
using System.IO;

namespace SpaceWarsHex.Serialization
{
    public class PrototypeSerializer : IPrototypeSerializer
    {
        private readonly JsonSerializer _serializer;

        public PrototypeSerializer(JsonSerializer serializer)
        {
            _serializer = serializer;
        }

        public T Deserialize<T>(Stream stream)
        {
            var reader = new StreamReader(stream);
            return (T)_serializer.Deserialize(reader, typeof(T))!;
        }

        public T Deserialize<T>(string path)
        {
            using var reader = new StreamReader(path);
            return (T)_serializer.Deserialize(reader,typeof(T))!;
        }

        public void Serialize<T>(Stream stream, T prototype)
        {
            using var writer = new StreamWriter(stream);
            _serializer.Serialize(writer, prototype);
        }

        public void Serialize<T>(string path, T prototype)
        {
            using var writer = new StreamWriter(path);
            _serializer.Serialize(writer, prototype);
        }
    }
}
