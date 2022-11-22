using Murk.DesignPatterns.Interfaces.Command;

namespace Murk.DesignPatterns.BaseClasses.Command
{
    /// <summary>
    /// Base class for reversible and disposable commands.
    /// Implements <see cref="System.Windows.Input.ICommand"/>.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public abstract class BaseCommandReversibleAndDisableAble<T> :
        BaseCommandDisableAble<T>,
        ICommandReversibleAndDisableAble<T>
    {
        /// <inheritdoc/>
        public abstract void Reverse(T parameter);
    }
}
