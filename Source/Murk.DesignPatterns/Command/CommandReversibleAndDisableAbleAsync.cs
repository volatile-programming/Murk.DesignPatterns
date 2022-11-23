using System;
using System.Threading.Tasks;
using Murk.Common.Helpers;
using Murk.DesignPatterns.BaseClasses.Command;

namespace Murk.DesignPatterns.Command
{
    /// <summary>
    /// Lightweight async parameterless command that can be disable and reverse.
    /// </summary>
    public class CommandReversibleAndDisableAbleAsync : BaseCommandReversibleAndDisableAbleAsync
    {
        #region Attributes

        private Func<bool> _canExecuteAction;
        private Action _actionToExecute;
        private Action _undoAction;

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
            Func<bool> canExecuteAction,
            Action actionToExecute,
            Action undoAction)
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

        /// <inheritdoc />
        public override Task ReverseAsync()
        {
            return Task.Run(() =>
            {
                if (IsDisposing || !CanExecuteAsync().Result)
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
            _canExecuteAction = null;
            _actionToExecute = null;
            _undoAction = null;
        }

        #endregion
    }

    /// <summary>
    /// Lightweight generic async command that can be disable and reverse.
    /// </summary>
    /// <typeparam name="T"><inheritdoc/></typeparam>
    public class CommandReversibleAndDisableAbleAsync<T> : BaseCommandReversibleAndDisableAbleAsync<T>
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
