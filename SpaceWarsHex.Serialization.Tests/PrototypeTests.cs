using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SpaceWarsHex.Interfaces.Prototypes;
using Xunit;

namespace SpaceWarsHex.Serialization.Tests
{
    public class PrototypeTests
    {
        [Fact]
        public void SerializeMocksTest()
        {
            var ser = new JsonSerializer();
            ser.Converters.Add(new StringEnumConverter());
            using var stream = new StreamWriter("weapons.json");
            var energyWeapons = Mock.Prototypes.Ship1().EnergyWeapons.ToList();
            energyWeapons.AddRange(Mock.Prototypes.Ship2().EnergyWeapons);

            ser.Serialize(stream, new
            {
                EnergyWeapons = energyWeapons
            });
            
        }
    }
}
