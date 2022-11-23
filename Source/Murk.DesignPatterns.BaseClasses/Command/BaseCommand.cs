using System;
using Murk.Common;
using Murk.DesignPatterns.Interfaces.Command;

namespace Murk.DesignPatterns.BaseClasses.Command
{
    /// <summary>
    /// Base class for parameterless commands.
    /// </summary>
    public abstract class BaseCommand : BaseDisposable, ICommand
    {
        /// <inheritdoc/>
        public abstract void Execute();
    }

    /// <summary>
    /// Base class for disableable parameterless commands.
    /// </summary>
    public abstract class BaseCommandDisableAble : BaseCommand, IDisableable
    {
        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged;

        /// <inheritdoc/>
        public abstract bool CanExecute();

        #region Interface Methods

        /// <summary>
        /// Notify to all subscribers that the command state has changed.
        /// </summary>
        public void RiseCanExecuteChanged()
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
    /// Base class for reversible parameterless commands.
    /// </summary>
    public abstract class BaseCommandReversible : BaseCommand, IReversible
    {
        /// <inheritdoc/>
        public abstract void Reverse();
    }

    /// <summary>
    /// Base class for reversible and disposable parameterless commands.
    /// </summary>
    public abstract class BaseCommandReversibleAndDisableAble : BaseCommandDisableAble, IReversible
    {
        /// <inheritdoc/>
        public abstract void Reverse();
    }

    /// <summary>
    /// Base class for generic commands.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public abstract class BaseCommand<T> : BaseDisposable, ICommand<T>
    {
        /// <inheritdoc/>
        public abstract void Execute(T parameter);
    }

    /// <summary>
    /// Base class for disable able generic commands.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public abstract class BaseCommandDisableAble<T> : BaseCommand<T>, IDisableable<T>
    {
        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged;

        /// <inheritdoc/>
        public abstract bool CanExecute(T parameter);

        #region Interface Methods

        /// <summary>
        /// Notify to all subscribers that the command state has changed.
        /// </summary>
        public void RiseCanExecuteChanged()
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
    /// Base class for reversible generic commands.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public abstract class BaseCommandReversible<T> : BaseCommand<T>, IReversible<T>
    {
        /// <inheritdoc/>
        public abstract void Reverse(T parameter);
    }

    /// <summary>
    /// Base class for reversible and disposable commands.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public abstract class BaseCommandReversibleAndDisableAble<T> : BaseCommandDisableAble<T>, IReversible<T>
    {
        /// <inheritdoc/>
        public abstract void Reverse(T parameter);
    }
}
