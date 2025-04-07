using Moq;
using SpaceWarsHex.Entities;
using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Interfaces.Bridges;
using SpaceWarsHex.Model;
using SpaceWarsHex.Rules;
using Xunit;

namespace SpaceWarsHex.Core.Tests
{
    public class DirectorTests
    {
        [Fact]
        public void DirectorCallesEntityFactoryTest()
        {
            var wrapperFactoryMock = new Mock<IWrapperFactory<IHexObject, IWrapper<IHexObject>>>();
            var factory = new EntityFactory<IWrapper<IHexObject>> (wrapperFactoryMock.Object, new BoardRules());

            var director = new Director(factory);

            var entity = director.CreateEntity(Mock.Prototypes.Ship1(), null, HexVector2.UnitX);

            Assert.IsAssignableFrom<IShip>(entity);
            Assert.Equal(HexVector2.UnitX, ((IShip)entity).Position);
        }
    }
}
