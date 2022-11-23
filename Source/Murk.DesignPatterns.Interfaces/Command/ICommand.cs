using System.Threading.Tasks;

namespace Murk.DesignPatterns.Interfaces.Command
{
    /// <summary>
    /// A interface that represents a simple Command.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// The main command concern to be executed when needed.
        /// </summary>
        void Execute();
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

    /// <summary>
    /// A generic interface that represents a multi parameter command.
    /// </summary>
    /// <typeparam name="T1">Parameters type.</typeparam>
    /// <typeparam name="T2">Parameters type.</typeparam>
    public interface ICommand<in T1, in T2>
    {
        /// <summary>
        /// The main command concern to be executed when needed.
        /// </summary>
        /// <param name="arg1">Command parameters of type: <typeparamref name="T1"/>.</param>
        /// <param name="arg2">Command parameters of type: <typeparamref name="T2"/>.</param>
        void Execute(T1 arg1, T2 arg2);
    }

    /// <summary>
    /// A generic interface that represents a multi parameter command.
    /// </summary>
    /// <typeparam name="T1">Parameters type.</typeparam>
    /// <typeparam name="T2">Parameters type.</typeparam>
    /// <typeparam name="T3">Parameters type.</typeparam>
    public interface ICommand<in T1, in T2, in T3>
    {
        /// <summary>
        /// The main command concern to be executed when needed.
        /// </summary>
        /// <param name="arg1">Command parameters of type: <typeparamref name="T1"/>.</param>
        /// <param name="arg2">Command parameters of type: <typeparamref name="T2"/>.</param>
        /// <param name="arg3">Command parameters of type: <typeparamref name="T3"/>.</param>
        void Execute(T1 arg1, T2 arg2, T3 arg3);
    }

    /// <summary>
    /// A generic interface that represents a async Command.
    /// </summary>
    public interface ICommandAsync
    {
        /// <summary>
        /// The main command concern to be executed when needed.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        Task ExecuteAsync();
    }

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
        /// <returns>A <see cref="Task"/>.</returns>
        Task ExecuteAsync(T parameter);
    }

    /// <summary>
    /// A generic interface that represents a multi parameter async command.
    /// </summary>
    /// <typeparam name="T1">Parameters type.</typeparam>
    /// <typeparam name="T2">Parameters type.</typeparam>
    public interface ICommandAsync<in T1, in T2>
    {
        /// <summary>
        /// The main command concern to be executed when needed.
        /// </summary>
        /// <param name="arg1">Command parameters of type: <typeparamref name="T1"/>.</param>
        /// <param name="arg2">Command parameters of type: <typeparamref name="T2"/>.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        Task ExecuteAsync(T1 arg1, T2 arg2);
    }

    /// <summary>
    /// A generic interface that represents a multi parameter async command.
    /// </summary>
    /// <typeparam name="T1">Parameters type.</typeparam>
    /// <typeparam name="T2">Parameters type.</typeparam>
    /// <typeparam name="T3">Parameters type.</typeparam>
    public interface ICommandAsync<in T1, in T2, in T3>
    {
        /// <summary>
        /// The main command concern to be executed when needed.
        /// </summary>
        /// <param name="arg1">Command parameters of type: <typeparamref name="T1"/>.</param>
        /// <param name="arg2">Command parameters of type: <typeparamref name="T2"/>.</param>
        /// <param name="arg3">Command parameters of type: <typeparamref name="T3"/>.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        Task ExecuteAsync(T1 arg1, T2 arg2, T3 arg3);
    }
}
