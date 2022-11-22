namespace Murk.DesignPatterns.Interfaces.Command
{
    /// <summary>
    /// A interface that represents a disable able command.
    /// Implements <see cref="System.Windows.Input.ICommand"/>
    /// </summary>
    public interface ICommandDisableAble :
        System.Windows.Input.ICommand
    { }

    /// <summary>
    /// A generic interface that represents a disable able command.
    /// Implements <see cref="System.Windows.Input.ICommand"/>
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface ICommandDisableAble<in T> :
        System.Windows.Input.ICommand,
        ICommand<T>
    {
        /// <summary>
        /// Determines whether or not the command can be executed.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        /// <returns>True/False</returns>
        bool CanExecute(T parameter);
    }
}
