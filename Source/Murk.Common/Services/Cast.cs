using System;

namespace Murk.Common
{
	public class Cast
	{
		private Cast(){}

		public static Cast Type { get; } = new Cast();

		public T Any<T>(object objectToCast)
		{
			if (objectToCast is T castedObject)
				return castedObject;

			throw new InvalidCastException(
				$"Cannot Cast typed {objectToCast.GetType().FullName} to " +
				$"{typeof(T).FullName}");
		}
	}
}
