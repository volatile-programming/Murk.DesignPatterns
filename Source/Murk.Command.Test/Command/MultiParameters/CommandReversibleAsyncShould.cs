using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Murk.DesignPatterns.Command.MultiParameters;

namespace Murk.Command.Test.Command.MultiParameters
{
    [TestClass]
    public class CommandReversibleAsyncShould
    {
        #region Attributes
        private CommandReversibleAsync _sut;
        private int _actualCount;
        private Action<object[]> _actionToExecute;
        private Action<object[]> _undoAction;
        private readonly object[] _parameters = { 1, 2f, 3d, '¡', "!" };
        #endregion

        [TestInitialize]
        public void TestInitialize()
        {
            _actualCount = 0;
            _actionToExecute = o => _actualCount++;
            _undoAction = o => _actualCount--;
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

            await _sut.ExecuteAsync(_parameters);

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
        public void Execute_GuardsAgainstEmpty()
        {
            Assert.ThrowsExceptionAsync<ArgumentException>(
                () => _sut.ExecuteAsync(new object[] { }));
        }

        [TestMethod]
        public async Task Reverse()
        {
            var originalCount = _actualCount;

            await _sut.ExecuteAsync(_parameters);
            Assert.AreNotEqual(originalCount, _actualCount);

            await _sut.ReverseAsync(_parameters);
            Assert.AreEqual(originalCount, _actualCount);
        }

        [TestMethod]
        public void Reverse_GuardsAgainstNull()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => _sut.ReverseAsync(null));
        }

        [TestMethod]
        public void Reverse_GuardsAgainstEmpty()
        {
            Assert.ThrowsExceptionAsync<ArgumentException>(
                () => _sut.ReverseAsync(new object[] { }));
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

            await _sut.ExecuteAsync(_parameters);
            Assert.AreEqual(originalCount, _actualCount);

            await _sut.ReverseAsync(_parameters);
            Assert.AreEqual(originalCount, _actualCount);
        }
        #endregion
    }
}
