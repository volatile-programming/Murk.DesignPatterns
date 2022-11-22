using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Murk.DesignPatterns.Command.MultiParameter;

namespace Murk.Command.Test.Command.MultiParameters
{
    [TestClass]
    public class CommandReversibleGenericShould
    {
        #region Attributes
        private CommandReversible<int> _sut;
        private int _actualCount;
        private Action<int[]> _actionToExecute;
        private Action<int[]> _undoAction;
        private readonly int[] _parameters = { 1, 2, 3 };
        #endregion

        [TestInitialize]
        public void TestInitialize()
        {
            _actualCount = 0;
            _actionToExecute = o => _actualCount++;
            _undoAction = o => _actualCount--;
            _sut = new CommandReversible<int>(
                _actionToExecute,
                _undoAction);
        }

        #region Constructor
        [TestMethod]
        public void Constructor_GuardsAgainstNull()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => new CommandReversible<int>(null, _undoAction));

            Assert.ThrowsException<ArgumentNullException>(
                () => new CommandReversible<int>(_actionToExecute, null));
        }

        [TestMethod]
        public void ConstructHimself()
        {
            _ = new CommandReversible<int>(_actionToExecute, _undoAction);
        }
        #endregion

        #region Execute
        [TestMethod]
        public void Execute()
        {
            var expectedCount = _actualCount;

            _sut.Execute(_parameters);

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
        public void Execute_GuardsAgainstEmpty()
        {
            Assert.ThrowsException<ArgumentException>(
                () => _sut.Execute(new int[] { }));
        }

        [TestMethod]
        public void Reverse()
        {
            var originalCount = _actualCount;

            _sut.Execute(_parameters);
            Assert.AreNotEqual(originalCount, _actualCount);

            _sut.Reverse(_parameters);
            Assert.AreEqual(originalCount, _actualCount);
        }

        [TestMethod]
        public void Reverse_GuardsAgainstNull()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => _sut.Reverse(null));
        }

        [TestMethod]
        public void Reverse_GuardsAgainstEmpty()
        {
            Assert.ThrowsException<ArgumentException>(
                () => _sut.Reverse(new int[] { }));
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

            _sut.Execute(_parameters);
            Assert.AreEqual(originalCount, _actualCount);

            _sut.Reverse(_parameters);
            Assert.AreEqual(originalCount, _actualCount);
        }
        #endregion
    }
}
