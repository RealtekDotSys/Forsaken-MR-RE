public sealed class AUDIO_DATA
{
	public class Entry
	{
		public string GameAudioEvent { get; set; }

		public AUDIO_DATA.WwiseAudioEvents WwiseAudioEvents { get; set; }
	}

	public class Event1 : AUDIO_DATA.WwiseEventInfo
	{
		public override string Name { get; set; }
	}

	public class Event2 : AUDIO_DATA.WwiseEventInfo
	{
		public override string Name { get; set; }
	}

	public class Event3 : AUDIO_DATA.WwiseEventInfo
	{
		public override string Name { get; set; }
	}

	public class Event4 : AUDIO_DATA.WwiseEventInfo
	{
		public override string Name { get; set; }
	}

	public class Event5 : AUDIO_DATA.WwiseEventInfo
	{
		public override string Name { get; set; }
	}

	public class Root
	{
		public global::System.Collections.Generic.List<AUDIO_DATA.Entry> Entries { get; set; }
	}

	public class WwiseAudioEvents
	{
		public AUDIO_DATA.Event1 Event1 { get; set; }

		public AUDIO_DATA.Event2 Event2 { get; set; }

		public AUDIO_DATA.Event3 Event3 { get; set; }

		public AUDIO_DATA.Event4 Event4 { get; set; }

		public AUDIO_DATA.Event5 Event5 { get; set; }
	}

	public abstract class WwiseEventInfo
	{
		public virtual string Name { get; set; }
	}
}
