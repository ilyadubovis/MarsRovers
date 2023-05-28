using MarsRovers.Domain.Models;
using Action = MarsRovers.Domain.Models.Action;

namespace MarsRovers.Test
{
    [TestClass]
    public class ActionTests
    {
        [TestMethod]
        [DataRow('L', ActionType.Left)]
        [DataRow('l', ActionType.Left)]
        [DataRow('R', ActionType.Right)]
        [DataRow('r', ActionType.Right)]
        [DataRow('M', ActionType.Move)]
        [DataRow('m', ActionType.Move)]
        public void TestValidActions(char abbreviation, ActionType expectedActionType)
        {
            Assert.IsTrue(new Action(abbreviation).Type == expectedActionType);
        }

        [TestMethod]
        [DataRow(' ')]
        [DataRow('A')]
        [DataRow('b')]
        [DataRow('1')]
        public void TestInvalidActions(char abbreviation)
        {
            Assert.ThrowsException<ArgumentException>(() => new Action(abbreviation));
        }
    }
}