namespace SRF.UI
{
	[global::UnityEngine.ExecuteInEditMode]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.RectTransform))]
	public abstract class ResponsiveBase : global::SRF.SRMonoBehaviour
	{
		private bool _queueRefresh;

		protected global::UnityEngine.RectTransform RectTransform => (global::UnityEngine.RectTransform)base.CachedTransform;

		protected void OnEnable()
		{
			_queueRefresh = true;
		}

		protected void OnRectTransformDimensionsChange()
		{
			_queueRefresh = true;
		}

		protected void Update()
		{
			if (_queueRefresh)
			{
				Refresh();
				_queueRefresh = false;
			}
		}

		protected abstract void Refresh();

		[global::UnityEngine.ContextMenu("Refresh")]
		private void DoRefresh()
		{
			Refresh();
		}
	}
}
