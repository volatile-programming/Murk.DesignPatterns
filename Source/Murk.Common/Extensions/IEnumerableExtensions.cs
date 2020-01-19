using Murk.Commons;
using System.Collections;

namespace Murk.Common.Extensions
{
	public static class IEnumerableExtensions
	{
		/// <summary>
		/// Determines whether a sequence contains any elements.
		/// </summary>
		/// <param name="enumerable">Enumerable to be verified.</param>
		/// <returns>True/False</returns>
		public static bool Any(this IEnumerable enumerable)
		{
			Guard.Against.Null(enumerable, nameof(enumerable));

			foreach (var item in enumerable)
				return true;

			return false;
		}
	}
}
