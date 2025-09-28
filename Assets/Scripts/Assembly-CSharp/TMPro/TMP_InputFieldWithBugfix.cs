namespace TMPro
{
	public class TMP_InputFieldWithBugfix : global::TMPro.TMP_InputField
	{
		protected override void Append(char input)
		{
			if (base.readOnly || global::UnityEngine.TouchScreenKeyboard.isSupported)
			{
				return;
			}
			if (base.onValidateInput != null)
			{
				input = base.onValidateInput(base.text, base.stringPositionInternal, input);
			}
			else
			{
				if (base.characterValidation == global::TMPro.TMP_InputField.CharacterValidation.CustomValidator)
				{
					input = Validate(base.text, base.stringPositionInternal, input);
					if (input != 0)
					{
						typeof(global::TMPro.TMP_InputField).GetMethod("SendOnValueChanged", global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.NonPublic).Invoke(this, null);
						UpdateLabel();
					}
					return;
				}
				if (base.characterValidation != global::TMPro.TMP_InputField.CharacterValidation.None)
				{
					input = Validate(base.text, base.stringPositionInternal, input);
				}
			}
			if (input != 0)
			{
				typeof(global::TMPro.TMP_InputField).GetMethod("Insert", global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.NonPublic).Invoke(this, new object[1] { input });
			}
		}
	}
}
