namespace Murk.DesignPatterns.Interfaces.Command
{
    /// <summary>
    /// A generic interface that represents a reversible
    /// and disable able Command.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface ICommandReversibleAndDisableAble<in T> :
        ICommandDisableAble<T>,
        ICommandReversible<T>
    { }
}
