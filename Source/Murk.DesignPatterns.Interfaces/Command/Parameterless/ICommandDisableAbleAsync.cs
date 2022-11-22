using System.Threading.Tasks;

namespace Murk.DesignPatterns.Interfaces.Command.Parameterless
{
    /// <summary>
    /// A interface that represents a async disable able command.
    /// </summary>
    public interface ICommandDisableAbleAsync :
        System.Windows.Input.ICommand,
        ICommandAsync
    {
        /// <summary>
        /// Determines whether or not the command can be executed.
        /// </summary>
        /// <returns>True/False</returns>
        Task<bool> CanExecuteAsync();
    }
}
