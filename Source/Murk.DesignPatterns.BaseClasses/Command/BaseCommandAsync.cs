using Murk.Common;
using Murk.DesignPatterns.Interfaces.Command;
using System.Threading.Tasks;

namespace Murk.DesignPatterns.BaseClasses.Command
{
    /// <summary>
    /// Base class for generic async commands.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public abstract class BaseCommandAsync<T> :
        BaseDisposable,
        ICommandAsync<T>
    {
        /// <inheritdoc/>
        public abstract Task ExecuteAsync(T parameter);
    }
}
