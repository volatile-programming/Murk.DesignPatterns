using Murk.Common;
using Murk.DesignPatterns.BaseClasses.Command;
using System;
using System.Threading.Tasks;

namespace Murk.DesignPatterns.Command
{
    /// <summary>
    /// Disposable disable parameterless async command.
    /// </summary>
    public class CommandDisableAbleAsync : BaseCommandDisableAbleAsync
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
            return Task.Run(() =>
            {
                if (IsDisposing)
                    return false;

                return _canExecuteAction.Invoke();
            });
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

    /// <summary>
    /// Disposable generic disable able async command.
    /// </summary>
    /// <typeparam name="T"><inheritdoc/></typeparam>
    public class CommandDisableAbleAsync<T> : BaseCommandDisableAbleAsync<T>
    {
        #region Attributes
        private Func<T, bool> _canExecuteAction;
        private Action<T> _actionToExecute;
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
            Func<T, bool> canExecuteAction,
            Action<T> actionToExecute)
        {
            Guard.Against.Null(canExecuteAction, nameof(canExecuteAction));
            Guard.Against.Null(actionToExecute, nameof(actionToExecute));

            _canExecuteAction = canExecuteAction;
            _actionToExecute = actionToExecute;
        }

        #region Interface Methods
        /// <inheritdoc />
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
