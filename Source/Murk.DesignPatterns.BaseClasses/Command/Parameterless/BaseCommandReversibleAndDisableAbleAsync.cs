using System.Threading.Tasks;
using Murk.DesignPatterns.Interfaces.Command.Parameterless;

namespace Murk.DesignPatterns.BaseClasses.Command.Parameterless
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
