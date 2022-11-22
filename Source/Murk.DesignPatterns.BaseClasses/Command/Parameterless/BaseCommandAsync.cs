using Murk.Common;
using Murk.DesignPatterns.Interfaces.Command.Parameterless;
using System.Threading.Tasks;

namespace Murk.DesignPatterns.BaseClasses.Command.Parameterless
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
