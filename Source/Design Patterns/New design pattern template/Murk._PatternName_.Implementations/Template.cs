using Murk.Commons;
using System;
using System.Diagnostics;

namespace Murk._PatternName_
{
	/// <summary>
	/// Implementing class example.
	/// </summary>
	public class Template : BaseTemplate
	{
		private string _messageBody;

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="messageBody">Message body to be display on debug
		/// console.</param>
		/// <exception cref="ArgumentNullException" />
		/// <exception cref="ArgumentException" />
		public Template(string messageBody)
		{
			Guard.Against.NullOrEmpty(messageBody, nameof(messageBody));

			_messageBody = messageBody;
		}

		/// <inheritdoc/>
		/// <exception cref="ArgumentNullException" />
		/// <exception cref="ArgumentException" />
		public override bool SayHello(string name)
		{
			Guard.Against.NullOrEmpty(name, nameof(name));

			if (IsDisposing)
				return false;

			Debug.WriteLine(_messageBody, name);
			return true;
		}

		#region Dispose
		/// <inheritdoc/>
		protected override void Dispose(bool isDisposing)
		{
			base.Dispose(isDisposing);
			_messageBody = null;
		}
		#endregion
	}
}
