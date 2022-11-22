using Murk.Common;
using Murk.DesignPatterns.Interfaces.Command.Parameterless;
using System.Threading.Tasks;

namespace Murk.DesignPatterns.BaseClasses.Command.Parameterless
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
