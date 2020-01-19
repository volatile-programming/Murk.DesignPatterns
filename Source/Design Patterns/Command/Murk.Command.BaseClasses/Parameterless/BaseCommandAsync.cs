using Murk.Commons;
using System.Threading.Tasks;

namespace Murk.Command.Parameterless
{
	/// <summary>
	/// Base class for parameterless async commands.
	/// </summary>
	public abstract class BaseCommandAsync :
		BaseDisposable,
		ICommandAsync
	{
		/// <inheritdoc/>
		public abstract Task ExecuteAsync();
	}
}
