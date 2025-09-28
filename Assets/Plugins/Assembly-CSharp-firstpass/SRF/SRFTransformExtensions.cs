namespace SRF
{
	public static class SRFTransformExtensions
	{
		public static global::System.Collections.Generic.IEnumerable<global::UnityEngine.Transform> GetChildren(this global::UnityEngine.Transform t)
		{
			int i = 0;
			while (i < t.childCount)
			{
				yield return t.GetChild(i);
				int num = i + 1;
				i = num;
			}
		}

		public static void ResetLocal(this global::UnityEngine.Transform t)
		{
			t.localPosition = global::UnityEngine.Vector3.zero;
			t.localRotation = global::UnityEngine.Quaternion.identity;
			t.localScale = global::UnityEngine.Vector3.one;
		}

		public static global::UnityEngine.GameObject CreateChild(this global::UnityEngine.Transform t, string name)
		{
			global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject(name);
			gameObject.transform.parent = t;
			gameObject.transform.ResetLocal();
			gameObject.gameObject.layer = t.gameObject.layer;
			return gameObject;
		}

		public static void SetParentMaintainLocals(this global::UnityEngine.Transform t, global::UnityEngine.Transform parent)
		{
			t.SetParent(parent, worldPositionStays: false);
		}

		public static void SetLocals(this global::UnityEngine.Transform t, global::UnityEngine.Transform from)
		{
			t.localPosition = from.localPosition;
			t.localRotation = from.localRotation;
			t.localScale = from.localScale;
		}

		public static void Match(this global::UnityEngine.Transform t, global::UnityEngine.Transform from)
		{
			t.position = from.position;
			t.rotation = from.rotation;
		}

		public static void DestroyChildren(this global::UnityEngine.Transform t)
		{
			foreach (global::UnityEngine.Transform item in t)
			{
				global::UnityEngine.Object.Destroy(item.gameObject);
			}
		}
	}
}
