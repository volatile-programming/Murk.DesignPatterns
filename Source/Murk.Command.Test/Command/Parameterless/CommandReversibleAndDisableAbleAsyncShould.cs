﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Murk.DesignPatterns.Command.Parameterless;

namespace Murk.Command.Test.Command.Parameterless
{
    [TestClass]
    public class CommandReversibleAndDisableAbleAsyncShould
    {
        #region Attributes
        private CommandReversibleAndDisableAbleAsync _sut;
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
            _sut = new CommandReversibleAndDisableAbleAsync(
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
                () => new CommandReversibleAndDisableAbleAsync(
                    canExecuteAction: null,
                    _actionToExecute,
                    _undoAction));

            Assert.ThrowsException<ArgumentNullException>(
                () => new CommandReversibleAndDisableAbleAsync(
                    _canExecuteAction,
                    actionToExecute: null,
                    _undoAction));

            Assert.ThrowsException<ArgumentNullException>(
                () => new CommandReversibleAndDisableAbleAsync(
                    _canExecuteAction,
                    _actionToExecute,
                    undoAction: null));
        }

        [TestMethod]
        public void ConstructHimself()
        {
            _ = new CommandReversibleAndDisableAbleAsync(
                _canExecuteAction,
                _actionToExecute,
                _undoAction);
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
        public void CanExecute_NullParameter()
        {
            var actualResult = _sut.CanExecute(null);

            Assert.AreEqual(expected: true, actualResult);
        }

        [TestMethod]
        public async Task CannotExecute()
        {
            int expectedCount = _actualCount;
            var sut = new CommandReversibleAndDisableAbleAsync(
                () => false,
                _actionToExecute,
                _undoAction);

            await sut.ExecuteAsync();
            Assert.AreEqual(expectedCount, _actualCount);

            await sut.ReverseAsync();
            Assert.AreEqual(expectedCount, _actualCount);

            bool actualResult = await sut.CanExecuteAsync();
            Assert.AreEqual(expected: false, actualResult);
        }

        [TestMethod]
        public async Task Execute()
        {
            var expectedCount = _actualCount;

            await _sut.ExecuteAsync();

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

            await _sut.ExecuteAsync();
            Assert.AreEqual(originalCount, _actualCount);

            _sut.Execute(null);
            Assert.AreEqual(originalCount, _actualCount);

            await _sut.ReverseAsync();
            Assert.AreEqual(originalCount, _actualCount);

            bool actualResult = await _sut.CanExecuteAsync();
            Assert.AreEqual(expected: false, actualResult);

            actualResult = _sut.CanExecute(null);
            Assert.AreEqual(expected: false, actualResult);
        }
        #endregion
    }
}
