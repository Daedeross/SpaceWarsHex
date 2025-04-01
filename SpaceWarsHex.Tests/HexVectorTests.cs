﻿using SpaceWars.Model;
using SpaceWarsHex.Bridges;
using Xunit;

namespace SpaceWars.Tests
{
    public class HexVectorTests
    {
        public static TheoryData<HexVector2, HexVector2, HashSet<HexVector2>> HexesAlongData =
            new()
            {
                // simple cases
                { HexVector2.Zero, HexVector2.Zero, new HashSet<HexVector2> { HexVector2.Zero } },
                { HexVector2.UnitX, HexVector2.UnitX, new HashSet<HexVector2> { HexVector2.UnitX } },
                { HexVector2.UnitY, HexVector2.UnitY, new HashSet<HexVector2> { HexVector2.UnitY } },
                { HexVector2.Zero, HexVector2.UnitX, new HashSet<HexVector2> { HexVector2.Zero, HexVector2.UnitX } },
                { HexVector2.Zero, HexVector2.UnitX, new HashSet<HexVector2> { HexVector2.Zero, HexVector2.UnitX } },
                { HexVector2.UnitX, HexVector2.Zero, new HashSet<HexVector2> { HexVector2.Zero, HexVector2.UnitX } },

                // line is along hex's edge
                { HexVector2.Zero, new HexVector2(1, 1), new HashSet<HexVector2> { HexVector2.Zero, new(0, 1), new(1, 0), new(1, 1) } },
                { new HexVector2(-1, -1), new HexVector2(1, 1), new HashSet<HexVector2> { new(0, -1), new(-1, 0), new(-1, -1), HexVector2.Zero, new(0, 1), new(1, 0), new(1, 1) } },

                // complex case
                { new HexVector2(10, 20), new HexVector2(12, 21), new HashSet<HexVector2> { new(10, 20), new(11, 20), new(11, 21), new(12, 21) } }
            };

        [Theory]
        [MemberData(nameof(HexesAlongData))]
        public void GetHexexAlongLineBetweenHexesTest(HexVector2 origin, HexVector2 target, HashSet<HexVector2> expected)
        {
            var actual = new HashSet<HexVector2>(origin.GetHexesTo(target));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void HashCodeShouldBeOrderDependantTest()
        {
            int code1 = HashCode.Combine(0, 1);
            int code2 = HashCode.Combine(1, 0);

            Assert.NotEqual(code1, code2);
        }
    }
}
