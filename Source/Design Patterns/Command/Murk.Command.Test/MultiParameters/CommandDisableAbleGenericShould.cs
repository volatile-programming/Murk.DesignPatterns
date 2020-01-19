using Microsoft.VisualStudio.TestTools.UnitTesting;
using Murk.Command.MultiParameters;
using System;

namespace Murk.Test.Command.MultiParameters
{
	[TestClass]
	public class CommandDisableAbleGenericShould
	{
		#region Attributes
		private int _actualCount;
		private CommandDisableAble<int> _sut;
		private Func<int[], bool> _canExecuteAction;
		private Action<int[]> _actionToExecute;
		private readonly int[] _parameters = { 1, 2, 3 };
		#endregion

		[TestInitialize]
		public void TestInitialize()
		{
			_actualCount = 0;
			_canExecuteAction = o => true;
			_actionToExecute = o => _actualCount++;
			_sut = new CommandDisableAble<int>(
				_canExecuteAction,
				_actionToExecute);
			_sut.CanExecuteChanged += (o, e) => _actualCount++;
		}

		#region Constructor
		[TestMethod]
		public void Constructor_GuardsAgainstNull()
		{
			Assert.ThrowsException<ArgumentNullException>(
				() => new CommandDisableAble<int>(null, _actionToExecute));

			Assert.ThrowsException<ArgumentNullException>(
				() => new CommandDisableAble<int>(_canExecuteAction, null));
		}

		[TestMethod]
		public void ConstructHimself()
		{
			_ = new CommandDisableAble<int>(
				_canExecuteAction,
				_actionToExecute);
		}
		#endregion

		#region Execute
		[TestMethod]
		public void CanExecute()
		{
			var actualResult = _sut.CanExecute(_parameters);

			Assert.AreEqual(expected: true, actualResult);
		}

		[TestMethod]
		public void CanExecute_ObjectParameter()
		{
			var actualResult = _sut.CanExecute(_parameters as object);

			Assert.AreEqual(expected: true, actualResult);
		}

		[TestMethod]
		public void CanExecute_GuardsAgainstNull()
		{
			Assert.ThrowsException<ArgumentNullException>(
				() => _sut.CanExecute(null));

			object a = null;
			Assert.ThrowsException<ArgumentNullException>(
				() => _sut.CanExecute(a));
		}

		[TestMethod]
		public void CanExecute_GuardsAgainstEmpty()
		{
			Assert.ThrowsException<ArgumentException>(
				() => _sut.CanExecute(Array.Empty<int>()));

			Assert.ThrowsException<ArgumentException>(
				() => _sut.CanExecute(Array.Empty<int>() as object));
		}

		[TestMethod]
		public void CannotExecute()
		{
			int expectedCount = _actualCount;
			var sut = new CommandDisableAble<int>(
				o => false, _actionToExecute);

			bool actualResult = sut.CanExecute(_parameters);
			Assert.AreEqual(expected: false, actualResult);

			sut.Execute(_parameters);
			Assert.AreEqual(expectedCount, _actualCount);

			sut.Execute(_parameters as object);
			Assert.AreEqual(expectedCount, _actualCount);
		}

		[TestMethod]
		public void Execute()
		{
			var expectedCount = _actualCount;

			_sut.Execute(_parameters);

			Assert.AreNotEqual(expectedCount, _actualCount);
			Assert.IsTrue(expectedCount < _actualCount);
		}

		[TestMethod]
		public void Execute_ObjectParameter()
		{
			var expectedCount = _actualCount;

			_sut.Execute(_parameters as object);

			Assert.AreNotEqual(expectedCount, _actualCount);
			Assert.IsTrue(expectedCount < _actualCount);
		}

		[TestMethod]
		public void Execute_GuardsAgainstNull()
		{
			Assert.ThrowsException<ArgumentNullException>(
				() => _sut.Execute(null));

			object a = null;
			Assert.ThrowsException<ArgumentNullException>(
				() => _sut.Execute(a));
		}

		[TestMethod]
		public void Execute_GuardsAgainstEmpty()
		{
			Assert.ThrowsException<ArgumentException>(
				() => _sut.Execute(Array.Empty<int>()));

			Assert.ThrowsException<ArgumentException>(
				() => _sut.Execute(Array.Empty<int>() as object));
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

			_sut.Execute(_parameters);
			Assert.AreEqual(originalCount, _actualCount);

			_sut.Execute(_parameters as object);
			Assert.AreEqual(originalCount, _actualCount);

			bool actualResult = _sut.CanExecute(_parameters);
			Assert.AreEqual(expected: false, actualResult);

			actualResult = _sut.CanExecute(_parameters as object);
			Assert.AreEqual(expected: false, actualResult);
		}
		#endregion
	}
}
