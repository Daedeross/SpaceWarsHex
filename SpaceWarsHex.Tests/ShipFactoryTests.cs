using SpaceWars.Interfaces.Prototypes;
using SpaceWars.Mock;
using Xunit;

namespace SpaceWarsHex.Tests
{
#pragma warning disable xUnit1045 // Avoid using TheoryData type arguments that might not be serializable
    public class ShipFactoryTests
    {
        public static readonly TheoryData<IShipPrototype> ShipPrototypes =
        [
            Prototypes.Ship1(),
            Prototypes.Ship2(),
        ];

        [Theory]
        [MemberData(nameof(ShipPrototypes))]
        public void ShipConstructorTest(IShipPrototype shipPrototype)
        {
            //var ship = new Ship()
        }
    }
#pragma warning restore xUnit1045 // Avoid using TheoryData type arguments that might not be serializable
}
