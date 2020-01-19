using Murk.Commons;
using System;
using System.Threading.Tasks;

namespace Murk.Command
{
	/// <summary>
	/// Base class for disable able async commands.
	/// Implements <see cref="System.Windows.Input.ICommand" />.
	/// </summary>
	public abstract class BaseCommandDisableAbleAsync :
		BaseInputCommand,
		ICommandDisableAbleAsync
	{
		/// <inheritdoc/>
		public abstract Task<bool> CanExecuteAsync(object parameter);

		/// <inheritdoc/>
		public abstract Task ExecuteAsync(object parameter);

		#region Interface Methods
		/// <inheritdoc/>
		/// <exception cref="ArgumentNullException" />
		/// <exception cref="InvalidCastException" />
		public override bool CanExecute(object parameter)
		{
			Guard.Against.Null(parameter, nameof(parameter));

			if (IsDisposing)
				return false;

			return CanExecuteAsync(parameter).Result;
		}

		/// <inheritdoc/>
		/// <exception cref="ArgumentNullException" />
		/// <exception cref="InvalidCastException" />
		public override void Execute(object parameter)
		{
			Guard.Against.Null(parameter, nameof(parameter));

			if (IsDisposing || !CanExecuteAsync(parameter).Result)
				return;

			ExecuteAsync(parameter).Wait();
		}
		#endregion
	}

	/// <summary>
	/// Base class for generic disable able async commands.
	/// Implements <see cref="System.Windows.Input.ICommand" />.
	/// </summary>
	/// <typeparam name="T">Parameter type.</typeparam>
	public abstract class BaseCommandDisableAbleAsync<T> :
		BaseInputCommand,
		ICommandDisableAbleAsync<T>
	{
		/// <inheritdoc/>
		public abstract Task<bool> CanExecuteAsync(T parameter);

		/// <inheritdoc/>
		public abstract Task ExecuteAsync(T parameter);

		#region Interface Methods
		/// <inheritdoc/>
		/// <exception cref="ArgumentNullException" />
		/// <exception cref="InvalidCastException" />
		public override bool CanExecute(object parameter)
		{
			Guard.Against.Null(parameter, nameof(parameter));
			T typedParameter = Cast.Type.Any<T>(parameter);

			if (IsDisposing)
				return false;

			return CanExecuteAsync(typedParameter).Result;
		}

		/// <inheritdoc/>
		/// <exception cref="ArgumentNullException" />
		/// <exception cref="InvalidCastException" />
		public override void Execute(object parameter)
		{
			Guard.Against.Null(parameter, nameof(parameter));
			T typedParameter = Cast.Type.Any<T>(parameter);

			if (IsDisposing || !CanExecuteAsync(typedParameter).Result)
				return;

			ExecuteAsync(typedParameter).Wait();
		}
		#endregion
	}
}
