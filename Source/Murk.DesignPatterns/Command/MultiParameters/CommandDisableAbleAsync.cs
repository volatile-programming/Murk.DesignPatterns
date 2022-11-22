using Murk.Common;
using Murk.DesignPatterns.BaseClasses.Command.MultiParameters;
using System;
using System.Threading.Tasks;

namespace Murk.DesignPatterns.Command.MultiParameters
{
    /// <summary>
    /// Disposable multi parameter async command.
    /// </summary>
    public class CommandDisableAbleAsync :
        BaseCommandDisableAbleAsync
    {
        #region Attributes
        private Func<object[], bool> _canExecuteAction;
        private Action<object[]> _actionToExecute;
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
            Func<object[], bool> canExecuteAction,
            Action<object[]> actionToExecute)
        {
            Guard.Against.Null(canExecuteAction, nameof(canExecuteAction));
            Guard.Against.Null(actionToExecute, nameof(actionToExecute));

            _canExecuteAction = canExecuteAction;
            _actionToExecute = actionToExecute;
        }

        #region Interface Methods
        /// <inheritdoc />
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
    /// Disposable multi parameter generic async command.
    /// </summary>
    /// <typeparam name="T"><inheritdoc/></typeparam>
    public class CommandDisableAbleAsync<T> :
        BaseCommandDisableAbleAsync<T>
    {
        #region Attributes
        private Func<T[], bool> _canExecuteAction;
        private Action<T[]> _actionToExecute;
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
            Func<T[], bool> canExecuteAction,
            Action<T[]> actionToExecute)
        {
            Guard.Against.Null(canExecuteAction, nameof(canExecuteAction));
            Guard.Against.Null(actionToExecute, nameof(actionToExecute));

            _canExecuteAction = canExecuteAction;
            _actionToExecute = actionToExecute;
        }

        #region Interface Methods
        /// <inheritdoc />
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
