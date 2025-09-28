namespace PaperPlaneTools
{
	[global::UnityEngine.ExecuteInEditMode]
	public class Scaler : global::UnityEngine.MonoBehaviour
	{
		public float maxWidth = 1f;

		public float maxHeight = 1f;

		private void Update()
		{
			global::UnityEngine.RectTransform component = GetComponent<global::UnityEngine.RectTransform>();
			global::UnityEngine.RectTransform rectTransform = ((base.transform.parent != null) ? base.transform.parent.GetComponent<global::UnityEngine.RectTransform>() : null);
			float a = 1f;
			float width = component.rect.width;
			float num = ((rectTransform != null) ? rectTransform.rect.width : 0f);
			if (width > 0f)
			{
				a = global::UnityEngine.Mathf.Min(1f, num * maxWidth / width);
			}
			float b = 1f;
			float height = component.rect.height;
			float num2 = ((rectTransform != null) ? rectTransform.rect.height : 0f);
			if (width > 0f)
			{
				b = global::UnityEngine.Mathf.Min(1f, num2 * maxHeight / height);
			}
			float num3 = global::UnityEngine.Mathf.Min(a, b);
			base.transform.localScale = new global::UnityEngine.Vector3(num3, num3, 1f);
		}
	}
}
