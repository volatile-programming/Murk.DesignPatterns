using System.Threading.Tasks;

namespace Murk.DesignPatterns.Interfaces.Command.Parameterless
{
    /// <summary>
    /// A generic interface that represents a async Command.
    /// </summary>
    public interface ICommandAsync
    {
        /// <summary>
        /// The main command concern to be executed when needed.
        /// </summary>
        Task ExecuteAsync();
    }
}
