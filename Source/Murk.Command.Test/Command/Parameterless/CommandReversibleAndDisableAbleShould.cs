using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Murk.DesignPatterns.Command;

namespace Murk.Command.Test.Command.Parameterless
{
    [TestClass]
    public class CommandReversibleAndDisableAbleShould
    {
        #region Attributes
        private CommandReversibleAndDisableAble _sut;
        private int _actualCount;
        private Func<bool> _canExecuteAction;
        private Action _actionToExecute;
        private Action _undoAction;
        #endregion

        [TestInitialize]
        public void TestInitialize()
        {
            _actualCount = 0;
            _canExecuteAction = () => true;
            _actionToExecute = () => _actualCount++;
            _undoAction = () => _actualCount--;
            _sut = new CommandReversibleAndDisableAble(
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
                () => new CommandReversibleAndDisableAble(
                    canExecuteAction: null,
                    _actionToExecute,
                    _undoAction));

            Assert.ThrowsException<ArgumentNullException>(
                () => new CommandReversibleAndDisableAble(
                    _canExecuteAction,
                    actionToExecute: null,
                    _undoAction));

            Assert.ThrowsException<ArgumentNullException>(
                () => new CommandReversibleAndDisableAble(
                    _canExecuteAction,
                    _actionToExecute,
                    undoAction: null));
        }

        [TestMethod]
        public void ConstructHimself()
        {
            _ = new CommandReversibleAndDisableAble(
                _canExecuteAction, _actionToExecute, _undoAction);
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
        public void CannotExecute()
        {
            int expectedCount = _actualCount;
            var sut = new CommandReversibleAndDisableAble(
                () => false, _actionToExecute, _undoAction);

            sut.Execute();
            Assert.AreEqual(expectedCount, _actualCount);

            sut.Reverse();
            Assert.AreEqual(expectedCount, _actualCount);

            bool actualResult = sut.CanExecute();
            Assert.AreEqual(expected: false, actualResult);
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
        public void Reverse()
        {
            var originalCount = _actualCount;

            _sut.Execute();
            Assert.AreNotEqual(originalCount, _actualCount);

            _sut.Reverse();
            Assert.AreEqual(originalCount, _actualCount);
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

            _sut.Reverse();
            Assert.AreEqual(originalCount, _actualCount);

            bool actualResult = _sut.CanExecute();
            Assert.AreEqual(expected: false, actualResult);
        }

        #endregion
    }
}
