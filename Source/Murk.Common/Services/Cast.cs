using System;

namespace Murk.Commons
{
	public class Cast
	{
		internal Cast() {}

		private static Cast _instance;
		public static Cast Type =>
			_instance ?? (_instance = new Cast());

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
