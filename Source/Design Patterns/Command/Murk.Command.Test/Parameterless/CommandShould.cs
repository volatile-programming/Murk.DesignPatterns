using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Clean = Murk.Command.Parameterless;

namespace Murk.Test.Command.Parameterless
{
	[TestClass]
	public class CommandShould
	{
		#region Attributes
		private int _actualCount;
		private Clean.Command _sut;
		private Action _actionToExecute;
		#endregion

		[TestInitialize]
		public void TestInitialize()
		{
			_actualCount = 0;
			_actionToExecute = () => _actualCount++;
			_sut = new Clean.Command(_actionToExecute);
		}

		#region Constructor
		[TestMethod]
		public void Constructor_GuardsAgainstNull()
		{
			Assert.ThrowsException<ArgumentNullException>(
				() => new Clean.Command(null));
		}

		[TestMethod]
		public void ConstructHimself()
		{
			_ = new Clean.Command(_actionToExecute);
		}
		#endregion

		#region Execute
		[TestMethod]
		public void Execute()
		{
			var expectedCount = _actualCount;

			_sut.Execute();

			Assert.AreNotEqual(expectedCount, _actualCount);
			Assert.IsTrue(expectedCount < _actualCount);
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

			_sut.Execute();
			Assert.AreEqual(originalCount, _actualCount);
		}
		#endregion
	}
}
