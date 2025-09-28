namespace VLB_Samples
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
	public class CameraToggleBeamVisibility : global::UnityEngine.MonoBehaviour
	{
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.KeyCode m_KeyCode = global::UnityEngine.KeyCode.Space;

		private void Update()
		{
			if (global::UnityEngine.Input.GetKeyDown(m_KeyCode))
			{
				global::UnityEngine.Camera component = GetComponent<global::UnityEngine.Camera>();
				int geometryLayerID = global::VLB.Config.Instance.geometryLayerID;
				int num = 1 << geometryLayerID;
				if ((component.cullingMask & num) == num)
				{
					component.cullingMask &= ~num;
				}
				else
				{
					component.cullingMask |= num;
				}
			}
		}
	}
}
