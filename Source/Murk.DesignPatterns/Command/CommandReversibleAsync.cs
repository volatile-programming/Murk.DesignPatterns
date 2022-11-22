using Murk.Common;
using Murk.DesignPatterns.BaseClasses.Command;
using System;
using System.Threading.Tasks;

namespace Murk.DesignPatterns.Command
{
    /// <summary>
    /// Lightweight reversible generic async command.
    /// </summary>
    /// <typeparam name="T"><inheritdoc/></typeparam>
    public class CommandReversibleAsync<T> :
        BaseCommandReversibleAsync<T>
    {
        #region Attributes
        private Action<T> _actionToExecute;
        private Action<T> _undoAction;
        #endregion

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="actionToExecute">The command to be executed.
        /// </param>
        /// <param name="undoAction">The undo command operation.
        /// </param>
        /// <exception cref="ArgumentNullException" />
        public CommandReversibleAsync(
            Action<T> actionToExecute,
            Action<T> undoAction)
        {
            Guard.Against.Null(actionToExecute, nameof(actionToExecute));
            Guard.Against.Null(undoAction, nameof(undoAction));

            _actionToExecute = actionToExecute;
            _undoAction = undoAction;
        }

        #region Interface Methods
        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException" />
        public override Task ExecuteAsync(T parameter)
        {
            Guard.Against.Null(parameter, nameof(parameter));

            return Task.Run(() =>
            {
                if (IsDisposing)
                    return;

                _actionToExecute.Invoke(parameter);
            });
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException" />
        public override Task ReverseAsync(T parameter)
        {
            Guard.Against.Null(parameter, nameof(parameter));

            return Task.Run(() =>
            {
                if (IsDisposing)
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
            _actionToExecute = null;
            _undoAction = null;
        }
        #endregion
    }
}
