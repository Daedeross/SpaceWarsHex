using SpaceWarsHex.Entities;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Mock;
using SpaceWarsHex.Rules;
using Xunit;

namespace SpaceWarsHex.Tests
{
#pragma warning disable xUnit1045 // Avoid using TheoryData type arguments that might not be serializable
    public class ShipFactoryTests
    {
        public static readonly TheoryData<IShipPrototype> ShipPrototypes =
            new()
            {
                Mock.Prototypes.Ship1(),
                Mock.Prototypes.Ship2(),
            };

        [Theory]
        [MemberData(nameof(ShipPrototypes))]
        public void ShipConstructorTest(IShipPrototype shipPrototype)
        {
            var ship = new Ship(shipPrototype, new BoardRules());
        }
    }
#pragma warning restore xUnit1045 // Avoid using TheoryData type arguments that might not be serializable
}
