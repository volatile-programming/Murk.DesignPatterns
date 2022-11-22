using Murk.Common;
using Murk.DesignPatterns.BaseClasses.Command;
using System;
using System.Threading.Tasks;

namespace Murk.DesignPatterns.Command
{
    /// <summary>
    /// Lightweight disposable generic command.
    /// Just has the execute functionality.
    /// </summary>
    /// <typeparam name="T"><inheritdoc /></typeparam>
    public class CommandAsync<T> : BaseCommandAsync<T>
    {
        private Action<T> _actionToExecute;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="actionToExecute">The command to be executed.
        /// </param>
        /// <exception cref="ArgumentNullException" />
        public CommandAsync(Action<T> actionToExecute)
        {
            Guard.Against.Null(actionToExecute, nameof(actionToExecute));

            _actionToExecute = actionToExecute;
        }

        /// <inheritdoc />
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
