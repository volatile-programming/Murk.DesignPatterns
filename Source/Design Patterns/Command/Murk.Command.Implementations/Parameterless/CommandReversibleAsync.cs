using Murk.Common;
using System;
using System.Threading.Tasks;

namespace Murk.Command.Parameterless
{
	/// <summary>
	/// Lightweight reversible parameterless async command.
	/// </summary>
	public class CommandReversibleAsync :
		BaseCommandReversibleAsync
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
		public CommandReversibleAsync(
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
		public override Task ExecuteAsync()
		{
			return Task.Run(() =>
			{
				if (IsDisposing)
					return;

				_actionToExecute.Invoke();
			});
		}

		/// <inheritdoc/>
		public override Task ReverseAsync()
		{
			return Task.Run(() =>
			{
				if (IsDisposing)
					return;

				_undoAction.Invoke();
			});
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
