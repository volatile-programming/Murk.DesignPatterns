using Microsoft.VisualStudio.TestTools.UnitTesting;
using Murk.Command;
using System;

namespace Murk.Test.Command
{
	[TestClass]
	public class CommandDisableAbleShould
	{
		#region Attributes
		private int _actualCount;
		private CommandDisableAble _sut;
		private Func<object, bool> _canExecuteAction;
		private Action<object> _actionToExecute;
		#endregion

		[TestInitialize]
		public void TestInitialize()
		{
			_actualCount = 0;
			_canExecuteAction = o => true;
			_actionToExecute = o => _actualCount++;
			_sut = new CommandDisableAble(
				_canExecuteAction,
				_actionToExecute);
			_sut.CanExecuteChanged += (o, e) => _actualCount++;
		}

		#region Constructor
		[TestMethod]
		public void Constructor_GuardsAgainstNull()
		{
			Assert.ThrowsException<ArgumentNullException>(
				() => new CommandDisableAble(null, _actionToExecute));

			Assert.ThrowsException<ArgumentNullException>(
				() => new CommandDisableAble(_canExecuteAction, null));
		}

		[TestMethod]
		public void ConstructHimself()
		{
			_ = new CommandDisableAble(
				_canExecuteAction,
				_actionToExecute);
		}
		#endregion

		#region Execute
		[TestMethod]
		public void CanExecute()
		{
			var actualResult = _sut.CanExecute(1);

			Assert.AreEqual(expected: true, actualResult);
		}

		[TestMethod]
		public void CanExecute_ObjectParameter()
		{
			var actualResult = _sut.CanExecute(1 as object);

			Assert.AreEqual(expected: true, actualResult);
		}

		[TestMethod]
		public void CannotExecute()
		{
			int expectedCount = _actualCount;
			var sut = new CommandDisableAble(
				o => false, _actionToExecute);

			sut.Execute(1);
			bool actualResult = sut.CanExecute(1);

			Assert.AreEqual(expected: false, actualResult);
			Assert.AreEqual(expectedCount, _actualCount);
		}

		[TestMethod]
		public void CanExecute_GuardsAgainstNull()
		{
			Assert.ThrowsException<ArgumentNullException>(
				() => _sut.CanExecute(null));
		}

		[TestMethod]
		public void Execute()
		{
			var expectedCount = _actualCount;

			_sut.Execute(1);

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
		public void Dispose_CannotExecute()
		{
			int originalCount = _actualCount;

			_sut.Dispose();

			_sut.Execute(1);
			Assert.AreEqual(originalCount, _actualCount);

			_sut.Execute(1 as object);
			Assert.AreEqual(originalCount, _actualCount);

			bool actualResult = _sut.CanExecute(1);
			Assert.AreEqual(expected: false, actualResult);

			actualResult = _sut.CanExecute(1 as object);
			Assert.AreEqual(expected: false, actualResult);
		}
		#endregion
	}
}
