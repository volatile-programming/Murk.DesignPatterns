using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Murk.DesignPatterns.Command;

namespace Murk.Command.Test.Command.Parameterless
{
    [TestClass]
    public class CommandDisableAbleAsyncShould
    {
        #region Attributes
        private int _actualCount;
        private CommandDisableAbleAsync _sut;
        private Func<bool> _canExecuteAction;
        private Action _actionToExecute;
        #endregion

        [TestInitialize]
        public void TestInitialize()
        {
            _actualCount = 0;
            _canExecuteAction = () => true;
            _actionToExecute = () => _actualCount++;
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
        public async Task CanExecute()
        {
            var actualResult = await _sut.CanExecuteAsync();

            Assert.AreEqual(expected: true, actualResult);
        }

        [TestMethod]
        public async Task CannotExecute()
        {
            int expectedCount = _actualCount;
            var sut = new CommandDisableAbleAsync(
                () => false, _actionToExecute);

            await sut.ExecuteAsync();
            bool actualResult = await sut.CanExecuteAsync();

            Assert.AreEqual(expected: false, actualResult);
            Assert.AreEqual(expectedCount, _actualCount);
        }

        [TestMethod]
        public async Task Execute()
        {
            var expectedCount = _actualCount;

            await _sut.ExecuteAsync();

            Assert.AreNotEqual(expectedCount, _actualCount);
            Assert.IsTrue(expectedCount < _actualCount);
        }

        #endregion

        #region Can Execute Changed And Dispose

        [TestMethod]
        public async Task RiseCanExecuteChangedAsync()
        {
            int originalCount = _actualCount;

            await _sut.RiseCanExecuteChangedAsync();

            Assert.AreNotEqual(originalCount, _actualCount);
            Assert.IsTrue(originalCount < _actualCount);
        }

        [TestMethod]
        public async Task Dispose()
        {
            int originalCount = _actualCount;

            _sut.Dispose();
            await _sut.RiseCanExecuteChangedAsync();

            Assert.AreEqual(originalCount, _actualCount);
        }

        [TestMethod]
        public async Task Dispose_CannotExecute()
        {
            int originalCount = _actualCount;

            _sut.Dispose();

            await _sut.ExecuteAsync();
            Assert.AreEqual(originalCount, _actualCount);

            bool actualResult = await _sut.CanExecuteAsync();
            Assert.AreEqual(expected: false, actualResult);
        }

        #endregion
    }
}
