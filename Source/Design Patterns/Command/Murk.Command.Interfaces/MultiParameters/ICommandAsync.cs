using System.Threading.Tasks;

namespace Murk.Command.MultiParameters
{
	/// <summary>
	/// A generic interface that represents a async Command.
	/// </summary>
	/// <typeparam name="T">Parameter type.</typeparam>
	public interface ICommandAsync : System.IDisposable
	{
		/// <summary>
		/// The main command concern to be executed when needed.
		/// </summary>
		/// <param name="parameter">Command parameter.</param>
		Task ExecuteAsync(params object[] parameter);
	}

	/// <summary>
	/// A generic interface that represents a async
	/// multi parameter command.
	/// </summary>
	/// <typeparam name="T">Parameters type.</typeparam>
	public interface ICommandAsync<in T> : System.IDisposable
	{
		/// <summary>
		/// The main command concern to be executed when needed.
		/// </summary>
		/// <param name="parameters">Command parameters.</param>
		Task ExecuteAsync(params T[] parameters);
	}
}
