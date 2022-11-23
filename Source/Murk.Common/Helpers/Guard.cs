using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Murk.Common.Helpers
{
	public class Guard
	{
		private Guard() {}

		public static Guard Against { get; } = new Guard();

		public void Null(object parameter, string parameterName)
		{
			if (parameter is null)
				throw new System.ArgumentNullException(parameterName);
		}
	}
}
