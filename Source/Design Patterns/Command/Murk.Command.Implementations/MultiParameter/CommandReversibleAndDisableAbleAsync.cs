using Murk.Commons;
using System;
using System.Threading.Tasks;

namespace Murk.Command.MultiParameters
{
	/// <summary>
	/// Multi parameter async command that can be disable and reverse.
	/// Implements <see cref="System.Windows.Input.ICommand"/>
	/// </summary>
	public class CommandReversibleAndDisableAbleAsync :
		BaseCommandReversibleAndDisableAbleAsync
	{
		#region Attributes
		private Func<object[], bool> _canExecuteAction;
		private Action<object[]> _actionToExecute;
		private Action<object[]> _undoAction;
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
			Func<object[], bool> canExecuteAction,
			Action<object[]> actionToExecute,
			Action<object[]> undoAction)
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
		/// <exception cref="ArgumentException" />
		/// <exception cref="ArgumentNullException" />
		public override Task<bool>
			CanExecuteAsync(params object[] parameters)
		{
			Guard.Against.NullOrEmpty(parameters, nameof(parameters));

			if (IsDisposing)
				return Task.FromResult(false);

			return Task.Run(() => _canExecuteAction.Invoke(parameters));
		}

		/// <inheritdoc />
		/// <exception cref="ArgumentException" />
		/// <exception cref="ArgumentNullException" />
		public override Task ExecuteAsync(params object[] parameters)
		{
			Guard.Against.NullOrEmpty(parameters, nameof(parameters));

			return Task.Run(() =>
			{
				if (IsDisposing || !CanExecuteAsync(parameters).Result)
					return;

				_actionToExecute.Invoke(parameters);
			});
		}

		/// <inheritdoc />
		/// <exception cref="ArgumentException" />
		/// <exception cref="ArgumentNullException" />
		public override Task ReverseAsync(params object[] parameters)
		{
			Guard.Against.NullOrEmpty(parameters, nameof(parameters));

			return Task.Run(() =>
			{
				if (IsDisposing || !CanExecuteAsync(parameters).Result)
					return;

				_undoAction.Invoke(parameters);
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
	/// Multi parameter generic async command
	/// that can be disable and reverse.
	/// Implements <see cref="System.Windows.Input.ICommand"/>
	/// </summary>
	/// <typeparam name="T"><inheritdoc/></typeparam>
	public class CommandReversibleAndDisableAbleAsync<T> :
		BaseCommandReversibleAndDisableAbleAsync<T>
	{
		#region Attributes
		private Func<T[], bool> _canExecuteAction;
		private Action<T[]> _actionToExecute;
		private Action<T[]> _undoAction;
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
			Func<T[], bool> canExecuteAction,
			Action<T[]> actionToExecute,
			Action<T[]> undoAction)
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
		/// <exception cref="ArgumentException" />
		/// <exception cref="ArgumentNullException" />
		public override Task<bool> CanExecuteAsync(params T[] parameters)
		{
			Guard.Against.NullOrEmpty(parameters, nameof(parameters));

			if (IsDisposing)
				return Task.FromResult(false);

			return Task.Run(() => _canExecuteAction.Invoke(parameters));
		}

		/// <inheritdoc />
		/// <exception cref="ArgumentException" />
		/// <exception cref="ArgumentNullException" />
		public override Task ExecuteAsync(params T[] parameters)
		{
			Guard.Against.NullOrEmpty(parameters, nameof(parameters));

			return Task.Run(() =>
			{
				if (IsDisposing || !CanExecuteAsync(parameters).Result)
					return;

				_actionToExecute.Invoke(parameters);
			});
		}

		/// <inheritdoc />
		/// <exception cref="ArgumentException" />
		/// <exception cref="ArgumentNullException" />
		public override Task ReverseAsync(params T[] parameters)
		{
			Guard.Against.NullOrEmpty(parameters, nameof(parameters));

			return Task.Run(() =>
			{
				if (IsDisposing || !CanExecuteAsync(parameters).Result)
					return;

				_undoAction.Invoke(parameters);
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
