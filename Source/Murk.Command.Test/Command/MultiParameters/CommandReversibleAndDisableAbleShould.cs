﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Murk.DesignPatterns.Command.MultiParameter;

namespace Murk.Command.Test.Command.MultiParameters
{
    [TestClass]
    public class CommandReversibleAndDisableAbleShould
    {
        #region Attributes
        private CommandReversibleAndDisableAble _sut;
        private int _actualCount;
        private Func<object[], bool> _canExecuteAction;
        private Action<object[]> _actionToExecute;
        private Action<object[]> _undoAction;
        private readonly object[] _parameters = { 1, 2f, 3d, '¡', "!" };
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
            var actualResult = _sut.CanExecute(_parameters);

            Assert.AreEqual(expected: true, actualResult);
        }

        [TestMethod]
        public void CanExecute_GuardsAgainstNull()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => _sut.CanExecute(null));
        }

        [TestMethod]
        public void CanExecute_GuardsAgainstEmpty()
        {
            Assert.ThrowsException<ArgumentException>(
                () => _sut.CanExecute(new object[] { }));
        }

        [TestMethod]
        public void CannotExecute()
        {
            int expectedCount = _actualCount;
            var sut = new CommandReversibleAndDisableAble(
                o => false,
                _actionToExecute,
                _undoAction);

            sut.Execute(_parameters);
            Assert.AreEqual(expectedCount, _actualCount);

            sut.Reverse(_parameters);
            Assert.AreEqual(expectedCount, _actualCount);

            bool actualResult = sut.CanExecute(_parameters);
            Assert.AreEqual(expected: false, actualResult);
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
        public void Execute_GuardsAgainstNull()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => _sut.Execute(null));
        }

        [TestMethod]
        public void Execute_GuardsAgainstEmpty()
        {
            Assert.ThrowsException<ArgumentException>(
                () => _sut.Execute(new object[] { }));
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
                () => _sut.Reverse(new object[] { }));
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

            _sut.Execute(_parameters);
            Assert.AreEqual(originalCount, _actualCount);

            _sut.Reverse(_parameters);
            Assert.AreEqual(originalCount, _actualCount);

            bool actualResult = _sut.CanExecute(_parameters);
            Assert.AreEqual(expected: false, actualResult);
        }
        #endregion
    }
}
