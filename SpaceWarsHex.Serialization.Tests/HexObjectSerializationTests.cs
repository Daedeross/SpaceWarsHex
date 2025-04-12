using ProtoBuf;
using SpaceWarsHex.Model;
using SpaceWarsHex.States;
using SpaceWarsHex.States.Entities;
using Xunit;

namespace SpaceWarsHex.Serialization.Tests
{
    public class HexObjectSerializationTests
    {
        [ProtoContract]
        public class TestState
        {
            [ProtoMember(1)]
            public Guid Prop1 { get; set; }
            [ProtoMember(2)]
            public int Prop2 { get; set; }
        }

        public static readonly TheoryData<StateBase> TestStates = [
            new StateBase
            {
                Id = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C5941100001"),
                Hash = 1,
            },
            new HexObjectState
            {
                Id = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C5941100002"),
                Hash = 2,
                Name = "HexObject",
                TeamNumber = 1,
                Position = new HexVector2(0, 1)
            } as StateBase,
            new MovingHexObjectState
            {
                Id = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C5941100002"),
                Hash = 3,
                Name = "MovingHexObject",
                TeamNumber = 1,
                Position = new HexVector2(0, 1),
                Velocity = new HexVector2(-2, -1),
            } as StateBase,
        ];

        [Theory]
        [MemberData(nameof(TestStates))]
        public void HexObjectProtobufTest(StateBase state)
        {

            using (var stream = new MemoryStream())
            {
                TestTypeModel.Instance.Serialize(stream, state);

                var bytes = stream.ToArray();
                Assert.True(bytes.Length > 0);

                stream.Position = 0;
                var entity = TestTypeModel.Instance.Deserialize<StateBase>(stream);

                Assert.IsType(state.GetType(), entity);
                Assert.Equal(state, entity, RuntimeEqualityComparer.Instance);
            }
        }

        [Fact]
        public void ProtobufHandlesHexVectors()
        {
            var state = new HexVector2(1, -5);

            using (var stream = new MemoryStream())
            {
                TestTypeModel.Instance.Serialize(stream, state);

                var bytes = stream.ToArray();
                Assert.True(bytes.Length > 0);

                stream.Position = 0;

                var entity = TestTypeModel.Instance.Deserialize<HexVector2>(stream);

                Assert.IsType(state.GetType(), entity);
                Assert.Equal(entity, state);
            }
        }

        [Fact]
        public void ProtobufHandlesHexObjects()
        {
            var state = new HexObjectState
            {
                Id = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C5941100002"),
                Hash = 2,
                TeamNumber = 1,
                Position = new HexVector2(0, 1)
            };

            using (var stream = new MemoryStream())
            {
                TestTypeModel.Instance.Serialize(stream, state);

                var bytes = stream.ToArray();
                Assert.True(bytes.Length > 0);

                stream.Position = 0;

                var entity = TestTypeModel.Instance.Deserialize<HexObjectState>(stream);

                Assert.NotNull(entity);
                Assert.IsType(state.GetType(), entity);

                Assert.Equal(state.Id, entity.Id);
                Assert.Equal(state.Hash, entity.Hash);
                Assert.Equal(state.TeamNumber, entity.TeamNumber);
                Assert.Equal(state.Position, entity.Position);
            }
        }

        [Fact]
        public void ProtobufHandlesPolymorhism()
        {
            var state = new HexObjectState
            {
                Id = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C5941100002"),
                Hash = 2,
                TeamNumber = 1,
                Position = new HexVector2(0, 1)
            } as StateBase;

            using (var stream = new MemoryStream())
            {
                TestTypeModel.Instance.Serialize(stream, state);

                var bytes = stream.ToArray();
                Assert.True(bytes.Length > 0);

                stream.Position = 0;

                var entity = TestTypeModel.Instance.Deserialize<StateBase>(stream);

                Assert.IsType(state.GetType(), entity);
                Assert.Equal(state, entity, RuntimeEqualityComparer.Instance);
            }
        }

        [Fact]
        public void ProtobufHandlesDeepTree()
        {
            var state = new MovingHexObjectState
            {
                Id = Guid.Parse("3A2AD62F-146B-47EE-85F2-2C5941100002"),
                Hash = 2,
                Name = "MovingHexObject",
                TeamNumber = 1,
                Position = new HexVector2(0, 1),
                Velocity = new HexVector2(0, 1),
            } as StateBase;

            using (var stream = new MemoryStream())
            {
                TestTypeModel.Instance.Serialize(stream, state);

                var bytes = stream.ToArray();
                Assert.True(bytes.Length > 0);

                stream.Position = 0;

                var entity = TestTypeModel.Instance.Deserialize<StateBase>(stream);

                Assert.IsType(state.GetType(), entity);
                Assert.Equal(state, entity, RuntimeEqualityComparer.Instance);
            }
        }
    }
}
