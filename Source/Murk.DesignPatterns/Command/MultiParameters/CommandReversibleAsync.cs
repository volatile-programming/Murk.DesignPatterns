using Murk.Common;
using Murk.DesignPatterns.BaseClasses.Command.MultiParameters;
using System;
using System.Threading.Tasks;

namespace Murk.DesignPatterns.Command.MultiParameters
{
    /// <summary>
    /// Lightweight reversible multi parameter async command.
    /// </summary>
    public class CommandReversibleAsync :
        BaseCommandReversibleAsync
    {
        #region Attributes
        private Action<object[]> _actionToExecute;
        private Action<object[]> _undoAction;
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
            Action<object[]> actionToExecute,
            Action<object[]> undoAction)
        {
            Guard.Against.Null(actionToExecute, nameof(actionToExecute));
            Guard.Against.Null(undoAction, nameof(undoAction));

            _actionToExecute = actionToExecute;
            _undoAction = undoAction;
        }

        #region Interface Methods
        /// <inheritdoc/>
        /// <exception cref="ArgumentException" />
        /// <exception cref="ArgumentNullException" />
        public override Task ExecuteAsync(params object[] parameters)
        {
            Guard.Against.NullOrEmpty(parameters, nameof(parameters));

            return Task.Run(() =>
            {
                if (IsDisposing)
                    return;

                _actionToExecute.Invoke(parameters);
            });
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException" />
        /// <exception cref="ArgumentNullException" />
        public override Task ReverseAsync(params object[] parameters)
        {
            Guard.Against.NullOrEmpty(parameters, nameof(parameters));

            return Task.Run(() =>
            {
                if (IsDisposing)
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
            _actionToExecute = null;
            _undoAction = null;
        }
        #endregion
    }

    /// <summary>
    /// Lightweight reversible multi parameter generic async command.
    /// </summary>
    /// <typeparam name="T"><inheritdoc/></typeparam>
    public class CommandReversibleAsync<T> :
        BaseCommandReversibleAsync<T>
    {
        #region Attributes
        private Action<T[]> _actionToExecute;
        private Action<T[]> _undoAction;
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
            Action<T[]> actionToExecute,
            Action<T[]> undoAction)
        {
            Guard.Against.Null(actionToExecute, nameof(actionToExecute));
            Guard.Against.Null(undoAction, nameof(undoAction));

            _actionToExecute = actionToExecute;
            _undoAction = undoAction;
        }

        #region Interface Methods
        /// <inheritdoc/>
        /// <exception cref="ArgumentException" />
        /// <exception cref="ArgumentNullException" />
        public override Task ExecuteAsync(params T[] parameters)
        {
            Guard.Against.NullOrEmpty(parameters, nameof(parameters));

            return Task.Run(() =>
            {
                if (IsDisposing)
                    return;

                _actionToExecute.Invoke(parameters);
            });
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException" />
        /// <exception cref="ArgumentNullException" />
        public override Task ReverseAsync(params T[] parameters)
        {
            Guard.Against.NullOrEmpty(parameters, nameof(parameters));

            return Task.Run(() =>
            {
                if (IsDisposing)
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
            _actionToExecute = null;
            _undoAction = null;
        }
        #endregion
    }
}
