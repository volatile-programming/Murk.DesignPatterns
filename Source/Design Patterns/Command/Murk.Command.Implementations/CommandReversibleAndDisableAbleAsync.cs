using Murk.Common;
using System;
using System.Threading.Tasks;

namespace Murk.Command
{
	/// <summary>
	/// Lightweight async command that can be disable and reverse.
	/// Implements <see cref="System.Windows.Input.ICommand"/>
	/// </summary>
	public class CommandReversibleAndDisableAbleAsync :
		BaseCommandReversibleAndDisableAbleAsync
	{
		#region Attributes
		private Func<object, bool> _canExecuteAction;
		private Action<object> _actionToExecute;
		private Action<object> _undoAction;
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="canExecuteAction">Function that indicates
		/// whether or not the command can be executed.
		/// </param>
		/// <param name="actionToExecute">The command to be executed.
		/// </param>
		/// <param name="undoAction">The undo command operation.
		/// </param>
		/// <exception cref="ArgumentNullException" />
		public CommandReversibleAndDisableAbleAsync(
			Func<object, bool> canExecuteAction,
			Action<object> actionToExecute,
			Action<object> undoAction)
		{
			Guard.Against.Null(canExecuteAction, nameof(canExecuteAction));
			Guard.Against.Null(actionToExecute, nameof(actionToExecute));
			Guard.Against.Null(undoAction, nameof(undoAction));

			_canExecuteAction = canExecuteAction;
			_actionToExecute = actionToExecute;
			_undoAction = undoAction;
		}

		#region Interface Methods
		/// <inheritdoc/>
		/// <exception cref="ArgumentNullException" />
		public override Task<bool> CanExecuteAsync(object parameter)
		{
			Guard.Against.Null(parameter, nameof(parameter));

			if (IsDisposing)
				return Task.FromResult(false);

			return Task.Run(() => _canExecuteAction.Invoke(parameter));
		}

		/// <inheritdoc />
		/// <exception cref="ArgumentNullException" />
		public override Task ExecuteAsync(object parameter)
		{
			Guard.Against.Null(parameter, nameof(parameter));

			return Task.Run(() =>
			{
				if (IsDisposing || !CanExecuteAsync(parameter).Result)
					return;

				_actionToExecute.Invoke(parameter);
			});
		}

		/// <inheritdoc />
		/// <exception cref="ArgumentNullException" />
		public override Task ReverseAsync(object parameter)
		{
			Guard.Against.Null(parameter, nameof(parameter));

			return Task.Run(() =>
			{
				if (IsDisposing || !CanExecuteAsync(parameter).Result)
					return;

				_undoAction.Invoke(parameter);
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
			_undoAction = null;
		}
		#endregion
	}

	/// <summary>
	/// Lightweight generic async command that can be disable and reverse.
	/// Implements <see cref="System.Windows.Input.ICommand"/>
	/// </summary>
	/// <typeparam name="T"><inheritdoc/></typeparam>
	public class CommandReversibleAndDisableAbleAsync<T> :
		BaseCommandReversibleAndDisableAbleAsync<T>
	{
		#region Attributes
		private Func<T, bool> _canExecuteAction;
		private Action<T> _actionToExecute;
		private Action<T> _undoAction;
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="canExecuteAction">Function that indicates
		/// whether or not the command can be executed.
		/// </param>
		/// <param name="actionToExecute">The command to be executed.
		/// </param>
		/// <param name="undoAction">The undo command operation.
		/// </param>
		/// <exception cref="ArgumentNullException" />
		public CommandReversibleAndDisableAbleAsync(
			Func<T, bool> canExecuteAction,
			Action<T> actionToExecute,
			Action<T> undoAction)
		{
			Guard.Against.Null(canExecuteAction, nameof(canExecuteAction));
			Guard.Against.Null(actionToExecute, nameof(actionToExecute));
			Guard.Against.Null(undoAction, nameof(undoAction));

			_canExecuteAction = canExecuteAction;
			_actionToExecute = actionToExecute;
			_undoAction = undoAction;
		}

		#region Interface Methods
		/// <inheritdoc/>
		/// <exception cref="ArgumentNullException" />
		public override Task<bool> CanExecuteAsync(T parameter)
		{
			Guard.Against.Null(parameter, nameof(parameter));

			if (IsDisposing)
				return Task.FromResult(false);

			return Task.Run(() => _canExecuteAction.Invoke(parameter));
		}

		/// <inheritdoc />
		/// <exception cref="ArgumentNullException" />
		public override Task ExecuteAsync(T parameter)
		{
			Guard.Against.Null(parameter, nameof(parameter));

			return Task.Run(() =>
			{
				if (IsDisposing || !CanExecuteAsync(parameter).Result)
					return;

				_actionToExecute.Invoke(parameter);
			});
		}

		/// <inheritdoc />
		/// <exception cref="ArgumentNullException" />
		public override Task ReverseAsync(T parameter)
		{
			Guard.Against.Null(parameter, nameof(parameter));

			return Task.Run(() =>
			{
				if (IsDisposing || !CanExecuteAsync(parameter).Result)
					return;

				_undoAction.Invoke(parameter);
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
			_undoAction = null;
		}
		#endregion
	}
}
