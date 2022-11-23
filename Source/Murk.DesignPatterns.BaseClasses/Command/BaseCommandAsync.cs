using System;
using System.Threading.Tasks;
using Murk.Common.BaseClasses;
using Murk.DesignPatterns.Interfaces.Command;

namespace Murk.DesignPatterns.BaseClasses.Command
{
    /// <summary>
    /// Base class for parameterless async commands.
    /// </summary>
    public abstract class BaseCommandAsync : BaseDisposable, ICommandAsync
    {
        /// <inheritdoc/>
        public abstract Task ExecuteAsync();
    }

    /// <summary>
    /// Base class for disableable parameterless async commands.
    /// </summary>
    public abstract class BaseCommandDisableAbleAsync : BaseCommandAsync, IDisableableAsync
    {
        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged;

        /// <inheritdoc/>
        public abstract Task<bool> CanExecuteAsync();

        #region Interface Methods

        /// <summary>
        /// Notify to all subscribers that the command state has changed when needed.
        /// </summary>
        public async Task RiseCanExecuteChangedAsync()
        {
            CanExecuteChanged?.Invoke(this, System.EventArgs.Empty);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            if (isDisposing)
                CanExecuteChanged = null;
        }

        #endregion
    }

    /// <summary>
    /// Base class for async reversible parameterless commands.
    /// </summary>
    public abstract class BaseCommandReversibleAsync : BaseCommandAsync, IReversibleAsync
    {
        /// <inheritdoc/>
        public abstract Task ReverseAsync();
    }

    /// <summary>
    /// Base class for reversible and disposable parameterless async commands.
    /// </summary>
    public abstract class BaseCommandReversibleAndDisableAbleAsync : BaseCommandDisableAbleAsync, IReversibleAsync
    {
        /// <inheritdoc/>
        public abstract Task ReverseAsync();
    }

    /// <summary>
    /// Base class for generic async commands.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public abstract class BaseCommandAsync<T> : BaseDisposable, ICommandAsync<T>
    {
        /// <inheritdoc/>
        public abstract Task ExecuteAsync(T parameter);
    }

    /// <summary>
    /// Base class for generic disable able async commands.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public abstract class BaseCommandDisableAbleAsync<T> : BaseCommandAsync<T>, IDisableableAsync<T>
    {
        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged;

        /// <inheritdoc/>
        public abstract Task<bool> CanExecuteAsync(T parameter);

        #region Interface Methods

        /// <summary>
        /// Notify to all subscribers that the command state has changed when needed.
        /// </summary>
        public async Task RiseCanExecuteChangedAsync()
        {
            CanExecuteChanged?.Invoke(this, System.EventArgs.Empty);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            if (isDisposing)
                CanExecuteChanged = null;
        }

        #endregion
    }

    /// <summary>
    /// Base class for async reversible generic commands.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public abstract class BaseCommandReversibleAsync<T> : BaseCommandAsync<T>, IReversibleAsync<T>
    {
        /// <inheritdoc/>
        public abstract Task ReverseAsync(T parameter);
    }

    /// <summary>
    /// Base class for reversible and disposable async commands.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public abstract class BaseCommandReversibleAndDisableAbleAsync<T> : BaseCommandDisableAbleAsync<T>, IReversibleAsync<T>
    {
        /// <inheritdoc/>
        public abstract Task ReverseAsync(T parameter);
    }
}
