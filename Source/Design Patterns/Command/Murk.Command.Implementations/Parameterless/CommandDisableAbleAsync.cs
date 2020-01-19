using Murk.Commons;
using System;
using System.Threading.Tasks;

namespace Murk.Command.Parameterless
{
	/// <summary>
	/// Disposable parameterless async command.
	/// </summary>
	public class CommandDisableAbleAsync :
		BaseCommandDisableAbleAsync
	{
		#region Attributes
		private Func<bool> _canExecuteAction;
		private Action _actionToExecute;
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="canExecuteAction">Function that indicates
		/// whether or not the command can be executed.
		/// </param>
		/// <param name="actionToExecute">The command to be executed.
		/// </param>
		/// <exception cref="ArgumentNullException" />
		public CommandDisableAbleAsync(
			Func<bool> canExecuteAction,
			Action actionToExecute)
		{
			Guard.Against.Null(canExecuteAction, nameof(canExecuteAction));
			Guard.Against.Null(actionToExecute, nameof(actionToExecute));

			_canExecuteAction = canExecuteAction;
			_actionToExecute = actionToExecute;
		}

		#region Interface Methods
		/// <inheritdoc />
		public override Task<bool> CanExecuteAsync()
		{
			if (IsDisposing)
				return Task.FromResult(false);

			return Task.Run(() => _canExecuteAction.Invoke());
		}

		/// <inheritdoc />
		public override Task ExecuteAsync()
		{
			return Task.Run(() =>
			{
				if (IsDisposing || !CanExecuteAsync().Result)
					return;

				_actionToExecute.Invoke();
			});
		}
		#endregion

		#region Dispose
		/// <inheritdoc/>
		protected override void Dispose(bool isDisposing)
		{
			base.Dispose(isDisposing);
			_canExecuteAction = null;
			_actionToExecute = null;
		}
		#endregion
	}
}
