using Murk.DesignPatterns.Interfaces.Command.MultiParameters;

namespace Murk.DesignPatterns.BaseClasses.Command.MultiParameters
{
    /// <summary>
    /// Base class for reversible and disposable commands.
    /// Implements <see cref="System.Windows.Input.ICommand"/>.
    /// </summary>
    public abstract class BaseCommandReversibleAndDisableAble :
        BaseCommandDisableAble, ICommandReversibleAndDisableAble
    {
        /// <inheritdoc/>
        public abstract void Reverse(params object[] parameter);
    }

    /// <summary>
    /// Base class for reversible and disposable commands.
    /// Implements <see cref="System.Windows.Input.ICommand"/>.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public abstract class BaseCommandReversibleAndDisableAble<T> :
        BaseCommandDisableAble<T>, ICommandReversibleAndDisableAble<T>
    {
        /// <inheritdoc/>
        public abstract void Reverse(params T[] parameter);
    }
}
