namespace Murk.Command.MultiParameters
{
	/// <summary>
	/// A interface that represents a disable able command.
	/// </summary>
	public interface ICommandDisableAble :
		System.Windows.Input.ICommand,
		ICommand
	{
		/// <summary>
		/// Determines whether or not the command can be executed.
		/// </summary>
		/// <param name="parameters">Command parameter.</param>
		/// <returns>True/False</returns>
		bool CanExecute(params object[] parameters);
	}

	/// <summary>
	/// A generic interface that represents a disable able command.
	/// </summary>
	/// <typeparam name="T">Parameter type.</typeparam>
	public interface ICommandDisableAble<in T> :
		System.Windows.Input.ICommand,
		ICommand<T>
	{
		/// <summary>
		/// Determines whether or not the command can be executed.
		/// </summary>
		/// <param name="parameters">Command parameter.</param>
		/// <returns>True/False</returns>
		bool CanExecute(params T[] parameters);
	}
}
