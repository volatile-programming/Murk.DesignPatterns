namespace Murk.Command.MultiParameters
{
	/// <summary>
	/// A interface that represents a multi parameter command.
	/// </summary>
	public interface ICommand : System.IDisposable
	{
		/// <summary>
		/// The main command concern to be executed when needed.
		/// </summary>
		/// <param name="parameters">Command parameters.</param>
		void Execute(params object[] parameters);
	}

	/// <summary>
	/// A generic interface that represents a multi parameter command.
	/// </summary>
	/// <typeparam name="T">Parameters type.</typeparam>
	public interface ICommand<in T> : System.IDisposable
	{
		/// <summary>
		/// The main command concern to be executed when needed.
		/// </summary>
		/// <param name="parameters">Command parameters.</param>
		void Execute(params T[] parameters);
	}
}
