using Murk.Common;
using System;

namespace Murk.Command.MultiParameters
{
	/// <summary>
	/// Lightweight disposable multi parameter command.
	/// Just has the execute functionality.
	/// </summary>
	public class Command : BaseCommand
	{
		private Action<object[]> _actionToExecute;

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="actionToExecute">The command to be executed.
		/// </param>
		/// <exception cref="System.ArgumentNullException"/>
		public Command(Action<object[]> actionToExecute)
		{
			Guard.Against.Null(actionToExecute, nameof(actionToExecute));

			_actionToExecute = actionToExecute;
		}

		/// <inheritdoc/>
		/// <exception cref="System.ArgumentException"/>
		/// <exception cref="System.ArgumentNullException"/>
		public override void Execute(params object[] parameters)
		{
			Guard.Against.NullOrEmpty(parameters, nameof(parameters));

			if (IsDisposing)
				return;

			_actionToExecute.Invoke(parameters);
		}

		#region Dispose
		/// <inheritdoc/>
		protected override void Dispose(bool isDisposing)
		{
			base.Dispose(isDisposing);
			_actionToExecute = null;
		}
		#endregion
	}

	/// <summary>
	/// Lightweight generic multi parameter command.
	/// Just has the execute functionality.
	/// </summary>
	/// <typeparam name="T"><inheritdoc/></typeparam>
	public class Command<T> :
		BaseCommand<T>
	{
		private Action<T[]> _actionToExecute;

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="actionToExecute">The command to be executed.
		/// </param>
		public Command(Action<T[]> actionToExecute)
		{
			Guard.Against.Null(actionToExecute, nameof(actionToExecute));

			_actionToExecute = actionToExecute;
		}

		/// <inheritdoc/>
		/// <exception cref="System.ArgumentException"/>
		/// <exception cref="System.ArgumentNullException"/>
		public override void Execute(params T[] parameters)
		{
			Guard.Against.NullOrEmpty(parameters, nameof(parameters));

			if (IsDisposing)
				return;

			_actionToExecute.Invoke(parameters);
		}

		#region Dispose
		/// <inheritdoc/>
		protected override void Dispose(bool isDisposing)
		{
			base.Dispose(isDisposing);
			_actionToExecute = null;
		}
		#endregion
	}
}
