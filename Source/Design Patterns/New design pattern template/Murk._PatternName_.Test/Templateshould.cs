using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Murk._PatternName_.Test
{
	[TestClass]
	public class TemplateShould
	{
		private Template _sut;

		#region "Test Initialize"
		[TestInitialize]
		public void TestInitialize()
		{
			_sut = new Template("Hello {0}");
		}
		#endregion

		#region Constructor
		[TestMethod]
		public void Constructor_GuardsAgainstNull()
		{
			Assert.ThrowsException<ArgumentNullException>(
				() => new Template(null));
		}

		[TestMethod]
		public void Constructor_GuardsAgainstEmpty()
		{
			Assert.ThrowsException<ArgumentException>(
				() => new Template(""));
		}
		#endregion

		#region "Say Hello"
		[TestMethod]
		public void SayHello()
		{
			var result = _sut.SayHello("Jesse");

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void SayHello_GuardsAgainstNull()
		{
			Assert.ThrowsException<ArgumentNullException>(
				() => _sut.SayHello(null));
		}

		[TestMethod]
		public void SayHello_GuardsAgainstEmpty()
		{
			Assert.ThrowsException<ArgumentException>(
				() => _sut.SayHello(""));
		}
		#endregion

		#region Dispose
		[TestMethod]
		public void Dispose()
		{
			_sut.Dispose();
		}

		[TestMethod]
		public void Dispose_NoSayHello()
		{
			_sut.Dispose();

			var result = _sut.SayHello("Jeffrey");

			Assert.IsFalse(result);
		}
		#endregion
	}
}
