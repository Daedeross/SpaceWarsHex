using SpaceWars.Entities;
using SpaceWars.Helpers;
using SpaceWars.Interfaces;
using SpaceWars.Mock;
using SpaceWars.Model;
using SpaceWars.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SpaceWarsHex.Core.Tests
{
    public class HexMatrixTests
    {
        public static TheoryData<HexVector2> TestVectors =
            new()
            {
                new(0, 0)   ,
                new(0, 1)   ,
                new(1, 0)   ,
                new(0, -5)  ,
                new(-5, 0)  ,
                new(7, 13)  ,
                new(7, 3)   ,
                new(-10, -5),
            };

        [Theory]
        [MemberData(nameof(TestVectors))]
        public void HexMatrixGetsShipInHexTest(HexVector2 input)
        {
            var ship = new Ship(Prototypes.Ship1(), new BoardRules());
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
            var ship = new Ship(Prototypes.Ship1(), new BoardRules());
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
