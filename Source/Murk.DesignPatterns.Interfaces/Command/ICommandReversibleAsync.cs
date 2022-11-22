using System.Threading.Tasks;

namespace Murk.DesignPatterns.Interfaces.Command
{
    /// <summary>
    /// A interface that represents a async reversible
    /// Command.
    /// </summary>
    public interface ICommandReversibleAsync : ICommandAsync
    {
        /// <summary>
        /// The undo command operation.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        Task ReverseAsync(object parameter);
    }

    /// <summary>
    /// A generic interface that represents a async reversible
    /// Command.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface ICommandReversibleAsync<in T> : ICommandAsync<T>
    {
        /// <summary>
        /// The undo command operation.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        Task ReverseAsync(T parameter);
    }
}
