using Murk.Common;
using Murk.DesignPatterns.Interfaces.Command.MultiParameters;

namespace Murk.DesignPatterns.BaseClasses.Command.MultiParameters
{
    /// <summary>
    /// Base class for reversible multi parameter commands.
    /// </summary>
    public abstract class BaseCommandReversible :
        BaseDisposable,
        ICommandReversible
    {
        /// <inheritdoc/>
        public abstract void Execute(params object[] parameter);

        /// <inheritdoc/>
        public abstract void Reverse(params object[] parameter);
    }

    /// <summary>
    /// Base class for reversible multi parameter generic commands.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public abstract class BaseCommandReversible<T> :
        BaseDisposable,
        ICommandReversible<T>
    {
        /// <inheritdoc/>
        public abstract void Execute(params T[] parameter);

        /// <inheritdoc/>
        public abstract void Reverse(params T[] parameter);
    }
}
