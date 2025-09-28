namespace SRF.UI
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.UI.Graphic))]
	[global::UnityEngine.ExecuteInEditMode]
	[global::UnityEngine.AddComponentMenu("SRF/UI/Inherit Colour")]
	public class InheritColour : global::SRF.SRMonoBehaviour
	{
		private global::UnityEngine.UI.Graphic _graphic;

		public global::UnityEngine.UI.Graphic From;

		private global::UnityEngine.UI.Graphic Graphic
		{
			get
			{
				if (_graphic == null)
				{
					_graphic = GetComponent<global::UnityEngine.UI.Graphic>();
				}
				return _graphic;
			}
		}

		private void Refresh()
		{
			if (!(From == null))
			{
				Graphic.color = From.canvasRenderer.GetColor();
			}
		}

		private void Update()
		{
			Refresh();
		}

		private void Start()
		{
			Refresh();
		}
	}
}
