namespace Murk.DesignPatterns.Interfaces.Command
{
    /// <summary>
    /// A generic interface that represents a async reversible
    /// and disable able Command.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface ICommandReversibleAndDisableAbleAsync<in T> :
        ICommandDisableAbleAsync<T>,
        ICommandReversibleAsync<T>
    { }
}
