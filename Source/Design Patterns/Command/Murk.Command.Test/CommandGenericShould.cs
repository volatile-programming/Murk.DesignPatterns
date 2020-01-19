using Murk.Command;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Murk.Test.Command
{
	[TestClass]
	public class CommandGenericShould
	{
		#region Attributes
		private int _actualCount;
		private Command<object> _sut;
		private Action<object> _actionToExecute;
		#endregion

		[TestInitialize]
		public void TestInitialize()
		{
			_actualCount = 0;
			_actionToExecute = o => _actualCount++;
			_sut = new Command<object>(_actionToExecute);
		}

		#region Constructor
		[TestMethod]
		public void Constructor_GuardsAgainstNull()
		{
			Assert.ThrowsException<ArgumentNullException>(
				() => new Command<object>(null));
		}

		[TestMethod]
		public void ConstructHimself()
		{
			_ = new Command<object>(_actionToExecute);
		}
		#endregion

		#region Execute
		[TestMethod]
		public void Execute()
		{
			var expectedCount = _actualCount;

			_sut.Execute(1);

			Assert.AreNotEqual(expectedCount, _actualCount);
			Assert.IsTrue(expectedCount < _actualCount);
		}

		[TestMethod]
		public void Execute_GuardsAgainstNull()
		{
			Assert.ThrowsException<ArgumentNullException>(
				() => _sut.Execute(null));
		}
		#endregion

		#region Dispose
		[TestMethod]
		public void Dispose()
		{
			_sut.Dispose();
		}

		[TestMethod]
		public void Dispose_CannotExecute()
		{
			int originalCount = _actualCount;

			_sut.Dispose();
			_sut.Execute(1);

			Assert.AreEqual(originalCount, _actualCount);
		}
		#endregion
	}
}
