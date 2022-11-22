﻿using System.Threading.Tasks;

namespace Murk.DesignPatterns.Interfaces.Command
{
    /// <summary>
    /// A interface that represents an async Command.
    /// </summary>
    public interface ICommandAsync
    {
        /// <summary>
        /// The main command concern to be executed when needed.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        Task ExecuteAsync(object parameter);
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
        Task ExecuteAsync(T parameter);
    }
}
