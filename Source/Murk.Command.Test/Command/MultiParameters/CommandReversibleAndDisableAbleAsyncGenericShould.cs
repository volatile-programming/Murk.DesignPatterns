using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Murk.DesignPatterns.Command.MultiParameter;

namespace Murk.Command.Test.Command.MultiParameters
{
    [TestClass]
    public class CommandReversibleAndDisableAbleAsyncGenericShould
    {
        #region Attributes
        private CommandReversibleAndDisableAbleAsync<int> _sut;
        private int _actualCount;
        private Func<int[], bool> _canExecuteAction;
        private Action<int[]> _actionToExecute;
        private Action<int[]> _undoAction;
        private readonly int[] _parameters = { 1, 2, 3 };
        #endregion

        [TestInitialize]
        public void TestInitialize()
        {
            _actualCount = 0;
            _canExecuteAction = o => true;
            _actionToExecute = o => _actualCount++;
            _undoAction = o => _actualCount--;
            _sut = new CommandReversibleAndDisableAbleAsync<int>(
                _canExecuteAction,
                _actionToExecute,
                _undoAction);
            _sut.CanExecuteChanged += (o, e) => _actualCount++;
        }

        #region Constructor
        [TestMethod]
        public void Constructor_GuardsAgainstNull()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => new CommandReversibleAndDisableAbleAsync<int>(
                    canExecuteAction: null,
                    _actionToExecute,
                    _undoAction));

            Assert.ThrowsException<ArgumentNullException>(
                () => new CommandReversibleAndDisableAbleAsync<int>(
                    _canExecuteAction,
                    actionToExecute: null,
                    _undoAction));

            Assert.ThrowsException<ArgumentNullException>(
                () => new CommandReversibleAndDisableAbleAsync<int>(
                    _canExecuteAction,
                    _actionToExecute,
                    undoAction: null));
        }

        [TestMethod]
        public void ConstructHimself()
        {
            _ = new CommandReversibleAndDisableAbleAsync<int>(
                _canExecuteAction,
                _actionToExecute,
                _undoAction);
        }
        #endregion

        #region Execute
        [TestMethod]
        public async Task CanExecute()
        {
            var actualResult = await _sut.CanExecuteAsync(_parameters);

            Assert.AreEqual(expected: true, actualResult);
        }

        [TestMethod]
        public void CanExecute_GuardsAgainstNull()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => _sut.CanExecuteAsync(null));
        }

        [TestMethod]
        public void CanExecute_GuardsAgainstEmpty()
        {
            Assert.ThrowsExceptionAsync<ArgumentException>(
                () => _sut.CanExecuteAsync(new int[] { }));
        }

        [TestMethod]
        public async Task CannotExecute()
        {
            int expectedCount = _actualCount;
            var sut = new CommandReversibleAndDisableAbleAsync<int>(
                o => false,
                _actionToExecute,
                _undoAction);

            await sut.ExecuteAsync(_parameters);
            Assert.AreEqual(expectedCount, _actualCount);

            await sut.ReverseAsync(_parameters);
            Assert.AreEqual(expectedCount, _actualCount);

            bool actualResult = await sut.CanExecuteAsync(_parameters);
            Assert.AreEqual(expected: false, actualResult);
        }

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
                () => _sut.ExecuteAsync(new int[] { }));
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
                () => _sut.ReverseAsync(new int[] { }));
        }
        #endregion

        #region Can Execute Changed And Dispose
        [TestMethod]
        public void RiseCanExecuteChanged()
        {
            int originalCount = _actualCount;

            _sut.RiseCanExecuteChanged();

            Assert.AreNotEqual(originalCount, _actualCount);
            Assert.IsTrue(originalCount < _actualCount);
        }

        [TestMethod]
        public void Dispose()
        {
            int originalCount = _actualCount;

            _sut.Dispose();
            _sut.RiseCanExecuteChanged();

            Assert.AreEqual(originalCount, _actualCount);
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

            bool actualResult = await _sut.CanExecuteAsync(_parameters);
            Assert.AreEqual(expected: false, actualResult);
        }
        #endregion
    }
}
