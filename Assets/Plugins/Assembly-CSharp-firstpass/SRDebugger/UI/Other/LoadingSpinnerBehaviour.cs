namespace SRDebugger.UI.Other
{
	public class LoadingSpinnerBehaviour : global::SRF.SRMonoBehaviour
	{
		private float _dt;

		public int FrameCount = 12;

		public float SpinDuration = 0.8f;

		private void Update()
		{
			_dt += global::UnityEngine.Time.unscaledDeltaTime;
			global::UnityEngine.Vector3 eulerAngles = base.CachedTransform.localRotation.eulerAngles;
			float num = eulerAngles.z;
			float num2 = SpinDuration / (float)FrameCount;
			bool flag = false;
			while (_dt > num2)
			{
				num -= 360f / (float)FrameCount;
				_dt -= num2;
				flag = true;
			}
			if (flag)
			{
				base.CachedTransform.localRotation = global::UnityEngine.Quaternion.Euler(eulerAngles.x, eulerAngles.y, num);
			}
		}
	}
}
