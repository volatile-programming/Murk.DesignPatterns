namespace Murk.Command.Parameterless
{
	/// <summary>
	/// A interface that represents a async reversible
	/// and disable able Command.
	/// </summary>
	public interface ICommandReversibleAndDisableAbleAsync :
		ICommandDisableAbleAsync,
		ICommandReversibleAsync
	{ }
}
