using Murk.Common;

namespace Murk.Command.Parameterless
{
	/// <summary>
	/// Base class for reversible commands.
	/// </summary>
	public abstract class BaseCommandReversible :
		BaseDisposable,
		ICommandReversible
	{
		/// <inheritdoc/>
		public abstract void Execute();

		/// <inheritdoc/>
		public abstract void Reverse();
	}
}
