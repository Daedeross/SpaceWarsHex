using Newtonsoft.Json;
using SpaceWarsHex.Model;
using System.Text;
using System.Text.Json;
using Xunit;

namespace SpaceWarsHex.Core.Tests
{
    public class HexVectorTests
    {
        public static TheoryData<int, int> TestVectorData =
            new()
            {
                { 0 ,  0},
                { 0 ,  1},
                { 1 ,  0},
                { 0 , -5},
                {-5 ,  0},
                { 7 , 13},
                { 7 ,  3},
                {-10, -5},
            };

        public static TheoryData<HexVector2> TestVectors =
            new()
            {
                new HexVector2(0, 0),
                new HexVector2(0, 1),
                new HexVector2(1, 0),
                new HexVector2(0, -5),
                new HexVector2(-5, 0),
                new HexVector2(7, 13),
                new HexVector2(7, 3),
                new HexVector2(-10, -5),
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
        public void HexVectorNewtonsoftSerializeTest(HexVector2 input)
        {
            var serializer = new Newtonsoft.Json.JsonSerializer();
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

        [Theory(Skip = "Not supporting System.Text.Json yet")]
        [MemberData(nameof(TestVectorData))]
        public void HexVectorSystemSerializationTest(int x, int y)
        {
            //var options = new JsonSerializerOptions(JsonSerializerOptions.Default);
            //options.conver

            var hv = new HexVector2(x, y);

            var str = System.Text.Json.JsonSerializer.Serialize(hv);

            var result = System.Text.Json.JsonSerializer.Deserialize<HexVector2>(str);

            Assert.Equal(hv, result);
        }
    }
}
