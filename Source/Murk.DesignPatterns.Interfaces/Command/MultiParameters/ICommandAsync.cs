using System.Threading.Tasks;

namespace Murk.DesignPatterns.Interfaces.Command.MultiParameters
{
    /// <summary>
    /// A interface that represents a async Command.
    /// </summary>
    public interface ICommandAsync
    {
        /// <summary>
        /// The main command concern to be executed when needed.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        Task ExecuteAsync(params object[] parameter);
    }

    /// <summary>
    /// A generic interface that represents a async
    /// multi parameter command.
    /// </summary>
    /// <typeparam name="T">Parameters type.</typeparam>
    public interface ICommandAsync<in T>
    {
        /// <summary>
        /// The main command concern to be executed when needed.
        /// </summary>
        /// <param name="parameters">Command parameters.</param>
        Task ExecuteAsync(params T[] parameters);
    }
}
