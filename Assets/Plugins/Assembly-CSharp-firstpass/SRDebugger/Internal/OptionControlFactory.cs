namespace SRDebugger.Internal
{
	public static class OptionControlFactory
	{
		private static global::System.Collections.Generic.IList<global::SRDebugger.UI.Controls.DataBoundControl> _dataControlPrefabs;

		private static global::SRDebugger.UI.Controls.Data.ActionControl _actionControlPrefab;

		public static global::SRDebugger.UI.Controls.OptionsControlBase CreateControl(global::SRDebugger.Internal.OptionDefinition from, string categoryPrefix = null)
		{
			if (_dataControlPrefabs == null)
			{
				_dataControlPrefabs = global::UnityEngine.Resources.LoadAll<global::SRDebugger.UI.Controls.DataBoundControl>("SRDebugger/UI/Prefabs/Options");
			}
			if (_actionControlPrefab == null)
			{
				_actionControlPrefab = global::System.Linq.Enumerable.FirstOrDefault(global::UnityEngine.Resources.LoadAll<global::SRDebugger.UI.Controls.Data.ActionControl>("SRDebugger/UI/Prefabs/Options"));
			}
			if (_actionControlPrefab == null)
			{
				global::UnityEngine.Debug.LogError("[SRDebugger.Options] Cannot find ActionControl prefab.");
			}
			if (from.Property != null)
			{
				return CreateDataControl(from, categoryPrefix);
			}
			if (from.Method != null)
			{
				return CreateActionControl(from, categoryPrefix);
			}
			throw new global::System.Exception("OptionDefinition did not contain property or method.");
		}

		private static global::SRDebugger.UI.Controls.Data.ActionControl CreateActionControl(global::SRDebugger.Internal.OptionDefinition from, string categoryPrefix = null)
		{
			global::SRDebugger.UI.Controls.Data.ActionControl actionControl = SRInstantiate.Instantiate(_actionControlPrefab);
			if (actionControl == null)
			{
				global::UnityEngine.Debug.LogWarning("[SRDebugger.OptionsTab] Error creating action control from prefab");
				return null;
			}
			actionControl.SetMethod(from.Name, from.Method);
			actionControl.Option = from;
			return actionControl;
		}

		private static global::SRDebugger.UI.Controls.DataBoundControl CreateDataControl(global::SRDebugger.Internal.OptionDefinition from, string categoryPrefix = null)
		{
			global::SRDebugger.UI.Controls.DataBoundControl dataBoundControl = global::System.Linq.Enumerable.FirstOrDefault(_dataControlPrefabs, (global::SRDebugger.UI.Controls.DataBoundControl p) => p.CanBind(from.Property.PropertyType, !from.Property.CanWrite));
			if (dataBoundControl == null)
			{
				global::UnityEngine.Debug.LogWarning(global::SRF.SRFStringExtensions.Fmt("[SRDebugger.OptionsTab] Can't find data control for type {0}", from.Property.PropertyType));
				return null;
			}
			global::SRDebugger.UI.Controls.DataBoundControl dataBoundControl2 = SRInstantiate.Instantiate(dataBoundControl);
			try
			{
				string text = from.Name;
				if (!string.IsNullOrEmpty(categoryPrefix) && text.StartsWith(categoryPrefix))
				{
					text = text.Substring(categoryPrefix.Length);
				}
				dataBoundControl2.Bind(text, from.Property);
				dataBoundControl2.Option = from;
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogError(global::SRF.SRFStringExtensions.Fmt("[SRDebugger.Options] Error binding to property {0}", from.Name));
				global::UnityEngine.Debug.LogException(exception);
				global::UnityEngine.Object.Destroy(dataBoundControl2);
				dataBoundControl2 = null;
			}
			return dataBoundControl2;
		}
	}
}
