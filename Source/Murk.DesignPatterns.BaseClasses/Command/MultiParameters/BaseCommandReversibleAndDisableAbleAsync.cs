using System.Threading.Tasks;
using Murk.DesignPatterns.Interfaces.Command.MultiParameters;

namespace Murk.DesignPatterns.BaseClasses.Command.MultiParameters
{
    /// <summary>
    /// Base class for reversible and disposable async commands.
    /// Implements <see cref="System.Windows.Input.ICommand"/>.
    /// </summary>
    public abstract class
        BaseCommandReversibleAndDisableAbleAsync :
        BaseCommandDisableAbleAsync,
        ICommandReversibleAndDisableAbleAsync
    {
        /// <inheritdoc/>
        public abstract Task ReverseAsync(params object[] parameter);
    }

    /// <summary>
    /// Base class for reversible and disposable async commands.
    /// Implements <see cref="System.Windows.Input.ICommand"/>.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public abstract class
        BaseCommandReversibleAndDisableAbleAsync<T> :
        BaseCommandDisableAbleAsync<T>,
        ICommandReversibleAndDisableAbleAsync<T>
    {
        /// <inheritdoc/>
        public abstract Task ReverseAsync(params T[] parameter);
    }
}
