using Murk.Common;
using System.Threading.Tasks;

namespace Murk.Command.MultiParameters
{
	/// <summary>
	/// Base class for async multi parameters command.
	/// </summary>
	public abstract class BaseCommandAsync :
		BaseDisposable,
		ICommandAsync
	{
		/// <inheritdoc/>
		public abstract Task ExecuteAsync(params object[] parameters);
	}

	/// <summary>
	/// Base class for async generic multi parameters command.
	/// </summary>
	/// <typeparam name="T">Parameters type.</typeparam>
	public abstract class BaseCommandAsync<T> :
		BaseDisposable,
		ICommandAsync<T>
	{
		/// <inheritdoc/>
		public abstract Task ExecuteAsync(params T[] parameters);
	}
}
