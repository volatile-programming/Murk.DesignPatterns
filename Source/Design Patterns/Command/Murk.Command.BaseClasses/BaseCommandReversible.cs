using Murk.Common;

namespace Murk.Command
{
	/// <summary>
	/// Base class for reversible commands.
	/// </summary>
	public abstract class BaseCommandReversible :
		BaseDisposable,
		ICommandReversible
	{
		/// <inheritdoc/>
		public abstract void Execute(object parameter);

		/// <inheritdoc/>
		public abstract void Reverse(object parameter);
	}

	/// <summary>
	/// Base class for reversible generic commands.
	/// </summary>
	/// <typeparam name="T">Parameter type.</typeparam>
	public abstract class BaseCommandReversible<T> :
		BaseDisposable,
		ICommandReversible<T>
	{
		/// <inheritdoc/>
		public abstract void Execute(T parameter);

		/// <inheritdoc/>
		public abstract void Reverse(T parameter);
	}
}
