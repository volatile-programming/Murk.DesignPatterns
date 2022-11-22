using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Murk.DesignPatterns.Command.Parameterless;

namespace Murk.Command.Test.Command.Parameterless
{
    [TestClass]
    public class CommandReversibleShould
    {
        #region Attributes
        private CommandReversible _sut;
        private int _actualCount;
        private Action _actionToExecute;
        private Action _undoAction;
        #endregion

        [TestInitialize]
        public void TestInitialize()
        {
            _actualCount = 0;
            _actionToExecute = () => _actualCount++;
            _undoAction = () => _actualCount--;
            _sut = new CommandReversible(_actionToExecute, _undoAction);
        }

        #region Constructor
        [TestMethod]
        public void Constructor_GuardsAgainstNull()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => new CommandReversible(null, _undoAction));

            Assert.ThrowsException<ArgumentNullException>(
                () => new CommandReversible(_actionToExecute, null));
        }

        [TestMethod]
        public void ConstructHimself()
        {
            _ = new CommandReversible(_actionToExecute, _undoAction);
        }
        #endregion

        #region Execute
        [TestMethod]
        public void Execute()
        {
            var expectedCount = _actualCount;

            _sut.Execute();

            Assert.AreNotEqual(expectedCount, _actualCount);
            Assert.IsTrue(expectedCount < _actualCount);
        }

        [TestMethod]
        public void Reverse()
        {
            var originalCount = _actualCount;

            _sut.Execute();
            Assert.AreNotEqual(originalCount, _actualCount);

            _sut.Reverse();
            Assert.AreEqual(originalCount, _actualCount);
        }
        #endregion

        #region Dispose
        [TestMethod]
        public void Dispose()
        {
            _sut.Dispose();
        }

        [TestMethod]
        public void Dispose_CannotExecute()
        {
            int originalCount = _actualCount;

            _sut.Dispose();

            _sut.Execute();
            Assert.AreEqual(originalCount, _actualCount);

            _sut.Reverse();
            Assert.AreEqual(originalCount, _actualCount);
        }
        #endregion
    }
}
