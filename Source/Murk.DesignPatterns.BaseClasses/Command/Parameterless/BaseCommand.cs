using Murk.Common;
using Murk.DesignPatterns.Interfaces.Command.Parameterless;

namespace Murk.DesignPatterns.BaseClasses.Command.Parameterless
{
    /// <summary>
    /// Base class for parameterless commands.
    /// </summary>
    public abstract class BaseCommand : BaseDisposable, ICommand
    {
        /// <inheritdoc/>
        public abstract void Execute();
    }
}
