using Murk.Common;
using Murk.Common;
using System;

namespace Murk.Command
{
	/// <summary>
	/// Base class for disable able commands.
	/// Implements <see cref="System.Windows.Input.ICommand" />.
	/// </summary>
	public abstract class BaseCommandDisableAble :
		BaseInputCommand,
		ICommandDisableAble {}

	/// <summary>
	/// Base class for disable able generic commands.
	/// Implements <see cref="System.Windows.Input.ICommand" />.
	/// </summary>
	/// <typeparam name="T">Parameter type.</typeparam>
	public abstract class BaseCommandDisableAble<T> :
		BaseInputCommand,
		ICommandDisableAble<T>
	{
		/// <inheritdoc/>
		public abstract bool CanExecute(T parameter);

		/// <inheritdoc/>
		public abstract void Execute(T parameter);

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

			return CanExecute(typedParameter);
		}

		/// <inheritdoc/>
		/// <exception cref="ArgumentNullException" />
		/// <exception cref="InvalidCastException" />
		public override void Execute(object parameter)
		{
			Guard.Against.Null(parameter, nameof(parameter));
			T typedParameter = Cast.Type.Any<T>(parameter);

			if (IsDisposing || !CanExecute(typedParameter))
				return;

			Execute(typedParameter);
		}
		#endregion
	}
}
