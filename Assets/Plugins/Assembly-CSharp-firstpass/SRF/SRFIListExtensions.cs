namespace SRF
{
	public static class SRFIListExtensions
	{
		public static T Random<T>(this global::System.Collections.Generic.IList<T> list)
		{
			if (list.Count == 0)
			{
				throw new global::System.IndexOutOfRangeException("List needs at least one entry to call Random()");
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			return list[global::UnityEngine.Random.Range(0, list.Count)];
		}

		public static T RandomOrDefault<T>(this global::System.Collections.Generic.IList<T> list)
		{
			if (list.Count == 0)
			{
				return default(T);
			}
			return list.Random();
		}

		public static T PopLast<T>(this global::System.Collections.Generic.IList<T> list)
		{
			if (list.Count == 0)
			{
				throw new global::System.InvalidOperationException();
			}
			T result = list[list.Count - 1];
			list.RemoveAt(list.Count - 1);
			return result;
		}
	}
}
