namespace Murk.Commons
{
	/// <summary>
	/// A interface that represents a disposable and
	/// disable able command.
	/// Implements <see cref="System.Windows.Input.ICommand"/>
	/// and <see cref="System.IDisposable"/>
	/// </summary>
	public interface IInputCommand :
		System.Windows.Input.ICommand,
		System.IDisposable
	{
		/// <summary>
		/// Notify to all subscribers that the command state has changed.
		/// </summary>
		void RiseCanExecuteChanged();
	}
}