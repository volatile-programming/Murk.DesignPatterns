using System;
using System.Threading.Tasks;

namespace Murk.DesignPatterns.Interfaces.Command
{
    /// <summary>
    /// A generic interface that represents a disable able command.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface IDisableable
    {
        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        event EventHandler CanExecuteChanged;

        /// <summary>
        /// Determines whether or not the command can be executed.
        /// </summary>
        /// <returns>A <see cref="bool"/>.</returns>
        bool CanExecute();
    }

    /// <summary>
    /// A generic interface that represents a disable able command.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface IDisableable<in T>
    {
        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        event EventHandler CanExecuteChanged;

        /// <summary>
        /// Determines whether or not the command can be executed.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        /// <returns>A <see cref="bool"/>.</returns>
        bool CanExecute(T parameter);
    }

    /// <summary>
    /// A generic interface that represents a async disable able command.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface IDisableableAsync
    {
        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        event EventHandler CanExecuteChanged;

        /// <summary>
        /// Determines whether or not the command can be executed.
        /// </summary>
        /// <returns>A <see cref="Task"/> of <see cref="bool"/>.</returns>
        Task<bool> CanExecuteAsync();
    }

    /// <summary>
    /// A generic interface that represents a async disable able command.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface IDisableableAsync<in T>
    {
        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        event EventHandler CanExecuteChanged;

        /// <summary>
        /// Determines whether or not the command can be executed.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        /// <returns>A <see cref="Task"/> of <see cref="bool"/>.</returns>
        Task<bool> CanExecuteAsync(T parameter);
    }
}
