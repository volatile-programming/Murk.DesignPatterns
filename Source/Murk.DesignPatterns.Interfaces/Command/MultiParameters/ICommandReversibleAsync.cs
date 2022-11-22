using System.Threading.Tasks;

namespace Murk.DesignPatterns.Interfaces.Command.MultiParameters
{
    /// <summary>
    /// A interface that represents a async reversible
    /// Command.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface ICommandReversibleAsync : ICommandAsync
    {
        /// <summary>
        /// The undo command operation.
        /// </summary>
        /// <param name="parameters">Command parameter.</param>
        Task ReverseAsync(params object[] parameters);
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
        /// <param name="parameters">Command parameter.</param>
        Task ReverseAsync(params T[] parameters);
    }
}
