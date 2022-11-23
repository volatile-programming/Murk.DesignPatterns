using Murk.Common;
using Murk.DesignPatterns.BaseClasses.Command;
using System;

namespace Murk.DesignPatterns.Command
{
    /// <summary>
    /// Lightweight disposable parameterless command.
    /// Just has the execute functionality.
    /// </summary>
    public class Command : BaseCommand
    {
        private Action _actionToExecute;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="actionToExecute">The command to be executed.
        /// </param>
        /// <exception cref="ArgumentNullException" />
        public Command(Action actionToExecute)
        {
            Guard.Against.Null(actionToExecute, nameof(actionToExecute));

            _actionToExecute = actionToExecute;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException" />
        public override void Execute()
        {
            if (IsDisposing)
                return;

            _actionToExecute.Invoke();
        }

        #region Dispose
        /// <inheritdoc/>
        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            _actionToExecute = null;
        }
        #endregion
    }

    /// <summary>
    /// Lightweight disposable generic command.
    /// Just has the execute functionality.
    /// </summary>
    /// <typeparam name="T"><inheritdoc /></typeparam>
    public class Command<T> : BaseCommand<T>
    {
        private Action<T> _actionToExecute;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="actionToExecute">The command to be executed.
        /// </param>
        /// <exception cref="ArgumentNullException" />
        public Command(Action<T> actionToExecute)
        {
            Guard.Against.Null(actionToExecute, nameof(actionToExecute));

            _actionToExecute = actionToExecute;
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException" />
        public override void Execute(T parameter)
        {
            Guard.Against.Null(parameter, nameof(parameter));

            if (IsDisposing)
                return;

            _actionToExecute.Invoke(parameter);
        }

        #region Dispose
        /// <inheritdoc />
        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            _actionToExecute = null;
        }
        #endregion
    }
}
