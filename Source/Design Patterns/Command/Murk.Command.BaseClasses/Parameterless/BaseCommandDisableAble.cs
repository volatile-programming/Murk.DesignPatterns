namespace Murk.Command.Parameterless
{
	/// <summary>
	/// Base class for disable able commands.
	/// Implements <see cref="System.Windows.Input.ICommand" />.
	/// </summary>
	public abstract class BaseCommandDisableAble :
		Commons.BaseInputCommand,
		ICommandDisableAble
	{
		/// <inheritdoc/>
		public abstract bool CanExecute();

		/// <inheritdoc/>
		public abstract void Execute();

		#region Interface Methods
		/// <inheritdoc/>
		public override bool CanExecute(object parameter)
		{
			if (IsDisposing)
				return false;

			return CanExecute();
		}

		/// <inheritdoc/>
		public override void Execute(object parameter)
		{
			if (IsDisposing || !CanExecute())
				return;

			Execute();
		}
		#endregion
	}
}
