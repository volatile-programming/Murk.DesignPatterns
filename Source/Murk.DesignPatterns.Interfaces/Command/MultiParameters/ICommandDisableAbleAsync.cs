using System.Threading.Tasks;

namespace Murk.DesignPatterns.Interfaces.Command.MultiParameters
{
    /// <summary>
    /// A interface that represents a async
    /// disable able command.
    /// </summary>
    public interface ICommandDisableAbleAsync :
        System.Windows.Input.ICommand,
        ICommandAsync
    {
        /// <summary>
        /// Determines whether or not the command can be executed.
        /// </summary>
        /// <param name="parameters">Command parameter.</param>
        /// <returns>True/False</returns>
        Task<bool> CanExecuteAsync(params object[] parameters);
    }

    /// <summary>
    /// A generic interface that represents a async
    /// disable able command.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface ICommandDisableAbleAsync<in T> :
        System.Windows.Input.ICommand,
        ICommandAsync<T>
    {
        /// <summary>
        /// Determines whether or not the command can be executed.
        /// </summary>
        /// <param name="parameters">Command parameter.</param>
        /// <returns>True/False</returns>
        Task<bool> CanExecuteAsync(params T[] parameters);
    }
}
