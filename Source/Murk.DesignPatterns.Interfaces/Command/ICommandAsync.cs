using System.Threading.Tasks;

namespace Murk.DesignPatterns.Interfaces.Command
{
    /// <summary>
    /// A generic interface that represents an async Command.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface ICommandAsync<in T>
    {
        /// <summary>
        /// The main command concern to be executed when needed.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        Task ExecuteAsync(T parameter);
    }
}
