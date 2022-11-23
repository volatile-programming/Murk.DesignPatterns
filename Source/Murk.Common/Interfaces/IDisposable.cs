namespace Murk.Common.Interfaces
{
    public interface IDisposable : System.IDisposable
    {
		bool IsDisposing { get; }
    }
}
