using Murk.Common;
using Murk.DesignPatterns.BaseClasses.Command.MultiParameters;
using System;

namespace Murk.DesignPatterns.Command.MultiParameter
{
    /// <summary>
    /// Disposable multi parameter command.
    /// </summary>
    public class CommandDisableAble : BaseCommandDisableAble
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
        public CommandDisableAble(
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
        public override bool CanExecute(object[] parameters)
        {
            Guard.Against.NullOrEmpty(parameters, nameof(parameters));

            if (IsDisposing)
                return false;

            return _canExecuteAction.Invoke(parameters);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentException" />
        /// <exception cref="ArgumentNullException" />
        public override void Execute(object[] parameters)
        {
            Guard.Against.NullOrEmpty(parameters, nameof(parameters));

            if (IsDisposing || !CanExecute(parameters))
                return;

            _actionToExecute.Invoke(parameters);
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
    /// Disposable multi parameter generic command.
    /// </summary>
    /// <typeparam name="T"><inheritdoc/></typeparam>
    public class CommandDisableAble<T> : BaseCommandDisableAble<T>
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
        public CommandDisableAble(
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
        public override bool CanExecute(params T[] parameters)
        {
            Guard.Against.NullOrEmpty(parameters, nameof(parameters));

            if (IsDisposing)
                return false;

            return _canExecuteAction.Invoke(parameters);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentException" />
        /// <exception cref="ArgumentNullException" />
        public override void Execute(params T[] parameters)
        {
            Guard.Against.NullOrEmpty(parameters, nameof(parameters));

            if (IsDisposing || !CanExecute(parameters))
                return;

            _actionToExecute.Invoke(parameters);
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
