using Murk.Common;
using Murk.DesignPatterns.BaseClasses.Command.Parameterless;
using System;

namespace Murk.DesignPatterns.Command.Parameterless
{
    /// <summary>
    /// Lightweight disposable command.
    /// Implements <see cref="System.Windows.Input.ICommand"/>.
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
}
