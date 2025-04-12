using ProtoBuf.Meta;
using SpaceWarsHex.Model;

namespace SpaceWarsHex.Serialization.Tests
{
    public static class TestTypeModel
    {
        public static RuntimeTypeModel Instance { get; }

        static TestTypeModel()
        {
            Instance = RuntimeTypeModel.Create(nameof(TestTypeModel));
            AddHexVector2();
        }

        private static void AddHexVector2()
        {
            var meta = Instance.Add(typeof(HexVector2));
            meta.AddField(1, nameof(HexVector2.X));
            meta.AddField(2, nameof(HexVector2.Y));
        }
    }
}
