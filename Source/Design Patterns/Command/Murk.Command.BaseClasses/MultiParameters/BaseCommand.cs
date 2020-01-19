using Murk.Commons;

namespace Murk.Command.MultiParameters
{
	/// <summary>
	/// Base class for disposable multi parameters command.
	/// </summary>
	public abstract class BaseCommand :
		BaseDisposable,
		ICommand
	{
		/// <inheritdoc/>
		public abstract void Execute(params object[] parameters);
	}

	/// <summary>
	/// Base class for generic multi parameters command.
	/// </summary>
	/// <typeparam name="T">Parameters type.</typeparam>
	public abstract class BaseCommand<T> :
		BaseDisposable,
		ICommand<T>
	{
		/// <inheritdoc/>
		public abstract void Execute(params T[] parameters);
	}
}
