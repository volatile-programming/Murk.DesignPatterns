using Murk.Common.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Murk.Commons
{
	public class Guard
	{
		internal Guard() {}

		private static Guard _instance;
		public static Guard Against =>
			_instance ?? (_instance = new Guard());

		public void Null(object parameter, string parameterName)
		{
			if (parameter is null)
				throw new System.ArgumentNullException(parameterName);
		}

		public void NullOrEmpty<T>(IEnumerable<T> parameter,
			string parameterName)
		{
			Null(parameter, parameterName);

			if (!parameter.Any())
				throw new System.ArgumentException(
					"None arguments were given.",
					parameterName);
		}

		public void NullOrEmpty(IEnumerable parameter,
			string parameterName)
		{
			Null(parameter, parameterName);

			if (!parameter.Any())
				throw new System.ArgumentException(
					"None arguments were given.",
					parameterName);
		}
	}
}
