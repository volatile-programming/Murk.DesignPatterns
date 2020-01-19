namespace Murk._PatternName_
{
	/// <summary>
	/// An example interface.
	/// </summary>
	public interface ITemplate
	{
		/// <summary>
		/// Logs a message using the given <paramref name="name"/>.
		/// </summary>
		/// <param name="name">Name to be use in the message.</param>
		/// <returns>True/False</returns>
		bool SayHello(string name);
	}
}
