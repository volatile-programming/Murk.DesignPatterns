using System.Threading.Tasks;

namespace Murk.Command.Parameterless
{
	/// <summary>
	/// Base class for parameterless reversible and disable able
	/// async commands.
	/// Implements <see cref="System.Windows.Input.ICommand"/>.
	/// </summary>
	public abstract class
		BaseCommandReversibleAndDisableAbleAsync :
		BaseCommandDisableAbleAsync,
		ICommandReversibleAndDisableAbleAsync
	{
		/// <inheritdoc/>
		public abstract Task ReverseAsync();
	}
}
