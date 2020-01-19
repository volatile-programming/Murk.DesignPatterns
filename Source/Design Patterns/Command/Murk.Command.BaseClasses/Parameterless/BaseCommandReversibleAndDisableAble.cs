namespace Murk.Command.Parameterless
{
	/// <summary>
	/// Base class for reversible and disposable commands.
	/// Implements <see cref="System.Windows.Input.ICommand"/>.
	/// </summary>
	public abstract class BaseCommandReversibleAndDisableAble :
		BaseCommandDisableAble, ICommandReversibleAndDisableAble
	{
		/// <inheritdoc/>
		public abstract void Reverse();
	}
}
