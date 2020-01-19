using Microsoft.VisualStudio.TestTools.UnitTesting;
using Murk.Command;
using System;
using System.Threading.Tasks;

namespace Murk.Test.Command
{
	[TestClass]
	public class CommandDisableAbleAsyncShould
	{
		#region Attributes
		private int _actualCount;
		private CommandDisableAbleAsync _sut;
		private Func<object, bool> _canExecuteAction;
		private Action<object> _actionToExecute;
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
				() => new CommandDisableAbleAsync(null,
					_actionToExecute));

			Assert.ThrowsException<ArgumentNullException>(
				() => new CommandDisableAbleAsync(_canExecuteAction,
					null));
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
			var actualResult = await _sut.CanExecuteAsync(1);

			Assert.AreEqual(expected: true, actualResult);
		}

		[TestMethod]
		public void CanExecute_ObjectParameter()
		{
			var actualResult = _sut.CanExecute(1 as object);

			Assert.AreEqual(expected: true, actualResult);
		}

		[TestMethod]
		public void CanExecute_GuardsAgainstNull()
		{
			Assert.ThrowsExceptionAsync<ArgumentNullException>(
				() => _sut.CanExecuteAsync(null));

			Assert.ThrowsException<ArgumentNullException>(
				() => _sut.CanExecute(null));
		}

		[TestMethod]
		public async Task CannotExecute()
		{
			int expectedCount = _actualCount;
			var sut = new CommandDisableAbleAsync(
				o => false, _actionToExecute);

			await sut.ExecuteAsync(1);
			Assert.AreEqual(expectedCount, _actualCount);

			bool actualResult = await sut.CanExecuteAsync(1);
			Assert.AreEqual(expected: false, actualResult);
		}

		[TestMethod]
		public async Task Execute()
		{
			var expectedCount = _actualCount;

			await _sut.ExecuteAsync(1);

			Assert.AreNotEqual(expectedCount, _actualCount);
			Assert.IsTrue(expectedCount < _actualCount);
		}

		[TestMethod]
		public void Execute_ObjectParameter()
		{
			var expectedCount = _actualCount;

			_sut.Execute(1);

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

			await _sut.ExecuteAsync(1);
			Assert.AreEqual(originalCount, _actualCount);

			_sut.Execute(1);
			Assert.AreEqual(originalCount, _actualCount);

			bool actualResult = await _sut.CanExecuteAsync(1);
			Assert.AreEqual(expected: false, actualResult);

			actualResult = _sut.CanExecute(1);
			Assert.AreEqual(expected: false, actualResult);
		}
		#endregion
	}
}
