using Microsoft.VisualStudio.TestTools.UnitTesting;
using Murk.DesignPatterns.Command;
using System;

namespace Murk.Command.Test.Command
{
    [TestClass]
    public class CommandReversibleGenericShould
    {
        #region Attributes
        private CommandReversible<int?> _sut;
        private int _actualCount;
        private Action<int?> _actionToExecute;
        private Action<int?> _undoAction;
        #endregion

        [TestInitialize]
        public void TestInitialize()
        {
            _actualCount = 0;
            _actionToExecute = increase => _actualCount += increase.Value;
            _undoAction = decrease => _actualCount -= decrease.Value;
            _sut = new CommandReversible<int?>(
                _actionToExecute,
                _undoAction);
        }

        #region Constructor
        [TestMethod]
        public void Constructor_GuardsAgainstNull()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => new CommandReversible<int?>(null, _undoAction));

            Assert.ThrowsException<ArgumentNullException>(
                () => new CommandReversible<int?>(_actionToExecute, null));
        }

        [TestMethod]
        public void ConstructHimself()
        {
            _ = new CommandReversible<int?>(
                _actionToExecute,
                _undoAction);
        }
        #endregion

        #region Execute
        [TestMethod]
        public void Execute()
        {
            var expectedCount = _actualCount;

            _sut.Execute(1);

            Assert.AreNotEqual(expectedCount, _actualCount);
            Assert.IsTrue(expectedCount < _actualCount);
        }

        [TestMethod]
        public void Execute_GuardsAgainstNull()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => _sut.Execute(null));
        }

        [TestMethod]
        public void Reverse()
        {
            var originalCount = _actualCount;

            _sut.Execute(1);
            Assert.AreNotEqual(originalCount, _actualCount);

            _sut.Reverse(1);
            Assert.AreEqual(originalCount, _actualCount);
        }

        [TestMethod]
        public void Reverse_GuardsAgainstNull()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => _sut.Reverse(null));
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
            _sut.Execute(1);
            _sut.Reverse(2);

            Assert.AreEqual(originalCount, _actualCount);
        }
        #endregion
    }
}
