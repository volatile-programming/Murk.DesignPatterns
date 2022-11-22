using Microsoft.VisualStudio.TestTools.UnitTesting;
using Murk.DesignPatterns.Command;
using System;

namespace Murk.Command.Test.Command
{
    [TestClass]
    public class CommandReversibleAndDisableAbleShould
    {
        #region Attributes
        private CommandReversibleAndDisableAble _sut;
        private int _actualCount;
        private Func<object, bool> _canExecuteAction;
        private Action<object> _actionToExecute;
        private Action<object> _undoAction;
        #endregion

        [TestInitialize]
        public void TestInitialize()
        {
            _actualCount = 0;
            _canExecuteAction = o => true;
            _actionToExecute = o => _actualCount++;
            _undoAction = o => _actualCount--;
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
            var actualResult = _sut.CanExecute(1);

            Assert.AreEqual(expected: true, actualResult);
        }

        [TestMethod]
        public void CanExecute_ObjectParameter()
        {
            var actualResult = _sut.CanExecute(1 as object);

            Assert.AreEqual(expected: true, actualResult);
        }

        [TestMethod]
        public void CannotExecute()
        {
            int expectedCount = _actualCount;
            var sut = new CommandReversibleAndDisableAble(
                o => false, _actionToExecute, _undoAction);

            sut.Execute(1);
            Assert.AreEqual(expectedCount, _actualCount);

            sut.Reverse(2);
            Assert.AreEqual(expectedCount, _actualCount);

            bool actualResult = sut.CanExecute(1);
            Assert.AreEqual(expected: false, actualResult);
        }

        [TestMethod]
        public void CanExecute_GuardsAgainstNull()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => _sut.CanExecute(null));
        }

        [TestMethod]
        public void Execute()
        {
            var expectedCount = _actualCount;

            _sut.Execute(1);

            Assert.AreNotEqual(expectedCount, _actualCount);
            Assert.IsTrue(expectedCount < _actualCount);
        }

        [TestMethod]
        public void Execute_ObjectParameter()
        {
            var expectedCount = _actualCount;

            _sut.Execute(1 as object);

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

            _sut.Execute(1);
            Assert.AreEqual(originalCount, _actualCount);

            _sut.Execute(3 as object);
            Assert.AreEqual(originalCount, _actualCount);

            _sut.Reverse(2);
            Assert.AreEqual(originalCount, _actualCount);

            bool actualResult = _sut.CanExecute(1);
            Assert.AreEqual(expected: false, actualResult);

            actualResult = _sut.CanExecute(1 as object);
            Assert.AreEqual(expected: false, actualResult);
        }
        #endregion
    }
}
