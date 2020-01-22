using Murk.Common;
using System.Threading.Tasks;

namespace Murk.Command
{
	/// <summary>
	/// Base class for async commands.
	/// </summary>
	public abstract class BaseCommandAsync :
		BaseDisposable,
		ICommandAsync
	{
		/// <inheritdoc/>
		public abstract Task ExecuteAsync(object parameter);
	}

	/// <summary>
	/// Base class for generic async commands.
	/// </summary>
	/// <typeparam name="T">Parameter type.</typeparam>
	public abstract class BaseCommandAsync<T> :
		BaseDisposable,
		ICommandAsync<T>
	{
		/// <inheritdoc/>
		public abstract Task ExecuteAsync(T parameter);
	}
}
