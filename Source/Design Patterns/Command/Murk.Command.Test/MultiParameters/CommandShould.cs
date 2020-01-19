using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Clean = Murk.Command.MultiParameters;

namespace Murk.Test.Command.MultiParameters
{
	[TestClass]
	public class CommandShould
	{
		#region Attributes
		private int _actualCount;
		private Clean.Command _sut;
		private Action<object[]> _actionToExecute;
		private readonly object[] _parameters = { 1, 2f, 3d, '¡', "!" };
		#endregion

		[TestInitialize]
		public void TestInitialize()
		{
			_actualCount = 0;
			_actionToExecute = o => _actualCount++;
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
				() => _sut.Execute(Array.Empty<object>()));
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
