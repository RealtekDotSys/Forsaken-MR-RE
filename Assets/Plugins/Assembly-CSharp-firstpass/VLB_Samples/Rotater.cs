namespace VLB_Samples
{
	public class Rotater : global::UnityEngine.MonoBehaviour
	{
		[global::UnityEngine.Serialization.FormerlySerializedAs("m_EulerSpeed")]
		public global::UnityEngine.Vector3 EulerSpeed = global::UnityEngine.Vector3.zero;

		private void Update()
		{
			global::UnityEngine.Vector3 eulerAngles = base.transform.rotation.eulerAngles;
			eulerAngles += EulerSpeed * global::UnityEngine.Time.deltaTime;
			base.transform.rotation = global::UnityEngine.Quaternion.Euler(eulerAngles);
		}
	}
}
