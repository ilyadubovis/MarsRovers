using MarsRovers.Domain.Models;

namespace MarsRovers.Test
{
    [TestClass]
    public class DirectionTests
    {
        [TestMethod]
        [DataRow('N', DirectionType.North)]
        [DataRow('n', DirectionType.North)]
        [DataRow('E', DirectionType.East)]
        [DataRow('e', DirectionType.East)]
        [DataRow('S', DirectionType.South)]
        [DataRow('s', DirectionType.South)]
        [DataRow('W', DirectionType.West)]
        [DataRow('w', DirectionType.West)]
        public void TestValidDirections(char abbreviation, DirectionType expectedDirectionType)
        {
            Assert.IsTrue(new Direction(abbreviation).Type == expectedDirectionType);
        }

        [TestMethod]
        [DataRow(' ')]
        [DataRow('A')]
        [DataRow('b')]
        [DataRow('1')]
        public void TestInvalidDirections(char abbreviation)
        {
            Assert.ThrowsException<ArgumentException>(() => new Direction(abbreviation));
        }
    }
}