using Microsoft.VisualStudio.TestTools.UnitTesting;
using Murk.Command.MultiParameters;
using System;
using System.Threading.Tasks;

namespace Murk.Test.Command.MultiParameters
{
	[TestClass]
	public class CommandAsyncGenericShould
	{
		#region Attributes
		private int _actualCount;
		private CommandAsync<int> _sut;
		private Action<int[]> _actionToExecute;
		private readonly int[] _parameters = { 1, 2, 3 };
		#endregion

		[TestInitialize]
		public void TestInitialize()
		{
			_actualCount = 0;
			_actionToExecute = o => _actualCount++;
			_sut = new CommandAsync<int>(_actionToExecute);
		}

		#region Constructor
		[TestMethod]
		public void Constructor_GuardsAgainstNull()
		{
			Assert.ThrowsException<ArgumentNullException>(
				() => new CommandAsync<int>(null));
		}

		[TestMethod]
		public void ConstructHimself()
		{
			_ = new CommandAsync<int>(_actionToExecute);
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
				() => _sut.ExecuteAsync(Array.Empty<int>()));
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
