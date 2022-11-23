namespace Murk.Common.BaseClasses
{
	/// <summary>
	/// A base class for disposable objects.
	/// </summary>
	public abstract class BaseDisposable : Interfaces.IDisposable
	{
		/// <summary>
		/// Indicate whether the object is disposing or not.
		/// </summary>
		public bool IsDisposing { get; private set; }

        /// <summary>
        /// Dispose object dependencies an events subscribers.
        /// </summary>
        /// <param name="isDisposing">Indicate dispose estate.</param>
        protected virtual void Dispose(bool isDisposing)
        {
			IsDisposing = isDisposing;
        }

		/// <inheritdoc/>
		public void Dispose()
		{
			Dispose(true);
			System.GC.SuppressFinalize(this);
		}
	}
}
