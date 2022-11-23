using System.Threading.Tasks;

namespace Murk.DesignPatterns.Interfaces.Command
{
    /// <summary>
    /// A generic interface that represents a reversible Command.
    /// </summary>
    public interface IReversible
    {
        /// <summary>
        /// The undo command operation.
        /// </summary>
        void Reverse();
    }

    /// <summary>
    /// A generic interface that represents a reversible Command.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface IReversible<in T>
    {
        /// <summary>
        /// The undo command operation.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        void Reverse(T parameter);
    }

    /// <summary>
    /// A generic interface that represents a async reversible Command.
    /// </summary>
    public interface IReversibleAsync
    {
        /// <summary>
        /// The undo command operation when needed.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        Task ReverseAsync();
    }

    /// <summary>
    /// A generic interface that represents a async reversible Command.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface IReversibleAsync<in T>
    {
        /// <summary>
        /// The undo command operation when needed.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        Task ReverseAsync(T parameter);
    }
}
