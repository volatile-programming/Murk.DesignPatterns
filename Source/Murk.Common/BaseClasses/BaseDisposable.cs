namespace Murk.Commons
{
	/// <summary>
	/// A base class for disposable objects.
	/// </summary>
	public class BaseDisposable : System.IDisposable
	{
		/// <summary>
		/// Indicate whether the object is disposing or not.
		/// </summary>
		protected bool IsDisposing { get; private set; }

		/// <summary>
		/// Dispose object dependencies an events subscribers.
		/// </summary>
		/// <param name="isDisposing">Indicate dispose estate.</param>
		protected virtual void Dispose(bool isDisposing) =>
			IsDisposing = isDisposing;

		/// <inheritdoc/>
		public void Dispose()
		{
			Dispose(true);
			System.GC.SuppressFinalize(this);
		}
	}
}
