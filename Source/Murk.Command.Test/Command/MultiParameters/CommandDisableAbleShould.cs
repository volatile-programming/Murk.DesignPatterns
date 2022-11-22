using Microsoft.VisualStudio.TestTools.UnitTesting;
using Murk.DesignPatterns.Command.MultiParameters;
using System;

namespace Murk.Command.Test.Command.MultiParameters
{
    [TestClass]
    public class CommandDisableAbleShould
    {
        #region Attributes
        private int _actualCount;
        private CommandDisableAble _sut;
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
            var actualResult = _sut.CanExecute(_parameters);

            Assert.AreEqual(expected: true, actualResult);
        }

        [TestMethod]
        public void CanExecute_ObjectParameter()
        {
            var actualResult = _sut.CanExecute(_parameters as object);

            Assert.AreEqual(expected: true, actualResult);
        }

        [TestMethod]
        public void CanExecute_GuardsAgainstNull()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => _sut.CanExecute(null));

            object a = null;
            Assert.ThrowsException<ArgumentNullException>(
                () => _sut.CanExecute(a));
        }

        [TestMethod]
        public void CanExecute_GuardsAgainstEmpty()
        {
            Assert.ThrowsException<ArgumentException>(
                () => _sut.CanExecute(new object[] { }));

            Assert.ThrowsException<ArgumentException>(
                () => _sut.CanExecute(new object[] { } as object));
        }

        [TestMethod]
        public void CannotExecute()
        {
            int expectedCount = _actualCount;
            var sut = new CommandDisableAble(
                o => false, _actionToExecute);

            bool actualResult = sut.CanExecute(_parameters);
            Assert.AreEqual(expected: false, actualResult);

            sut.Execute(_parameters);
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
        public void Execute_ObjectParameter()
        {
            var expectedCount = _actualCount;

            _sut.Execute(_parameters as object);

            Assert.AreNotEqual(expectedCount, _actualCount);
            Assert.IsTrue(expectedCount < _actualCount);
        }

        [TestMethod]
        public void Execute_GuardsAgainstNull()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => _sut.Execute(null));

            object a = null;
            Assert.ThrowsException<ArgumentNullException>(
                () => _sut.Execute(a));
        }

        [TestMethod]
        public void Execute_GuardsAgainstEmpty()
        {
            Assert.ThrowsException<ArgumentException>(
                () => _sut.Execute(new object[] { }));

            Assert.ThrowsException<ArgumentException>(
                () => _sut.Execute(new object[] { } as object));
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

            _sut.Execute(_parameters as object);
            Assert.AreEqual(originalCount, _actualCount);

            bool actualResult = _sut.CanExecute(_parameters);
            Assert.AreEqual(expected: false, actualResult);

            actualResult = _sut.CanExecute(_parameters as object);
            Assert.AreEqual(expected: false, actualResult);
        }
        #endregion
    }
}
