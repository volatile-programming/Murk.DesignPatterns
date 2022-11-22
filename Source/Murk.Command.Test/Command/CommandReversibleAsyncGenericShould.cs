using Microsoft.VisualStudio.TestTools.UnitTesting;
using Murk.DesignPatterns.Command;
using System;
using System.Threading.Tasks;

namespace Murk.Command.Test.Command
{
    [TestClass]
    public class CommandReversibleAsyncGenericShould
    {
        #region Attributes
        private CommandReversibleAsync<int?> _sut;
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
            _sut = new CommandReversibleAsync<int?>(
                _actionToExecute,
                _undoAction);
        }

        #region Constructor
        [TestMethod]
        public void Constructor_GuardsAgainstNull()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => new CommandReversibleAsync<int?>(null, _undoAction));

            Assert.ThrowsException<ArgumentNullException>(
                () => new CommandReversibleAsync<int?>(_actionToExecute,
                    null));
        }

        [TestMethod]
        public void ConstructHimself()
        {
            _ = new CommandReversibleAsync<int?>(_actionToExecute,
                _undoAction);
        }
        #endregion

        #region Execute
        [TestMethod]
        public async Task Execute()
        {
            var expectedCount = _actualCount;

            await _sut.ExecuteAsync(1);

            Assert.AreNotEqual(expectedCount, _actualCount);
            Assert.IsTrue(expectedCount < _actualCount);
        }

        [TestMethod]
        public void Execute_GuardsAgainstNull()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => _sut.ExecuteAsync(null));
        }

        [TestMethod]
        public async Task Reverse()
        {
            var originalCount = _actualCount;

            await _sut.ExecuteAsync(1);
            Assert.AreNotEqual(originalCount, _actualCount);

            await _sut.ReverseAsync(1);
            Assert.AreEqual(originalCount, _actualCount);
        }

        [TestMethod]
        public void Reverse_GuardsAgainstNull()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => _sut.ReverseAsync(null));
        }
        #endregion

        #region Dispose
        [TestMethod]
        public void Dispose()
        {
            _sut.Dispose();
        }

        [TestMethod]
        public async Task Dispose_CannotExecute()
        {
            int originalCount = _actualCount;

            _sut.Dispose();

            await _sut.ExecuteAsync(1);
            Assert.AreEqual(originalCount, _actualCount);

            await _sut.ReverseAsync(2);
            Assert.AreEqual(originalCount, _actualCount);
        }
        #endregion
    }
}
