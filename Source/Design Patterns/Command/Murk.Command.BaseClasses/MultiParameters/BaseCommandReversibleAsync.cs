using Murk.Commons;
using System.Threading.Tasks;

namespace Murk.Command.MultiParameters
{
	/// <summary>
	/// Base class for async reversible commands.
	/// </summary>
	public abstract class BaseCommandReversibleAsync :
		BaseDisposable,
		ICommandReversibleAsync
	{
		/// <inheritdoc/>
		public abstract Task ExecuteAsync(params object[] parameter);

		/// <inheritdoc/>
		public abstract Task ReverseAsync(params object[] parameter);
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
		public abstract Task ExecuteAsync(params T[] parameter);

		/// <inheritdoc/>
		public abstract Task ReverseAsync(params T[] parameter);
	}
}
