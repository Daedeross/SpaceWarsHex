using SpaceWarsHex.Entities;
using SpaceWarsHex.Helpers;
using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Model;
using SpaceWarsHex.Rules;
using Xunit;

namespace SpaceWarsHex.Core.Tests
{
    public class HexMatrixTests
    {
        public static TheoryData<HexVector2> TestVectors =
            new()
            {
                new HexVector2(0, 0)   ,
                new HexVector2(0, 1)   ,
                new HexVector2(1, 0)   ,
                new HexVector2(0, -5)  ,
                new HexVector2(-5, 0)  ,
                new HexVector2(7, 13)  ,
                new HexVector2(7, 3)   ,
                new HexVector2(-10, -5),
            };

        [Theory]
        [MemberData(nameof(TestVectors))]
        public void HexMatrixGetsShipInHexTest(HexVector2 input)
        {
            var ship = new Ship(Mock.Prototypes.Ship1(), new BoardRules());
            ship.Position = input;

            var matrix = new HexMatrix<IHexObject>
            {
                ship
            };

            var result = matrix.GetCollidingEntities(input);
            Assert.Contains(ship, result);
        }

        [Theory]
        [MemberData(nameof(TestVectors))]
        public void HexMatrixHandlesMovingObjectsTest(HexVector2 input)
        {
            var ship = new Ship(Mock.Prototypes.Ship1(), new BoardRules());
            ship.Position = HexVector2.Zero;

            var matrix = new HexMatrix<IHexObject>
            {
                ship
            };

            var preMoveZero = matrix.GetCollidingEntities(HexVector2.Zero);
            // Ensure ship is found at initial position
            Assert.Contains(ship, preMoveZero);

            var preMoveResult = matrix.GetCollidingEntities(input);
            if (input != HexVector2.Zero)
            {
                Assert.Empty(preMoveResult);
            }

            // Do move
            ship.Position = input;

            var postMoveZero = matrix.GetCollidingEntities(HexVector2.Zero);
            // Ensure ship is not found at initial position (except unmovingt case)
            if (input != HexVector2.Zero)
            {
                Assert.Empty(postMoveZero);
            }

            var postMoveResult = matrix.GetCollidingEntities(input);
            Assert.Contains(ship, postMoveResult);
        }
    }
}
