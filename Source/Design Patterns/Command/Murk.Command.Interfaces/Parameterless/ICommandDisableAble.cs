namespace Murk.Command.Parameterless
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
		/// <returns>True/False</returns>
		bool CanExecute();
	}
}
