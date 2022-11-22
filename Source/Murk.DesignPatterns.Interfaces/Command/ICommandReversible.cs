namespace Murk.DesignPatterns.Interfaces.Command
{
    /// <summary>
    /// A generic interface that represents a reversible Command.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface ICommandReversible<in T> : ICommand<T>
    {
        /// <summary>
        /// The undo command operation.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        void Reverse(T parameter);
    }
}
