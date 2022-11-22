using Murk.Common;
using Murk.DesignPatterns.Interfaces.Command;
using System.Threading.Tasks;

namespace Murk.DesignPatterns.BaseClasses.Command
{
    /// <summary>
    /// Base class for async reversible generic commands.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public abstract class BaseCommandReversibleAsync<T> :
        BaseDisposable,
        ICommandReversibleAsync<T>
    {
        /// <inheritdoc/>
        public abstract Task ExecuteAsync(T parameter);

        /// <inheritdoc/>
        public abstract Task ReverseAsync(T parameter);
    }
}
