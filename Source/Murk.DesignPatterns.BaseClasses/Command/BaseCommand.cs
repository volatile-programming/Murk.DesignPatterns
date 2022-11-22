using Murk.Common;
using Murk.DesignPatterns.Interfaces.Command;

namespace Murk.DesignPatterns.BaseClasses.Command
{
    /// <summary>
    /// Base class for commands.
    /// </summary>
    public abstract class BaseCommand :
        BaseDisposable,
        ICommand
    {
        /// <inheritdoc/>
        public abstract void Execute(object parameter);
    }

    /// <summary>
    /// Base class for generic commands.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public abstract class BaseCommand<T> :
        BaseDisposable,
        ICommand<T>
    {
        /// <inheritdoc/>
        public abstract void Execute(T parameter);
    }
}
