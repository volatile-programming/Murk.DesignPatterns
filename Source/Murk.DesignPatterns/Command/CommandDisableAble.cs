using Murk.Common;
using Murk.DesignPatterns.BaseClasses.Command;
using System;

namespace Murk.DesignPatterns.Command
{
    /// <summary>
    /// Disposable disposable parameterless command.
    /// </summary>
    public class CommandDisableAble : BaseCommandDisableAble
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
        public CommandDisableAble(
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
        public override bool CanExecute()
        {
            if (IsDisposing)
                return false;

            return _canExecuteAction.Invoke();
        }

        /// <inheritdoc />
        public override void Execute()
        {
            if (IsDisposing || !CanExecute())
                return;

            _actionToExecute.Invoke();
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
    /// Disposable generic disposable command.
    /// </summary>
    /// <typeparam name="T"><inheritdoc/></typeparam>
    public class CommandDisableAble<T> : BaseCommandDisableAble<T>
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
        public CommandDisableAble(
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
        public override bool CanExecute(T parameter)
        {
            Guard.Against.Null(parameter, nameof(parameter));

            if (IsDisposing)
                return false;

            return _canExecuteAction.Invoke(parameter);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException" />
        public override void Execute(T parameter)
        {
            Guard.Against.Null(parameter, nameof(parameter));

            if (IsDisposing || !CanExecute(parameter))
                return;

            _actionToExecute.Invoke(parameter);
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
