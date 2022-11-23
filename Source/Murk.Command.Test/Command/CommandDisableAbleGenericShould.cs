using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Murk.DesignPatterns.Command;

namespace Murk.Command.Test.Command
{
    [TestClass]
    public class CommandDisableAbleGenericShould
    {
        #region Attributes
        private int _actualCount;
        private CommandDisableAble<int> _sut;
        private Func<int, bool> _canExecuteAction;
        private Action<int> _actionToExecute;
        #endregion

        [TestInitialize]
        public void TestInitialize()
        {
            _actualCount = 0;
            _canExecuteAction = o => true;
            _actionToExecute = increase => _actualCount += increase;
            _sut = new CommandDisableAble<int>(
                _canExecuteAction,
                _actionToExecute);
            _sut.CanExecuteChanged += (o, e) => _actualCount++;
        }

        #region Constructor

        [TestMethod]
        public void Constructor_GuardsAgainstNull()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => new CommandDisableAble<int>(null, _actionToExecute));

            Assert.ThrowsException<ArgumentNullException>(
                () => new CommandDisableAble<int>(_canExecuteAction, null));
        }

        [TestMethod]
        public void ConstructHimself()
        {
            _ = new CommandDisableAble<int>(
                _canExecuteAction,
                _actionToExecute);
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
        public void CannotExecute()
        {
            int expectedCount = _actualCount;
            var sut = new CommandDisableAble<int>(
                o => false, _actionToExecute);

            sut.Execute(1);
            bool actualResult = sut.CanExecute(1);

            Assert.AreEqual(expected: false, actualResult);
            Assert.AreEqual(expectedCount, _actualCount);
        }

        [TestMethod]
        public void Execute()
        {
            var expectedCount = _actualCount;

            _sut.Execute(1);

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

            _sut.Execute(1);
            Assert.AreEqual(originalCount, _actualCount);

            bool actualResult = _sut.CanExecute(1);
            Assert.AreEqual(expected: false, actualResult);
        }

        #endregion
    }
}
