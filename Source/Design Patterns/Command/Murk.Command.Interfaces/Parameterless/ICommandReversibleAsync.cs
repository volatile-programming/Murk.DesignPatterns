using System.Threading.Tasks;

namespace Murk.Command.Parameterless
{
	/// <summary>
	/// A interface that represents a async reversible command.
	/// </summary>
	public interface ICommandReversibleAsync : ICommandAsync
	{
		/// <summary>
		/// The undo command operation.
		/// </summary>
		Task ReverseAsync();
	}
}
