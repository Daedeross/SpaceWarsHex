using Moq;
using SpaceWars.Interfaces.Prototypes;
using SpaceWars.Interfaces.Systems;
using SpaceWars.Model;
using SpaceWars.Systems;
using Xunit;

namespace SpaceWars.Tests
{
    public class ReactorSystemTests
    {
        public static readonly IEnumerable<DamageThreshold> DamageThresholds =
            [
                new() { HullStrength = 1f   , SystemMultiplier = 1f    },
                new() { HullStrength = 0.75f, SystemMultiplier = 0.875f},
                new() { HullStrength = 0.5f , SystemMultiplier = 0.75f },
                new() { HullStrength = 0.25f, SystemMultiplier = 0.5f  }
            ];

        public static TheoryData<int, int, int, int> ReactorPrototypeValues =
            new()
            {
                {1 , 2 , 3, 4},       // simple
                {2 , 3 , 5, 7},       // primes
                {13, 20, 4, 4},       // From Klingon Heavy Cruiser
                {20, 20, 2, 0},       // Ship without Attack Power (format 1)
                {20, 0 , 2, 0},       // Ship without Attack Power (format 2)
            };

        [Theory]
        [MemberData(nameof(ReactorPrototypeValues))]
        public void ReactorSetsCorrectValuesFromPrototype(int cruise, int attack, int emergency, int turns)
        {
            var prototype = new Mock<IReactorPrototype>();

            prototype.SetupGet(p => p.CruisePower).Returns(cruise);
            prototype.SetupGet(p => p.AttackPower).Returns(attack);
            prototype.SetupGet(p => p.EmergencyPower).Returns(emergency);
            prototype.SetupGet(p => p.MaxTurnsAtAttackPower).Returns(turns);
            prototype.SetupGet(p => p.DamageThresholds).Returns(DamageThresholds);

            IReactor reactor = new Reactor(prototype.Object);

            Assert.Equal(cruise, reactor.CruisePower);
            Assert.Equal(attack, reactor.AttackPower);
            Assert.Equal(emergency, reactor.EmergencyPower);
            Assert.Equal(turns, reactor.MaxTurnsAtAttackPower);
        }

        [Theory]
        [MemberData(nameof(ReactorPrototypeValues))]
        public void ReactorSetsCorrectInitialValues(int cruise, int attack, int emergency, int turns)
        {
            var prototype = new Mock<IReactorPrototype>();

            prototype.SetupGet(p => p.CruisePower).Returns(cruise);
            prototype.SetupGet(p => p.AttackPower).Returns(attack);
            prototype.SetupGet(p => p.EmergencyPower).Returns(emergency);
            prototype.SetupGet(p => p.MaxTurnsAtAttackPower).Returns(turns);
            prototype.SetupGet(p => p.DamageThresholds).Returns(DamageThresholds);

            IReactor reactor = new Reactor(prototype.Object);

            // Assert defauls
            Assert.Equal(ReactorState.Cruise, reactor.CurrentState);
            Assert.False(reactor.UsingEmergencyPower);
            Assert.Equal(ReactorState.Cruise, reactor.PreviousState);
            Assert.False(reactor.UsedEmergencyPowerLastTurn);
            Assert.Equal(0, reactor.CurrentTurnPenalty);
            Assert.Equal(0, reactor.NextTurnPenalty);
            Assert.Equal(0, reactor.PowerAllocated);
            Assert.Equal(0, reactor.TurnsSpentAtAttackPower);

            // Assert dependent value
            Assert.Equal(cruise, reactor.CurrentAvailablePower);
        }

        /// <summary>
        /// Expected is based on Cruise = 13, Attack = 20, Emergency = 4
        /// </summary>
        public static TheoryData<ReactorState, bool, int, int, int, int> ReactorStateTestData
            = new()
            {
                { ReactorState.Cruise, false, 0, 12, 12, 13 },
                { ReactorState.Cruise, true , 0, 12, 12, 17 },
                { ReactorState.Attack, false, 0, 12, 12, 20 },
                { ReactorState.Attack, true , 0, 12, 12, 24 },
                { ReactorState.Cruise, false, 1, 12, 12, 12 },
                { ReactorState.Cruise, true , 1, 12, 12, 16 },
                { ReactorState.Attack, false, 1, 12, 12, 19 },
                { ReactorState.Attack, true , 1, 12, 12, 23 },
                { ReactorState.Attack, false, 0, 11, 12, 20 },  // ensure that thresold is not hit early
                { ReactorState.Cruise, false, 0, 6 , 12, 13 },
                { ReactorState.Cruise, true , 0, 6 , 12, 17 },
                { ReactorState.Attack, false, 0, 6 , 12, 15 },
                { ReactorState.Attack, true , 0, 6 , 12, 19 },
                { ReactorState.Cruise, false, 1, 6 , 12, 12 },
                { ReactorState.Cruise, true , 1, 6 , 12, 16 },
                { ReactorState.Attack, false, 1, 6 , 12, 14 },
                { ReactorState.Attack, true , 1, 6 , 12, 18 },
            };

        [Theory]
        [MemberData(nameof(ReactorStateTestData))]
        public void ReactorCalculatesCorrectAvailablePower(ReactorState state, bool usingEmergency, int penalty, int currentHull, int maxHull, int expected)
        {
            var prototype = new Mock<IReactorPrototype>();

            prototype.SetupGet(p => p.CruisePower).Returns(13);
            prototype.SetupGet(p => p.AttackPower).Returns(20);
            prototype.SetupGet(p => p.EmergencyPower).Returns(4);
            prototype.SetupGet(p => p.MaxTurnsAtAttackPower).Returns(5);
            prototype.SetupGet(p => p.DamageThresholds).Returns(DamageThresholds);

            IReactor reactor = new Reactor(prototype.Object);

            // setup state
            reactor.CurrentState = state;
            reactor.UsingEmergencyPower = usingEmergency;
            reactor.CurrentTurnPenalty = penalty;

            reactor.ApplyDamage(currentHull, maxHull);

            // Assert
            Assert.Equal(expected, reactor.CurrentAvailablePower);
        }
    }
}