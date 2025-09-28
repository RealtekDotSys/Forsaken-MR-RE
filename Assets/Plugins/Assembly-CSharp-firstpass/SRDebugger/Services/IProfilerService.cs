namespace SRDebugger.Services
{
	public interface IProfilerService
	{
		float AverageFrameTime { get; }

		float LastFrameTime { get; }

		global::SRDebugger.CircularBuffer<global::SRDebugger.Services.ProfilerFrame> FrameBuffer { get; }
	}
}
