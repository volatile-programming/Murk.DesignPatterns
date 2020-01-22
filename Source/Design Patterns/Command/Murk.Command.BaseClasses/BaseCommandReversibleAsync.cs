using Murk.Common;
using System.Threading.Tasks;

namespace Murk.Command
{
	/// <summary>
	/// Base class for async reversible commands.
	/// </summary>
	public abstract class BaseCommandReversibleAsync :
		BaseDisposable,
		ICommandReversibleAsync
	{
		/// <inheritdoc/>
		public abstract Task ExecuteAsync(object parameter);

		/// <inheritdoc/>
		public abstract Task ReverseAsync(object parameter);
	}

	/// <summary>
	/// Base class for async reversible generic commands.
	/// </summary>
	/// <typeparam name="T">Parameter type.</typeparam>
	public abstract class BaseCommandReversibleAsync<T> :
		BaseDisposable,
		ICommandReversibleAsync<T>
	{
		/// <inheritdoc/>
		public abstract Task ExecuteAsync(T parameter);

		/// <inheritdoc/>
		public abstract Task ReverseAsync(T parameter);
	}
}
