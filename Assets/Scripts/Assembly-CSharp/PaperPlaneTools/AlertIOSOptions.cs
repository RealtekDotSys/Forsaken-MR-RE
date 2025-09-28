namespace PaperPlaneTools
{
	public class AlertIOSOptions
	{
		public static global::PaperPlaneTools.AlertIOSButton.Type DefaultPositiveButton = global::PaperPlaneTools.AlertIOSButton.Type.Default;

		public static global::PaperPlaneTools.AlertIOSButton.Type DefaultNegativeButton = global::PaperPlaneTools.AlertIOSButton.Type.Default;

		public static global::PaperPlaneTools.AlertIOSButton.Type DefaultNeutralButton = global::PaperPlaneTools.AlertIOSButton.Type.Default;

		public static global::PaperPlaneTools.Alert.ButtonType DefaultPreferableButton = global::PaperPlaneTools.Alert.ButtonType.Positive;

		public static global::PaperPlaneTools.Alert.ButtonType[] DefaultButtonsAddOrder = new global::PaperPlaneTools.Alert.ButtonType[3]
		{
			global::PaperPlaneTools.Alert.ButtonType.Positive,
			global::PaperPlaneTools.Alert.ButtonType.Neutral,
			global::PaperPlaneTools.Alert.ButtonType.Negative
		};

		public global::PaperPlaneTools.AlertIOSButton.Type PositiveButton { get; set; }

		public global::PaperPlaneTools.AlertIOSButton.Type NegativeButton { get; set; }

		public global::PaperPlaneTools.AlertIOSButton.Type NeutralButton { get; set; }

		public global::PaperPlaneTools.Alert.ButtonType PreferableButton { get; set; }

		public global::PaperPlaneTools.Alert.ButtonType[] ButtonsAddOrder { get; set; }

		public AlertIOSOptions()
		{
			PositiveButton = DefaultPositiveButton;
			NegativeButton = DefaultNegativeButton;
			NeutralButton = DefaultNeutralButton;
			PreferableButton = DefaultPreferableButton;
			ButtonsAddOrder = DefaultButtonsAddOrder;
		}
	}
}
