using MarsRovers.Domain.Models;

namespace MarsRovers.Test
{
    [TestClass]
    public class ExplorationAreaTests
    {
        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(2, 5)]
        [DataRow(10, 1)]
        public void TestValidExplorationArea2Coordinates(int x, int y)
        {
            _ = new ExplorationArea(new Position(x, y));
            Assert.IsTrue(true); // exception was not thrown during instantiation 
        }

        [TestMethod]
        [DataRow(0, 0, 2, 2)]
        [DataRow(1, 2, 3, 4)]
        public void TestValidExplorationArea4Coordinates(int x0, int y0, int x1, int y1)
        {
            _ = new ExplorationArea(new Position(x0, y0), new Position(x1, y1));
            Assert.IsTrue(true); // exception was not thrown during instantiation 
        }

        [TestMethod]
        [DataRow(0, 0)]
        [DataRow(-2, 5)]
        [DataRow(10, -1)]
        public void TestInvalidExplorationArea2Coordinates(int x, int y)
        {
            Assert.ThrowsException<ArgumentException>(() => new ExplorationArea(new Position(x, y)));
        }

        [TestMethod]
        [DataRow(0, 0, -2, 2)]
        [DataRow(5, 6, 3, 4)]
        public void TestInvalidExplorationArea4Coordinates(int x0, int y0, int x1, int y1)
        {
            Assert.ThrowsException<ArgumentException>(() => new ExplorationArea(new Position(x0, y0), new Position(x1, y1)));
        }

        [TestMethod]
        [DataRow("1 1")]
        [DataRow("2 5")]
        [DataRow("6 8")]
        [DataRow("0 0 2 2")]
        [DataRow("1 2 3 4")]
        public void TestValidExplorationAreaCoordinatesFromString(string coordinates)
        {
            _ = new ExplorationArea(coordinates);
            Assert.IsTrue(true); // exception was not thrown during instantiation 
        }

        [TestMethod]
        [DataRow("1")]
        [DataRow("1 N")]
        [DataRow("1, 2")]
        public void TestInvalidExplorationAreaCoordinatesFromString(string coordinates)
        {
            Assert.ThrowsException<ArgumentException>(() => new ExplorationArea(coordinates));
        }

    }
}