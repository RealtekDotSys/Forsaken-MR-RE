public static class IListExtensions
{
	public static void Shuffle<T>(this global::System.Collections.Generic.IList<T> ts)
	{
		int count = ts.Count;
		int num = count - 1;
		for (int i = 0; i < num; i++)
		{
			int index = global::UnityEngine.Random.Range(i, count);
			T value = ts[i];
			ts[i] = ts[index];
			ts[index] = value;
		}
	}
}
