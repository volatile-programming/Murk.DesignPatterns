namespace Murk.DesignPatterns.Interfaces.Command.Parameterless
{
    /// <summary>
    /// A interface that represents a simple Command.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// The main command concern to be executed when needed.
        /// </summary>
        void Execute();
    }
}
