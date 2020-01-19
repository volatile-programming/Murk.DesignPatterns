namespace Murk.Command
{
	/// <summary>
	/// A interface that represents a async reversible
	/// and disable able Command.
	/// </summary>
	public interface ICommandReversibleAndDisableAbleAsync :
		ICommandDisableAbleAsync,
		ICommandReversibleAsync {}

	/// <summary>
	/// A generic interface that represents a async reversible
	/// and disable able Command.
	/// </summary>
	/// <typeparam name="T">Parameter type.</typeparam>
	public interface ICommandReversibleAndDisableAbleAsync<in T> :
		ICommandDisableAbleAsync<T>,
		ICommandReversibleAsync<T> {}
}
