using Murk.Command.MultiParameters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Murk.Test.Command.MultiParameters
{
	[TestClass]
	public class CommandReversibleAsyncGenericShould
	{
		#region Attributes
		private CommandReversibleAsync<int> _sut;
		private int _actualCount;
		private Action<int[]> _actionToExecute;
		private Action<int[]> _undoAction;
		private readonly int[] _parameters = { 1, 2, 3 };
		#endregion

		[TestInitialize]
		public void TestInitialize()
		{
			_actualCount = 0;
			_actionToExecute = o => _actualCount++;
			_undoAction = o => _actualCount--;
			_sut = new CommandReversibleAsync<int>(
				_actionToExecute,
				_undoAction);
		}

		#region Constructor
		[TestMethod]
		public void Constructor_GuardsAgainstNull()
		{
			Assert.ThrowsException<ArgumentNullException>(
				() => new CommandReversibleAsync<int>(null, _undoAction));

			Assert.ThrowsException<ArgumentNullException>(
				() => new CommandReversibleAsync<int>
					(_actionToExecute, null));
		}

		[TestMethod]
		public void ConstructHimself()
		{
			_ = new CommandReversibleAsync<int>(
				_actionToExecute,
				_undoAction);
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
				() => _sut.ExecuteAsync(new int[] {}));
		}

		[TestMethod]
		public async Task Reverse()
		{
			var originalCount = _actualCount;

			await _sut.ExecuteAsync(_parameters);
			Assert.AreNotEqual(originalCount, _actualCount);

			await _sut.ReverseAsync(_parameters);
			Assert.AreEqual(originalCount, _actualCount);
		}

		[TestMethod]
		public void Reverse_GuardsAgainstNull()
		{
			Assert.ThrowsExceptionAsync<ArgumentNullException>(
				() => _sut.ReverseAsync(null));
		}

		[TestMethod]
		public void Reverse_GuardsAgainstEmpty()
		{
			Assert.ThrowsExceptionAsync<ArgumentException>(
				() => _sut.ReverseAsync(new int[] {}));
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

			await _sut.ReverseAsync(_parameters);
			Assert.AreEqual(originalCount, _actualCount);
		}
		#endregion
	}
}
