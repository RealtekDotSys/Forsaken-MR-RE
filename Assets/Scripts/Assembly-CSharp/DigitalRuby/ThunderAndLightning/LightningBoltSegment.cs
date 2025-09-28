namespace DigitalRuby.ThunderAndLightning
{
	public struct LightningBoltSegment
	{
		public global::UnityEngine.Vector3 Start;

		public global::UnityEngine.Vector3 End;

		public override string ToString()
		{
			return Start.ToString() + ", " + End.ToString();
		}
	}
}
