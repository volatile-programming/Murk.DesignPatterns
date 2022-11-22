using Murk.Common;
using Murk.DesignPatterns.Interfaces.Command.MultiParameters;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace Murk.DesignPatterns.BaseClasses.Command.MultiParameters
{
    /// <summary>
    /// Base class for async commands.
    /// Implements <see cref="System.Windows.Input.ICommand" />.
    /// </summary>
    public abstract class BaseCommandDisableAbleAsync :
        BaseInputCommand,
        ICommandDisableAbleAsync
    {
        /// <inheritdoc/>
        public abstract Task<bool> CanExecuteAsync(
            params object[] parameters);

        /// <inheritdoc/>
        public abstract Task ExecuteAsync(params object[] parameters);

        #region Interface Methods
        /// <inheritdoc/>
        /// <exception cref="ArgumentException" />
        /// <exception cref="InvalidCastException" />
        /// <exception cref="ArgumentNullException" />
        public override bool CanExecute(object parameter)
        {
            Guard.Against.Null(parameter, nameof(parameter));
            var typedParameter = Cast.Type.Any<IEnumerable>(parameter);
            Guard.Against.NullOrEmpty(typedParameter,
                nameof(typedParameter));

            if (IsDisposing)
                return false;

            return CanExecuteAsync(typedParameter).Result;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException" />
        /// <exception cref="InvalidCastException" />
        /// <exception cref="ArgumentNullException" />
        public override void Execute(object parameter)
        {
            Guard.Against.Null(parameter, nameof(parameter));
            var typedParameter = Cast.Type.Any<IEnumerable>(parameter);
            Guard.Against.NullOrEmpty(typedParameter,
                nameof(typedParameter));

            if (IsDisposing || !CanExecuteAsync(typedParameter).Result)
                return;

            ExecuteAsync(typedParameter).Wait();
        }
        #endregion
    }

    /// <summary>
    /// Base class for async generic commands.
    /// Implements <see cref="System.Windows.Input.ICommand" />.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public abstract class BaseCommandDisableAbleAsync<T> :
        BaseInputCommand,
        ICommandDisableAbleAsync<T>
    {
        /// <inheritdoc/>
        public abstract Task<bool> CanExecuteAsync(params T[] parameters);

        /// <inheritdoc/>
        public abstract Task ExecuteAsync(params T[] parameters);

        #region Interface Methods
        /// <inheritdoc/>
        /// <exception cref="ArgumentException" />
        /// <exception cref="InvalidCastException" />
        /// <exception cref="ArgumentNullException" />
        public override bool CanExecute(object parameter)
        {
            Guard.Against.Null(parameter, nameof(parameter));
            T[] typedParameter = Cast.Type.Any<T[]>(parameter);

            if (IsDisposing)
                return false;

            return CanExecuteAsync(typedParameter).Result;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException" />
        /// <exception cref="InvalidCastException" />
        /// <exception cref="ArgumentNullException" />
        public override void Execute(object parameter)
        {
            Guard.Against.Null(parameter, nameof(parameter));
            T[] typedParameter = Cast.Type.Any<T[]>(parameter);

            if (IsDisposing || !CanExecuteAsync(typedParameter).Result)
                return;

            ExecuteAsync(typedParameter).Wait();
        }
        #endregion
    }
}
