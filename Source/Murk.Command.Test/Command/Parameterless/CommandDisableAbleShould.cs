using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Murk.DesignPatterns.Command.Parameterless;

namespace Murk.Command.Test.Command.Parameterless
{
    [TestClass]
    public class CommandDisableAbleShould
    {
        #region Attributes
        private int _actualCount;
        private CommandDisableAble _sut;
        private Func<bool> _canExecuteAction;
        private Action _actionToExecute;
        #endregion

        [TestInitialize]
        public void TestInitialize()
        {
            _actualCount = 0;
            _canExecuteAction = () => true;
            _actionToExecute = () => _actualCount++;
            _sut = new CommandDisableAble(
                _canExecuteAction,
                _actionToExecute);
            _sut.CanExecuteChanged += (o, e) => _actualCount++;
        }

        #region Constructor
        [TestMethod]
        public void Constructor_GuardsAgainstNull()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => new CommandDisableAble(null, _actionToExecute));

            Assert.ThrowsException<ArgumentNullException>(
                () => new CommandDisableAble(_canExecuteAction, null));
        }

        [TestMethod]
        public void ConstructHimself()
        {
            _ = new CommandDisableAble(
                _canExecuteAction,
                _actionToExecute);
        }
        #endregion

        #region Execute
        [TestMethod]
        public void CanExecute()
        {
            var actualResult = _sut.CanExecute();

            Assert.AreEqual(expected: true, actualResult);
        }

        [TestMethod]
        public void CanExecute_NullParameter()
        {
            var actualResult = _sut.CanExecute(null);

            Assert.AreEqual(expected: true, actualResult);
        }

        [TestMethod]
        public void CannotExecute()
        {
            int expectedCount = _actualCount;
            var sut = new CommandDisableAble(
                () => false, _actionToExecute);

            sut.Execute();
            bool actualResult = sut.CanExecute();

            Assert.AreEqual(expected: false, actualResult);
            Assert.AreEqual(expectedCount, _actualCount);
        }

        [TestMethod]
        public void Execute()
        {
            var expectedCount = _actualCount;

            _sut.Execute();

            Assert.AreNotEqual(expectedCount, _actualCount);
            Assert.IsTrue(expectedCount < _actualCount);
        }

        [TestMethod]
        public void Execute_NullParameter()
        {
            var expectedCount = _actualCount;

            _sut.Execute(null);

            Assert.AreNotEqual(expectedCount, _actualCount);
            Assert.IsTrue(expectedCount < _actualCount);
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
        public void Dispose_CannotExecute()
        {
            int originalCount = _actualCount;

            _sut.Dispose();

            _sut.Execute();
            Assert.AreEqual(originalCount, _actualCount);

            _sut.Execute(null);
            Assert.AreEqual(originalCount, _actualCount);

            bool actualResult = _sut.CanExecute();
            Assert.AreEqual(expected: false, actualResult);

            actualResult = _sut.CanExecute(null);
            Assert.AreEqual(expected: false, actualResult);
        }
        #endregion
    }
}
