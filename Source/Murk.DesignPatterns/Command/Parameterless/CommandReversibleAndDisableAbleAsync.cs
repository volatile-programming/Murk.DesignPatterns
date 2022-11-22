using Murk.Common;
using Murk.DesignPatterns.BaseClasses.Command.Parameterless;
using System;
using System.Threading.Tasks;

namespace Murk.DesignPatterns.Command.Parameterless
{
    /// <summary>
    /// Lightweight parameterless async command
    /// that can be disable and reverse.
    /// Implements <see cref="System.Windows.Input.ICommand"/>
    /// </summary>
    public class CommandReversibleAndDisableAbleAsync :
        BaseCommandReversibleAndDisableAbleAsync
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
}
