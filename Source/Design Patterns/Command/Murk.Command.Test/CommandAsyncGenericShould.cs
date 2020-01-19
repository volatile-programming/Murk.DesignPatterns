using Microsoft.VisualStudio.TestTools.UnitTesting;
using Murk.Command;
using System;
using System.Threading.Tasks;

namespace Murk.Test.Command
{
	[TestClass]
	public class CommandAsyncGenericShould
	{
		#region Attributes
		private int _actualCount;
		private CommandAsync<object> _sut;
		private Action<object> _actionToExecute;
		#endregion

		[TestInitialize]
		public void TestInitialize()
		{
			_actualCount = 0;
			_actionToExecute = o => _actualCount++;
			_sut = new CommandAsync<object>(_actionToExecute);
		}

		#region Constructor
		[TestMethod]
		public void Constructor_GuardsAgainstNull()
		{
			Assert.ThrowsException<ArgumentNullException>(
				() => new CommandAsync<object>(null));
		}

		[TestMethod]
		public void ConstructHimself()
		{
			_ = new CommandAsync<object>(_actionToExecute);
		}
		#endregion

		#region Execute
		[TestMethod]
		public async Task Execute()
		{
			var expectedCount = _actualCount;

			await _sut.ExecuteAsync(1);

			Assert.AreNotEqual(expectedCount, _actualCount);
			Assert.IsTrue(expectedCount < _actualCount);
		}

		[TestMethod]
		public void Execute_GuardsAgainstNull()
		{
			Assert.ThrowsExceptionAsync<ArgumentNullException>(
				() => _sut.ExecuteAsync(null));
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

			await _sut.ExecuteAsync(1);
			Assert.AreEqual(originalCount, _actualCount);
		}
		#endregion
	}
}
