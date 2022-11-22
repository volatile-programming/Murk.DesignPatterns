using Murk.Common;
using Murk.DesignPatterns.Interfaces.Command.Parameterless;
using System.Threading.Tasks;

namespace Murk.DesignPatterns.BaseClasses.Command.Parameterless
{
    /// <summary>
    /// Base class for parameterless async  commands.
    /// Implements <see cref="System.Windows.Input.ICommand" />.
    /// </summary>
    public abstract class BaseCommandDisableAbleAsync :
        BaseInputCommand,
        ICommandDisableAbleAsync
    {
        /// <inheritdoc/>
        public abstract Task<bool> CanExecuteAsync();

        /// <inheritdoc/>
        public abstract Task ExecuteAsync();

        #region Interface Methods
        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            if (IsDisposing)
                return false;

            return CanExecuteAsync().Result;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            if (IsDisposing || !CanExecute(parameter))
                return;

            ExecuteAsync().Wait();
        }
        #endregion
    }
}
