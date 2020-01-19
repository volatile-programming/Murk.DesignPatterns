using Microsoft.VisualStudio.TestTools.UnitTesting;
using Murk.Command;
using System;
using System.Threading.Tasks;

namespace Murk.Test.Command
{
	[TestClass]
	public class CommandReversibleAndDisableAbleAsyncGenericShould
	{
		#region Attributes
		private CommandReversibleAndDisableAbleAsync<int?> _sut;
		private int _actualCount;
		private Func<int?, bool> _canExecuteAction;
		private Action<int?> _actionToExecute;
		private Action<int?> _undoAction;
		#endregion

		[TestInitialize]
		public void TestInitialize()
		{
			_actualCount = 0;
			_canExecuteAction = o => true;
			_actionToExecute = increase => _actualCount += increase.Value;
			_undoAction = decrease => _actualCount -= decrease.Value;
			_sut = new CommandReversibleAndDisableAbleAsync<int?>(
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
				() => new CommandReversibleAndDisableAbleAsync<int?>(
					canExecuteAction: null,
					_actionToExecute,
					_undoAction));

			Assert.ThrowsException<ArgumentNullException>(
				() => new CommandReversibleAndDisableAbleAsync<int?>(
					_canExecuteAction,
					actionToExecute: null,
					_undoAction));

			Assert.ThrowsException<ArgumentNullException>(
				() => new CommandReversibleAndDisableAbleAsync<int?>(
					_canExecuteAction,
					_actionToExecute,
					undoAction: null));
		}

		[TestMethod]
		public void ConstructHimself()
		{
			_ = new CommandReversibleAndDisableAbleAsync<int?>(
				_canExecuteAction,
				_actionToExecute,
				_undoAction);
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
		public async Task CannotExecute()
		{
			int expectedCount = _actualCount;
			var sut = new CommandReversibleAndDisableAbleAsync<int?>(
				o => false,
				_actionToExecute,
				_undoAction);

			await sut.ExecuteAsync(1);
			Assert.AreEqual(expectedCount, _actualCount);

			await sut.ReverseAsync(2);
			Assert.AreEqual(expectedCount, _actualCount);

			bool actualResult = await sut.CanExecuteAsync(1);
			Assert.AreEqual(expected: false, actualResult);
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
		public void CanExecute_ThrowsInvalidCastException()
		{
			Assert.ThrowsException<InvalidCastException>(
				() => _sut.Execute(""));
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

			_sut.Execute(1 as object);

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

		[TestMethod]
		public void Execute_ThrowsInvalidCastException()
		{
			Assert.ThrowsException<InvalidCastException>(
				() => _sut.Execute(""));
		}

		[TestMethod]
		public async Task Reverse()
		{
			var originalCount = _actualCount;

			await _sut.ExecuteAsync(1);
			Assert.AreNotEqual(originalCount, _actualCount);

			await _sut.ReverseAsync(1);
			Assert.AreEqual(originalCount, _actualCount);
		}

		[TestMethod]
		public void Reverse_GuardsAgainstNull()
		{
			Assert.ThrowsExceptionAsync<ArgumentNullException>(
				() => _sut.ReverseAsync(null));
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

			await _sut.ReverseAsync(1);
			Assert.AreEqual(originalCount, _actualCount);

			bool actualResult = await _sut.CanExecuteAsync(1);
			Assert.AreEqual(expected: false, actualResult);

			actualResult = _sut.CanExecute(1);
			Assert.AreEqual(expected: false, actualResult);
		}
		#endregion
	}
}
