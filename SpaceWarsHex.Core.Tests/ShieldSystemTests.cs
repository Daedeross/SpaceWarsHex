using Moq;
using SpaceWars.Interfaces.Prototypes;
using SpaceWars.Interfaces.Systems;
using SpaceWars.Model;
using SpaceWars.Systems;
using System.Collections.Generic;
using Xunit;

namespace SpaceWars.Tests
{
    public class ShieldSystemTests
    {
        public static readonly IEnumerable<DamageThreshold> DamageThresholds =
            [
                new() { HullStrength = 1f   , SystemMultiplier = 1f     },
                new() { HullStrength = 0.75f, SystemMultiplier = 0.875f },
                new() { HullStrength = 0.5f , SystemMultiplier = 0.75f  },
                new() { HullStrength = 0.25f, SystemMultiplier = 0.5f   }
            ];

        public static readonly TheoryData<int> ShieldPrototypeTestData =
            new()
            {
                { 0  },
                { 1  },
                { 10 },
                { 13 },
                { 20 },
            };

        [Theory]
        [MemberData(nameof(ShieldPrototypeTestData))]
        public void ShieldSetsCorrectValuesFromPrototype(int maxPower)
        {
            var prototype = new Mock<IShieldsPrototype>();
            prototype.SetupGet(p => p.MaxPower).Returns(maxPower);

            IShields sheilds = new Shields(prototype.Object);

            Assert.Equal(maxPower, sheilds.MaxPower);
        }

        [Theory]
        [MemberData(nameof(ShieldPrototypeTestData))]
        public void ShieldSetsCorrectDefaultValues(int maxPower)
        {
            var prototype = new Mock<IShieldsPrototype>();
            prototype.SetupGet(p => p.MaxPower).Returns(maxPower);

            IShields sheilds = new Shields(prototype.Object);

            Assert.Equal(0, sheilds.CurrentPower);
            Assert.Equal(0, sheilds.AllocatedPower);
            Assert.Equal(maxPower, sheilds.MaxPowerAvailable);
        }

        /// <summary>
        /// Expected value based on max-power of 13
        /// </summary>
        public static TheoryData<int, int, int> ShieldDamageTestData
            = new()
            {
                { 12, 12, 13 },
                { 11, 12, 13 },
                { 9 , 12, 11 },
                { 8 , 12, 11 },
                { 6 , 12, 9  },
                { 5 , 12, 9  },
                { 3 , 12, 6  },
                { 2 , 12, 6  },
            };

        [Theory]
        [MemberData(nameof(ShieldDamageTestData))]
        public void ShieldCalculatesCorrectAvailablePower(int currentHull, int maxHull, int expected)
        {
            var prototype = new Mock<IShieldsPrototype>();
            prototype.SetupGet(p => p.MaxPower).Returns(13);
            prototype.SetupGet(p => p.DamageThresholds).Returns(DamageThresholds);

            IShields sheilds = new Shields(prototype.Object);
            sheilds.ApplyDamage(currentHull, maxHull);

            Assert.Equal(expected, sheilds.MaxPowerAvailable);
        }
    }
}
