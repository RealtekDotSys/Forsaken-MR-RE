public class GenericTemplateQueue<T>
{
	private readonly global::System.Collections.Generic.Queue<T> _contexts;

	public void QueueConfig(T config)
	{
		_contexts.Enqueue(config);
	}

	public bool HasContext()
	{
		return _contexts.Count > 0;
	}

	public T DequeueNextContext()
	{
		return _contexts.Dequeue();
	}

	public void TearDown()
	{
		_contexts.Clear();
	}

	public GenericTemplateQueue()
	{
		_contexts = new global::System.Collections.Generic.Queue<T>();
	}
}
