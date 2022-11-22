using Murk.Common;
using Murk.DesignPatterns.Interfaces.Command;

namespace Murk.DesignPatterns.BaseClasses.Command
{
    /// <summary>
    /// Base class for reversible generic commands.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public abstract class BaseCommandReversible<T> :
        BaseDisposable,
        ICommandReversible<T>
    {
        /// <inheritdoc/>
        public abstract void Execute(T parameter);

        /// <inheritdoc/>
        public abstract void Reverse(T parameter);
    }
}
