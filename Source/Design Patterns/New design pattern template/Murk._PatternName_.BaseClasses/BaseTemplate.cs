using Murk.Commons;

namespace Murk._PatternName_
{
	/// <summary>
	/// Base class example.
	/// </summary>
	public abstract class BaseTemplate : BaseDisposable, ITemplate
	{
		/// <inheritdoc/>
		public abstract bool SayHello(string name);
	}
}
