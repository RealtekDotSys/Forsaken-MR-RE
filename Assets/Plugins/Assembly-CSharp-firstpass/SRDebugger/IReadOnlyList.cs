namespace SRDebugger
{
	public interface IReadOnlyList<T> : global::System.Collections.Generic.IEnumerable<T>, global::System.Collections.IEnumerable
	{
		int Count { get; }

		T this[int index] { get; }
	}
}
