using System.Threading.Tasks;

namespace Murk.Command
{
	/// <summary>
	/// Base class for reversible and disposable async commands.
	/// Implements <see cref="System.Windows.Input.ICommand"/>.
	/// </summary>
	public abstract class BaseCommandReversibleAndDisableAbleAsync :
		BaseCommandDisableAbleAsync,
		ICommandReversibleAndDisableAbleAsync
	{
		/// <inheritdoc/>
		public abstract Task ReverseAsync(object parameter);
	}

	/// <summary>
	/// Base class for reversible and disposable async commands.
	/// Implements <see cref="System.Windows.Input.ICommand"/>.
	/// </summary>
	/// <typeparam name="T">Parameter type.</typeparam>
	public abstract class BaseCommandReversibleAndDisableAbleAsync<T> :
		BaseCommandDisableAbleAsync<T>,
		ICommandReversibleAndDisableAbleAsync<T>
	{
		/// <inheritdoc/>
		public abstract Task ReverseAsync(T parameter);
	}
}
