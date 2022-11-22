using Microsoft.VisualStudio.TestTools.UnitTesting;
using Murk.DesignPatterns.Command.MultiParameter;
using System;
using System.Threading.Tasks;

namespace Murk.Command.Test.Command.MultiParameters
{
    [TestClass]
    public class CommandDisableAbleAsyncShould
    {
        #region Attributes
        private int _actualCount;
        private CommandDisableAbleAsync _sut;
        private Func<object[], bool> _canExecuteAction;
        private Action<object[]> _actionToExecute;
        private readonly object[] _parameters = { 1, 2f, 3d, '¡', "!" };
        #endregion

        [TestInitialize]
        public void TestInitialize()
        {
            _actualCount = 0;
            _canExecuteAction = o => true;
            _actionToExecute = o => _actualCount++;
            _sut = new CommandDisableAbleAsync(
                _canExecuteAction,
                _actionToExecute);
            _sut.CanExecuteChanged += (o, e) => _actualCount++;
        }

        #region Constructor
        [TestMethod]
        public void Constructor_GuardsAgainstNull()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => new CommandDisableAbleAsync(null, _actionToExecute));

            Assert.ThrowsException<ArgumentNullException>(
                () => new CommandDisableAbleAsync(_canExecuteAction, null));
        }

        [TestMethod]
        public void ConstructHimself()
        {
            _ = new CommandDisableAbleAsync(
                _canExecuteAction,
                _actionToExecute);
        }
        #endregion

        #region Execute
        [TestMethod]
        public void CanExecute()
        {
            var actualResult = _sut.CanExecute(_parameters);

            Assert.AreEqual(expected: true, actualResult);
        }

        [TestMethod]
        public async Task CanExecuteAsync()
        {
            var actualResult = await _sut.CanExecuteAsync(_parameters);

            Assert.AreEqual(expected: true, actualResult);
        }

        [TestMethod]
        public void CanExecute_GuardsAgainstNull()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => _sut.CanExecuteAsync(null));

            Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => _sut.CanExecuteAsync(null));
        }

        [TestMethod]
        public void CanExecute_GuardsAgainstEmpty()
        {
            Assert.ThrowsExceptionAsync<ArgumentException>(
                () => _sut.CanExecuteAsync(new object[] { }));

            Assert.ThrowsException<ArgumentException>(
                () => _sut.CanExecute(new int[] { }));
        }

        [TestMethod]
        public async Task CannotExecute()
        {
            int expectedCount = _actualCount;
            var sut = new CommandDisableAbleAsync(
                o => false, _actionToExecute);

            bool actualResult = await sut.CanExecuteAsync(_parameters);
            Assert.AreEqual(expected: false, actualResult);

            await sut.ExecuteAsync(_parameters);
            Assert.AreEqual(expectedCount, _actualCount);

        }

        [TestMethod]
        public void Execute()
        {
            var expectedCount = _actualCount;

            _sut.Execute(_parameters);

            Assert.AreNotEqual(expectedCount, _actualCount);
            Assert.IsTrue(expectedCount < _actualCount);
        }

        [TestMethod]
        public async Task ExecuteAsync()
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

            Assert.ThrowsException<ArgumentNullException>(
                () => _sut.Execute(null));
        }

        [TestMethod]
        public void Execute_GuardsAgainstEmpty()
        {
            Assert.ThrowsExceptionAsync<ArgumentException>(
                () => _sut.ExecuteAsync(new object[] { }));

            Assert.ThrowsException<ArgumentException>(
                () => _sut.Execute(new int[] { }));
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

            _sut.Execute(_parameters);
            Assert.AreEqual(originalCount, _actualCount);

            bool actualResult = await _sut.CanExecuteAsync(_parameters);
            Assert.AreEqual(expected: false, actualResult);

            actualResult = _sut.CanExecute(_parameters);
            Assert.AreEqual(expected: false, actualResult);
        }
        #endregion
    }
}
