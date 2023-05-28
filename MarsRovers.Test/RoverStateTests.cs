using MarsRovers.Domain.State;

namespace MarsRovers.Test
{
    [TestClass]
    public class RoverStateTests
    {
        [TestMethod]
        [DataRow("1 1 N")]
        [DataRow("2 5 E")]
        [DataRow("6 8 S")]
        [DataRow("0 0 N")]
        [DataRow("1 2 W")]
        public void TestValidRoverStateFromString(string state)
        {
            _ = new RoverState(state);
            Assert.IsTrue(true); // exception was not thrown during instantiation 
        }

        [TestMethod]
        [DataRow("1 1")]
        [DataRow("1 N")]
        [DataRow("1 2 R")]
        public void TestInvalidRoverStateFromString(string state)
        {
            Assert.ThrowsException<ArgumentException>(() => new RoverState(state));
        }

    }
}