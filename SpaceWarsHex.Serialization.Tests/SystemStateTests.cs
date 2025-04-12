using SpaceWarsHex.Model;
using SpaceWarsHex.States;
using Xunit;

namespace SpaceWarsHex.Serialization.Tests
{
    public class SystemStateTests
    {
        [Fact]
        public void ReactorStateShouldSerializeTest()
        {

            var input = new States.Systems.ReactorState
            {
                Id = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C5941100042"),
                Hash = 42,
                Name = "I am a Reactor",
                CurrentState = ReactorState.Attack,
                PreviousState = ReactorState.Cruise,
                UsingEmergencyPower = false,
                UsedEmergencyPowerLastTurn = false,
                CurrentTurnPenalty = 0,
                NextTurnPenalty = 1,
                TurnsSpentAtAttackPower = 1,
                CurrentMaxPower = 15
            } as StateBase;

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
