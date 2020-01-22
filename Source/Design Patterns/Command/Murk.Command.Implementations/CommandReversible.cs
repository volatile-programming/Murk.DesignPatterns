using Murk.Common;
using System;

namespace Murk.Command
{
	/// <summary>
	/// Lightweight reversible command.
	/// </summary>
	public class CommandReversible : BaseCommandReversible
	{
		#region Attributes
		private Action<object> _actionToExecute;
		private Action<object> _undoAction;
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="actionToExecute">The command to be executed.
		/// </param>
		/// <param name="undoAction">The undo command operation.
		/// </param>
		/// <exception cref="ArgumentNullException" />
		public CommandReversible(
			Action<object> actionToExecute,
			Action<object> undoAction)
		{
			Guard.Against.Null(actionToExecute, nameof(actionToExecute));
			Guard.Against.Null(undoAction, nameof(undoAction));

			_actionToExecute = actionToExecute;
			_undoAction = undoAction;
		}

		#region Interface Methods
		/// <inheritdoc/>
		/// <exception cref="ArgumentNullException" />
		public override void Execute(object parameter)
		{
			Guard.Against.Null(parameter, nameof(parameter));

			if (IsDisposing)
				return;

			_actionToExecute.Invoke(parameter);
		}

		/// <inheritdoc/>
		/// <exception cref="ArgumentNullException" />
		public override void Reverse(object parameter)
		{
			Guard.Against.Null(parameter, nameof(parameter));

			if (IsDisposing)
				return;

			_undoAction.Invoke(parameter);
		}
		#endregion

		#region Dispose
		/// <inheritdoc/>
		protected override void Dispose(bool isDisposing)
		{
			base.Dispose(isDisposing);
			_actionToExecute = null;
			_undoAction = null;
		}
		#endregion
	}

	/// <summary>
	/// Lightweight reversible generic command.
	/// </summary>
	/// <typeparam name="T"><inheritdoc/></typeparam>
	public class CommandReversible<T> : BaseCommandReversible<T>
	{
		#region Attributes
		private Action<T> _actionToExecute;
		private Action<T> _undoAction;
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="actionToExecute">The command to be executed.
		/// </param>
		/// <param name="undoAction">The undo command operation.
		/// </param>
		/// <exception cref="ArgumentNullException" />
		public CommandReversible(
			Action<T> actionToExecute,
			Action<T> undoAction)
		{
			Guard.Against.Null(actionToExecute, nameof(actionToExecute));
			Guard.Against.Null(undoAction, nameof(undoAction));

			_actionToExecute = actionToExecute;
			_undoAction = undoAction;
		}

		#region Interface Methods
		/// <inheritdoc/>
		/// <exception cref="ArgumentNullException" />
		public override void Execute(T parameter)
		{
			Guard.Against.Null(parameter, nameof(parameter));

			if (IsDisposing)
				return;

			_actionToExecute.Invoke(parameter);
		}

		/// <inheritdoc/>
		/// <exception cref="ArgumentNullException" />
		public override void Reverse(T parameter)
		{
			Guard.Against.Null(parameter, nameof(parameter));

			if (IsDisposing)
				return;

			_undoAction.Invoke(parameter);
		}
		#endregion

		#region Dispose
		/// <inheritdoc/>
		protected override void Dispose(bool isDisposing)
		{
			base.Dispose(isDisposing);
			_actionToExecute = null;
			_undoAction = null;
		}
		#endregion
	}
}
