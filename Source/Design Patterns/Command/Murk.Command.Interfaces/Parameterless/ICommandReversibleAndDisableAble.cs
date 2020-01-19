namespace Murk.Command.Parameterless
{
	/// <summary>
	/// A interface for Commands that can be disable and reverse.
	/// </summary>
	public interface ICommandReversibleAndDisableAble :
		ICommandDisableAble,
		ICommandReversible
	{ }
}
