namespace Murk.Command
{
	/// <summary>
	/// A interface that represents a reversible Command.
	/// </summary>
	public interface ICommandReversible : ICommand
	{
		/// <summary>
		/// The undo command operation.
		/// </summary>
		/// <param name="parameter">Command parameter.</param>
		void Reverse(object parameter);
	}

	/// <summary>
	/// A generic interface that represents a reversible Command.
	/// </summary>
	/// <typeparam name="T">Parameter type.</typeparam>
	public interface ICommandReversible<in T> : ICommand<T>
	{
		/// <summary>
		/// The undo command operation.
		/// </summary>
		/// <param name="parameter">Command parameter.</param>
		void Reverse(T parameter);
	}
}
