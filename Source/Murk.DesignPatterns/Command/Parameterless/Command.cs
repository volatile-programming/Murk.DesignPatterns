using Murk.Common;
using Murk.DesignPatterns.BaseClasses.Command.Parameterless;
using System;

namespace Murk.DesignPatterns.Command.Parameterless
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
}
