namespace Murk.Commons
{
	/// <summary>
	/// Base class for disposable disable able commands.
	/// Implements <see cref="System.Windows.Input.ICommand" />.
	/// </summary>
	public abstract class BaseInputCommand :
		BaseDisposable,
		IInputCommand
	{
		/// <inheritdoc/>
		public event System.EventHandler CanExecuteChanged;

		/// <inheritdoc/>
		public abstract bool CanExecute(object parameter);

		/// <inheritdoc/>
		public abstract void Execute(object parameter);

		#region Interface Methods
		/// <inheritdoc/>
		public void RiseCanExecuteChanged() =>
			CanExecuteChanged?.Invoke(this, System.EventArgs.Empty);

		/// <inheritdoc/>
		protected override void Dispose(bool isDisposing)
		{
			base.Dispose(isDisposing);
			if (isDisposing)
				CanExecuteChanged = null;
		}
		#endregion
	}
}
