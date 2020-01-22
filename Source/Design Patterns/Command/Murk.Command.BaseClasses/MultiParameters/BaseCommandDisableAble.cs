using Murk.Common;
using Murk.Common;
using System;
using System.Collections;

namespace Murk.Command.MultiParameters
{
	/// <summary>
	/// Base class for disposable multi parameter commands.
	/// Implements <see cref="System.Windows.Input.ICommand" />.
	/// </summary>
	public abstract class BaseCommandDisableAble :
		BaseInputCommand,
		ICommandDisableAble
	{
		/// <inheritdoc/>
		public abstract bool CanExecute(object[] parameters);

		/// <inheritdoc/>
		public abstract void Execute(object[] parameters);

		#region Interface Methods
		/// <inheritdoc/>
		/// <exception cref="ArgumentNullException" />
		/// <exception cref="InvalidCastException" />
		/// <exception cref="ArgumentException" />
		public override bool CanExecute(object parameter)
		{
			Guard.Against.Null(parameter, nameof(parameter));
			var typedParameter = Cast.Type.Any<IEnumerable>(parameter);
			Guard.Against.NullOrEmpty(typedParameter,
				nameof(typedParameter));

			if (IsDisposing)
				return false;

			return CanExecute(typedParameter as object[]);
		}

		/// <inheritdoc/>
		/// <exception cref="ArgumentNullException" />
		/// <exception cref="InvalidCastException" />
		/// <exception cref="ArgumentException" />
		public override void Execute(object parameter)
		{
			Guard.Against.Null(parameter, nameof(parameter));
			var typedParameter = Cast.Type.Any<IEnumerable>(parameter);
			Guard.Against.NullOrEmpty(typedParameter,
				nameof(typedParameter));

			if (IsDisposing || !CanExecute(typedParameter))
				return;

			Execute(typedParameter as object[]);
		}
		#endregion
	}

	/// <summary>
	/// Base class for generic multi parameter commands.
	/// Implements <see cref="System.Windows.Input.ICommand" />.
	/// </summary>
	/// <typeparam name="T">Parameter type.</typeparam>
	public abstract class BaseCommandDisableAble<T> :
		BaseInputCommand,
		ICommandDisableAble<T>
	{
		/// <inheritdoc/>
		public abstract bool CanExecute(params T[] parameters);

		/// <inheritdoc/>
		public abstract void Execute(params T[] parameters);

		#region Interface Methods
		/// <inheritdoc/>
		/// <exception cref="ArgumentNullException" />
		/// <exception cref="InvalidCastException" />
		/// <exception cref="ArgumentException" />
		public override bool CanExecute(object parameter)
		{
			Guard.Against.Null(parameter, nameof(parameter));
			T[] typedParameter = Cast.Type.Any<T[]>(parameter);

			if (IsDisposing)
				return false;

			return CanExecute(typedParameter);
		}

		/// <inheritdoc/>
		/// <exception cref="ArgumentNullException" />
		/// <exception cref="InvalidCastException" />
		/// <exception cref="ArgumentException" />
		public override void Execute(object parameter)
		{
			Guard.Against.Null(parameter, nameof(parameter));
			T[] typedParameter = Cast.Type.Any<T[]>(parameter);

			if (IsDisposing || !CanExecute(parameter))
				return;

			Execute(typedParameter);
		}
		#endregion
	}
}
