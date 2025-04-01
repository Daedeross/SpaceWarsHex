using Moq;
using SpaceWars.Interfaces.Prototypes;
using SpaceWars.Interfaces.Systems;
using SpaceWars.Model;
using SpaceWars.Systems;
using Xunit;

namespace SpaceWars.Tests
{
    public class DriveSystemTests
    {
        public static IEnumerable<DamageThreshold> DamageThresholds
               = new DamageThreshold[]
               {
                new() { HullStrength = 1f   , SystemMultiplier = 1f    },
                new() { HullStrength = 0.75f, SystemMultiplier = 0.875f},
                new() { HullStrength = 0.5f , SystemMultiplier = 0.75f },
                new() { HullStrength = 0.25f, SystemMultiplier = 0.5f  }
               };

        public static TheoryData<int, int> DrivePrototypeValues =
            new()
            {
                { 5 , 2  },
                { 7 , 3  },
                { 10, 5  },
            };

        [Theory]
        [MemberData(nameof(DrivePrototypeValues))]
        public void DriveSetsCorrectValuesFromPrototype(int maxWarp, int acceleration)
        {
            var prototype = new Mock<IDrivePrototype>();

            prototype.SetupGet(p => p.MaxWarp).Returns(maxWarp);
            prototype.SetupGet(p => p.AccelerationClass).Returns(acceleration);

            IDrive drive = new Drive(prototype.Object);

            Assert.Equal(maxWarp, drive.MaxWarp);
            Assert.Equal(acceleration, drive.AccelerationClass);
        }

        [Theory]
        [MemberData(nameof(DrivePrototypeValues))]
        public void DriveSetsCorrectDefaults(int maxWarp, int acceleration)
        {
            var prototype = new Mock<IDrivePrototype>();

            prototype.SetupGet(p => p.MaxWarp).Returns(maxWarp);
            prototype.SetupGet(p => p.AccelerationClass).Returns(acceleration);

            IDrive drive = new Drive(prototype.Object);

            // Velocity and acceleration should default to zero.
            Assert.Equal(HexVector2.Zero, drive.Velocity);
            Assert.Equal(HexVector2.Zero, drive.Acceleration);
        }

        [Theory]
        [InlineData(5, 5)]
        [InlineData(4, 5)]
        [InlineData(3, 5)]
        [InlineData(2, 5)]
        [InlineData(1, 5)]
        [InlineData(0, 5)]
        public void DriveShouldNotBeAffectedByHullDamage(int currentHull, int maxHull)
        {
            const int warp = 7;
            const int acceleration = 3;

            var prototype = new Mock<IDrivePrototype>();

            prototype.SetupGet(p => p.MaxWarp).Returns(warp);
            prototype.SetupGet(p => p.AccelerationClass).Returns(acceleration);

            IDrive drive = new Drive(prototype.Object);
            drive.ApplyDamage(currentHull, maxHull);

            Assert.Equal(warp, drive.MaxWarp);
            Assert.Equal(acceleration, drive.AccelerationClass);
        }
    }
}
