using MarsRovers.Domain.Models;
using MarsRovers.Domain.State;

namespace MarsRovers.Test
{
    [TestClass]
    public class RoverTests
    {
        [TestMethod]
        [DataRow("5 5", "1 2 N", "LMLMLMLMM", "1 3 N")]
        [DataRow("5 5", "3 3 E", "MMRMMRMRRM", "5 1 E")]
        [DataRow("3 3", "0 0 S", "LMMLM", "2 1 N")]
        [DataRow("3 3", "1 2 W", "LMLMRM", "2 0 S")]
        public void TestRoverActions(string explorationAreaString, string initialStateString, string actionChainString, string expectedFinalState)
        {
            var explorationArea = new ExplorationArea(explorationAreaString);
            var initialState = new RoverState(initialStateString);

            var rover = Rover.CreateRover(explorationArea, initialState);
            rover.ExecuteActionChain(new ActionChain(actionChainString));

            var finalState = rover.State.ToString();
            Assert.IsTrue(finalState == expectedFinalState);
        }
    }
}