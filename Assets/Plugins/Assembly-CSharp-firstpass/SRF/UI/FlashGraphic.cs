namespace SRF.UI
{
	[global::UnityEngine.AddComponentMenu("SRF/UI/Flash Graphic")]
	[global::UnityEngine.ExecuteInEditMode]
	public class FlashGraphic : global::UnityEngine.EventSystems.UIBehaviour, global::UnityEngine.EventSystems.IPointerDownHandler, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.IPointerUpHandler
	{
		public float DecayTime = 0.15f;

		public global::UnityEngine.Color DefaultColor = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

		public global::UnityEngine.Color FlashColor = global::UnityEngine.Color.white;

		public global::UnityEngine.UI.Graphic Target;

		public void OnPointerDown(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			Target.CrossFadeColor(FlashColor, 0f, ignoreTimeScale: true, useAlpha: true);
		}

		public void OnPointerUp(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			Target.CrossFadeColor(DefaultColor, DecayTime, ignoreTimeScale: true, useAlpha: true);
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			Target.CrossFadeColor(DefaultColor, 0f, ignoreTimeScale: true, useAlpha: true);
		}

		protected void Update()
		{
		}

		public void Flash()
		{
			Target.CrossFadeColor(FlashColor, 0f, ignoreTimeScale: true, useAlpha: true);
			Target.CrossFadeColor(DefaultColor, DecayTime, ignoreTimeScale: true, useAlpha: true);
		}
	}
}
