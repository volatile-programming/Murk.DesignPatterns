using Microsoft.VisualStudio.TestTools.UnitTesting;
using Murk.DesignPatterns.Command.MultiParameter;
using System;
using System.Threading.Tasks;

namespace Murk.Command.Test.Command.MultiParameters
{
    [TestClass]
    public class CommandAsyncShould
    {
        #region Attributes
        private int _actualCount;
        private CommandAsync _sut;
        private Action<object[]> _actionToExecute;
        private readonly object[] _parameters = { 1, 2f, 3d, '¡', "!" };
        #endregion

        [TestInitialize]
        public void TestInitialize()
        {
            _actualCount = 0;
            _actionToExecute = o => _actualCount++;
            _sut = new CommandAsync(_actionToExecute);
        }

        #region Constructor
        [TestMethod]
        public void Constructor_GuardsAgainstNull()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => new CommandAsync(null));
        }

        [TestMethod]
        public void ConstructHimself()
        {
            _ = new CommandAsync(_actionToExecute);
        }
        #endregion

        #region Execute
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
                () => _sut.ExecuteAsync(new object[] { }));
        }
        #endregion

        #region Dispose
        [TestMethod]
        public void Dispose()
        {
            _sut.Dispose();
        }

        [TestMethod]
        public async Task Dispose_CannotExecute()
        {
            int originalCount = _actualCount;

            _sut.Dispose();

            await _sut.ExecuteAsync(_parameters);
            Assert.AreEqual(originalCount, _actualCount);
        }
        #endregion
    }
}
