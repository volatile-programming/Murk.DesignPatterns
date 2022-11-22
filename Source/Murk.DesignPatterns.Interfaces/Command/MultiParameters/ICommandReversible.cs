namespace Murk.DesignPatterns.Interfaces.Command.MultiParameters
{
    /// <summary>
    /// A interface that represents a reversible Command.
    /// </summary>
    public interface ICommandReversible : ICommand
    {
        /// <summary>
        /// The undo command operation.
        /// </summary>
        /// <param name="parameters">Command parameter.</param>
        void Reverse(params object[] parameters);
    }

    /// <summary>
    /// A generic interface that represents a reversible Command.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface ICommandReversible<in T> : ICommand<T>
    {
        /// <summary>
        /// The undo command operation.
        /// </summary>
        /// <param name="parameters">Command parameter.</param>
        void Reverse(params T[] parameters);
    }
}
