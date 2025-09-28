namespace DigitalRuby.ThunderAndLightning
{
	public class LightningBoltSegmentGroup
	{
		public float LineWidth;

		public int StartIndex;

		public int Generation;

		public float Delay;

		public float PeakStart;

		public float PeakEnd;

		public float LifeTime;

		public float EndWidthMultiplier;

		public global::UnityEngine.Color32 Color;

		public readonly global::System.Collections.Generic.List<global::DigitalRuby.ThunderAndLightning.LightningBoltSegment> Segments = new global::System.Collections.Generic.List<global::DigitalRuby.ThunderAndLightning.LightningBoltSegment>();

		public readonly global::System.Collections.Generic.List<global::UnityEngine.Light> Lights = new global::System.Collections.Generic.List<global::UnityEngine.Light>();

		public global::DigitalRuby.ThunderAndLightning.LightningLightParameters LightParameters;

		public int SegmentCount => Segments.Count - StartIndex;

		public void Reset()
		{
			LightParameters = null;
			Segments.Clear();
			Lights.Clear();
			StartIndex = 0;
		}
	}
}
