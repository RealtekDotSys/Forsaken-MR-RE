namespace SRF.UI
{
	[global::UnityEngine.AddComponentMenu("SRF/UI/Unselectable")]
	public sealed class Unselectable : global::SRF.SRMonoBehaviour, global::UnityEngine.EventSystems.ISelectHandler, global::UnityEngine.EventSystems.IEventSystemHandler
	{
		private bool _suspectedSelected;

		public void OnSelect(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			_suspectedSelected = true;
		}

		private void Update()
		{
			if (_suspectedSelected)
			{
				if (global::UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == base.CachedGameObject)
				{
					global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
				}
				_suspectedSelected = false;
			}
		}
	}
}
