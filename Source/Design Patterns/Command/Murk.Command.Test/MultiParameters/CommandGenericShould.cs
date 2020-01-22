using Murk.Command.MultiParameters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Murk.Test.Command.MultiParameters
{
	[TestClass]
	public class CommandGenericShould
	{
		#region Attributes
		private int _actualCount;
		private Command<int> _sut;
		private Action<int[]> _actionToExecute;
		private readonly int[] _parameters = { 1, 2, 3 };
		#endregion

		[TestInitialize]
		public void TestInitialize()
		{
			_actualCount = 0;
			_actionToExecute = o => _actualCount++;
			_sut = new Command<int>(_actionToExecute);
		}

		#region Constructor
		[TestMethod]
		public void Constructor_GuardsAgainstNull()
		{
			Assert.ThrowsException<ArgumentNullException>(
				() => new Command<int>(null));
		}

		[TestMethod]
		public void ConstructHimself()
		{
			_ = new Command<int>(_actionToExecute);
		}
		#endregion

		#region Execute
		[TestMethod]
		public void Execute()
		{
			var expectedCount = _actualCount;

			_sut.Execute(_parameters);

			Assert.AreNotEqual(expectedCount, _actualCount);
			Assert.IsTrue(expectedCount < _actualCount);
		}

		[TestMethod]
		public void Execute_GuardsAgainstNull()
		{
			Assert.ThrowsException<ArgumentNullException>(
				() => _sut.Execute(null));
		}

		[TestMethod]
		public void Execute_GuardsAgainstEmpty()
		{
			Assert.ThrowsException<ArgumentException>(
				() => _sut.Execute(new int[] {}));
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

			_sut.Execute(_parameters);
			Assert.AreEqual(originalCount, _actualCount);
		}
		#endregion
	}
}
