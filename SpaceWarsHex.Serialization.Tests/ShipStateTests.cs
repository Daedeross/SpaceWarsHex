using SpaceWarsHex.States;
using SpaceWarsHex.States.Entities;
using Xunit;

namespace SpaceWarsHex.Serialization.Tests
{
    public class ShipStateTests
    {
        public static TheoryData<ShipState> TestShipData { get; } = 
        [
            Mocks.TestStates.Ship1,
            Mocks.TestStates.Ship2,
        ];

        [Theory]
        [MemberData(nameof(TestShipData))]
        public void ShipStateShouldSerializeTest(ShipState input)
        {
            using (var stream = new MemoryStream())
            {
                TestTypeModel.Instance.Serialize(stream, input);

                var bytes = stream.ToArray();
                Assert.True(bytes.Length > 0);

                stream.Position = 0;
                var entity = TestTypeModel.Instance.Deserialize<StateBase>(stream);

                Assert.IsType(input.GetType(), entity);
                Assert.Equal(input, entity, RuntimeEqualityComparer.Instance);
            }
        }
    }
}
