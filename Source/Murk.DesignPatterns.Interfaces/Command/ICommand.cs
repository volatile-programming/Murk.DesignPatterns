namespace Murk.DesignPatterns.Interfaces.Command
{
    /// <summary>
    /// A interface that represents a simple command.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// The main command concern to be executed when needed.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        void Execute(object parameter);
    }

    /// <summary>
    /// A generic interface that represents a simple command.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface ICommand<in T>
    {
        /// <summary>
        /// The main command concern to be executed when needed.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        void Execute(T parameter);
    }
}
