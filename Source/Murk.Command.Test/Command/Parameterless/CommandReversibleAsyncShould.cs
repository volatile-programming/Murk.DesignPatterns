using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Murk.DesignPatterns.Command;

namespace Murk.Command.Test.Command.Parameterless
{
    [TestClass]
    public class CommandReversibleAsyncShould
    {
        #region Attributes
        private CommandReversibleAsync _sut;
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
            _sut = new CommandReversibleAsync(
                _actionToExecute,
                _undoAction);
        }

        #region Constructor
        [TestMethod]
        public void Constructor_GuardsAgainstNull()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => new CommandReversibleAsync(null, _undoAction));

            Assert.ThrowsException<ArgumentNullException>(
                () => new CommandReversibleAsync(_actionToExecute, null));
        }

        [TestMethod]
        public void ConstructHimself()
        {
            _ = new CommandReversibleAsync(_actionToExecute, _undoAction);
        }
        #endregion

        #region Execute

        [TestMethod]
        public async Task Execute()
        {
            var expectedCount = _actualCount;

            await _sut.ExecuteAsync();

            Assert.AreNotEqual(expectedCount, _actualCount);
            Assert.IsTrue(expectedCount < _actualCount);
        }

        [TestMethod]
        public async Task Reverse()
        {
            var originalCount = _actualCount;

            await _sut.ExecuteAsync();
            Assert.AreNotEqual(originalCount, _actualCount);

            await _sut.ReverseAsync();
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
        public async Task Dispose_CannotExecute()
        {
            int originalCount = _actualCount;

            _sut.Dispose();

            await _sut.ExecuteAsync();
            Assert.AreEqual(originalCount, _actualCount);

            await _sut.ReverseAsync();
            Assert.AreEqual(originalCount, _actualCount);
        }

        #endregion
    }
}
