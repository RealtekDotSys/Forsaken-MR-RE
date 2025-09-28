namespace DigitalRuby.ThunderAndLightning
{
	public class SingleLineAttribute : global::UnityEngine.PropertyAttribute
	{
		public string Tooltip { get; private set; }

		public SingleLineAttribute(string tooltip)
		{
			Tooltip = tooltip;
		}
	}
}
