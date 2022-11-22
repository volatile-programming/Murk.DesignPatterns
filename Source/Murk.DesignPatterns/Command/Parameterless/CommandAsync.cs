using Murk.Common;
using Murk.DesignPatterns.BaseClasses.Command.Parameterless;
using System;
using System.Threading.Tasks;

namespace Murk.DesignPatterns.Command.Parameterless
{
    /// <summary>
    /// Lightweight parameterless async command.
    /// Just has the execute functionality.
    /// </summary>
    public class CommandAsync : BaseCommandAsync
    {
        private Action _actionToExecute;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="actionToExecute">The command to be executed.
        /// </param>
        /// <exception cref="ArgumentNullException" />
        public CommandAsync(Action actionToExecute)
        {
            Guard.Against.Null(actionToExecute, nameof(actionToExecute));

            _actionToExecute = actionToExecute;
        }

        /// <inheritdoc />
        public override Task ExecuteAsync()
        {
            return Task.Run(() =>
            {
                if (IsDisposing)
                    return;

                _actionToExecute.Invoke();
            });
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
