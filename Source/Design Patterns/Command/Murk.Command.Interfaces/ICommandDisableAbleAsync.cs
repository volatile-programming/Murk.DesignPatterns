using System.Threading.Tasks;

namespace Murk.Command
{
	/// <summary>
	/// A generic interface that represents a async
	/// disable able command.
	/// </summary>
	public interface ICommandDisableAbleAsync :
		System.Windows.Input.ICommand,
		ICommandAsync
	{
		/// <summary>
		/// Determines whether or not the command can be executed.
		/// </summary>
		/// <param name="parameter">Command parameter.</param>
		/// <returns>True/False</returns>
		Task<bool> CanExecuteAsync(object parameter);
	}

	/// <summary>
	/// A generic interface that represents a async
	/// disable able command.
	/// </summary>
	/// <typeparam name="T">Parameter type.</typeparam>
	public interface ICommandDisableAbleAsync<in T> :
		System.Windows.Input.ICommand,
		ICommandAsync<T>
	{
		/// <summary>
		/// Determines whether or not the command can be executed.
		/// </summary>
		/// <param name="parameter">Command parameter.</param>
		/// <returns>True/False</returns>
		Task<bool> CanExecuteAsync(T parameter);
	}
}
