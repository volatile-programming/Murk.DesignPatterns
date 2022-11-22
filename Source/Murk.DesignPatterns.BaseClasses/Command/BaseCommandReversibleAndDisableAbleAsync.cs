using System.Threading.Tasks;
using Murk.DesignPatterns.Interfaces.Command;

namespace Murk.DesignPatterns.BaseClasses.Command
{
    /// <summary>
    /// Base class for reversible and disposable async commands.
    /// Implements <see cref="System.Windows.Input.ICommand"/>.
    /// </summary>
    public abstract class BaseCommandReversibleAndDisableAbleAsync :
        BaseCommandDisableAbleAsync,
        ICommandReversibleAndDisableAbleAsync
    {
        /// <inheritdoc/>
        public abstract Task ReverseAsync(object parameter);
    }

    /// <summary>
    /// Base class for reversible and disposable async commands.
    /// Implements <see cref="System.Windows.Input.ICommand"/>.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public abstract class BaseCommandReversibleAndDisableAbleAsync<T> :
        BaseCommandDisableAbleAsync<T>,
        ICommandReversibleAndDisableAbleAsync<T>
    {
        /// <inheritdoc/>
        public abstract Task ReverseAsync(T parameter);
    }
}
