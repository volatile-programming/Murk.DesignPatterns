using Murk.Common;
using System;
using System.Threading.Tasks;

namespace Murk.Command.MultiParameters
{
	/// <summary>
	/// Lightweight multi parameter command.
	/// Just has the execute functionality.
	/// </summary>
	public class CommandAsync : BaseCommandAsync
	{
		private Action<object[]> _actionToExecute;

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="actionToExecute">The command to be executed.
		/// </param>
		/// <exception cref="ArgumentNullException" />
		public CommandAsync(Action<object[]> actionToExecute)
		{
			Guard.Against.Null(actionToExecute, nameof(actionToExecute));

			_actionToExecute = actionToExecute;
		}

		/// <inheritdoc />
		/// <exception cref="ArgumentException" />
		/// <exception cref="ArgumentNullException" />
		public override Task ExecuteAsync(params object[] parameters)
		{
			Guard.Against.NullOrEmpty(parameters, nameof(parameters));

			return Task.Run(() =>
			{
				if (IsDisposing)
					return;

				_actionToExecute.Invoke(parameters);
			});
		}

		#region Dispose
		/// <inheritdoc />
		protected override void Dispose(bool isDisposing)
		{
			base.Dispose(isDisposing);
			_actionToExecute = null;
		}
		#endregion
	}

	/// <summary>
	/// Lightweight multi parameter generic command.
	/// Just has the execute functionality.
	/// </summary>
	/// <typeparam name="T"><inheritdoc /></typeparam>
	public class CommandAsync<T> : BaseCommandAsync<T>
	{
		private Action<T[]> _actionToExecute;

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="actionToExecute">The command to be executed.
		/// </param>
		/// <exception cref="ArgumentNullException" />
		public CommandAsync(Action<T[]> actionToExecute)
		{
			Guard.Against.Null(actionToExecute, nameof(actionToExecute));

			_actionToExecute = actionToExecute;
		}

		/// <inheritdoc />
		/// <exception cref="ArgumentException" />
		/// <exception cref="ArgumentNullException" />
		public override Task ExecuteAsync(params T[] parameters)
		{
			Guard.Against.NullOrEmpty(parameters, nameof(parameters));

			return Task.Run(() =>
			{
				if (IsDisposing)
					return;

				_actionToExecute.Invoke(parameters);
			});
		}

		#region Dispose
		/// <inheritdoc />
		protected override void Dispose(bool isDisposing)
		{
			base.Dispose(isDisposing);
			_actionToExecute = null;
		}
		#endregion
	}
}
