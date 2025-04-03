using Newtonsoft.Json;
using SpaceWars.Model;
using System.Text;
using Xunit;

namespace SpaceWars.Tests
{
    public class HexVectorTests
    {
        public static TheoryData<HexVector2> TestVectors =
            new()
            {
                new(0, 0),
                new(0, 1),
                new(1, 0),
                new(0, -5),
                new(-5, 0),
                new(7, 13),
                new(7, 3),
                new(-10, -5),
            };

        [Theory]
        [MemberData(nameof(TestVectors))]
        public void HexVectorsWithSameComponentsShouldBeEqual(HexVector2 input)
        {
            var newVector = new HexVector2(input.X, input.Y);
            Assert.Equal(input, newVector);
        }

        [Theory]
        [MemberData(nameof(TestVectors))]
        public void HexVectorsWithDifferentComponentsShouldNotBeEqual(HexVector2 input)
        {
            var newVector = new HexVector2(input.X, input.Y + 1);
            Assert.NotEqual(input, newVector);
        }

        [Theory]
        [MemberData(nameof(TestVectors))]
        public void HexVectorSerializeTest(HexVector2 input)
        {
            var serializer = new JsonSerializer();
            using var mem = new MemoryStream();

            using (var writer = new StreamWriter(mem, Encoding.UTF8, 1024, true))
            {
                serializer.Serialize(writer, input);
            }

            mem.Seek(0, SeekOrigin.Begin);

            using (var reader = new StreamReader(mem))
            {
                var output = serializer.Deserialize(reader, typeof(HexVector2));

                Assert.Equal(input, output);
            }
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
