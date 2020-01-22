using Murk.Common;
using System.Threading.Tasks;

namespace Murk.Command.Parameterless
{
	/// <summary>
	/// Base class for parameterless reversible async commands.
	/// </summary>
	public abstract class BaseCommandReversibleAsync :
		BaseDisposable,
		ICommandReversibleAsync
	{
		/// <inheritdoc/>
		public abstract Task ExecuteAsync();

		/// <inheritdoc/>
		public abstract Task ReverseAsync();
	}
}
