using Newtonsoft.Json;
using SpaceWarsHex.Model;
using SpaceWarsHex.Core.Tests;
using System.Diagnostics.CodeAnalysis;
using Xunit.Sdk;

[assembly: RegisterXunitSerializer(typeof(TestDataSerializer), typeof(HexVector2), typeof(HashSet<HexVector2>))]

namespace SpaceWarsHex.Core.Tests
{
    public class TestDataSerializer : IXunitSerializer
    {
        public static JsonSerializer _serializer = new JsonSerializer();

        public object Deserialize(Type type, string serializedValue)
        {
            if (type == typeof(HexVector2))
            {
                return JsonConvert.DeserializeObject<HexVector2>(serializedValue);
            }
            else if (type == typeof(HashSet<HexVector2>))
            {
                return new HashSet<HexVector2>(JsonConvert.DeserializeObject<List<HexVector2>>(serializedValue) ?? []);
            }
            else throw new NotSupportedException();
        }

        public bool IsSerializable(Type type, object? value, [NotNullWhen(false)] out string? failureReason)
        {
            if (type == typeof(HexVector2))
            {
                failureReason = null;
                return true;
            }
            if (type == typeof(HashSet<HexVector2>))
            {
                failureReason = null;
                return true;
            }

            failureReason = $"{type} not supported";
            return false;
        }

        public string Serialize(object value)
        {
            if (value is HexVector2 hv)
            {
                return JsonConvert.SerializeObject(hv);
            }
            else if (value is HashSet<HexVector2> set)
            {
                return JsonConvert.SerializeObject(set.ToList());
            }
            throw new NotSupportedException(value.GetType().ToString());
        }
    }
}
