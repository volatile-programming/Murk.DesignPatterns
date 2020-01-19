using Murk.Commons;
using System;

namespace Murk.Command.Parameterless
{
	/// <summary>
	/// Lightweight reversible command.
	/// </summary>
	public class CommandReversible : BaseCommandReversible
	{
		#region Attributes
		private Action _actionToExecute;
		private Action _undoAction;
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
			Action actionToExecute,
			Action undoAction)
		{
			Guard.Against.Null(actionToExecute, nameof(actionToExecute));
			Guard.Against.Null(undoAction, nameof(undoAction));

			_actionToExecute = actionToExecute;
			_undoAction = undoAction;
		}

		#region Interface Methods
		/// <inheritdoc/>
		public override void Execute()
		{
			if (IsDisposing)
				return;

			_actionToExecute.Invoke();
		}

		/// <inheritdoc/>
		public override void Reverse()
		{
			if (IsDisposing)
				return;

			_undoAction.Invoke();
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
