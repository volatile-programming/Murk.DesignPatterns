namespace Murk.Command.Parameterless
{
	/// <summary>
	/// A interface that represents a reversible Command.
	/// </summary>
	public interface ICommandReversible : ICommand
	{
		/// <summary>
		/// The undo command operation.
		/// </summary>
		void Reverse();
	}
}
