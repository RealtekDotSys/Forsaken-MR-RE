namespace MirzaBeig.ParticleSystems.Demos
{
	[global::System.Serializable]
	public class DemoManager_XPTitles : global::UnityEngine.MonoBehaviour
	{
		private global::MirzaBeig.ParticleSystems.Demos.LoopingParticleSystemsManager list;

		public global::UnityEngine.UI.Text particleCountText;

		public global::UnityEngine.UI.Text currentParticleSystemText;

		private global::MirzaBeig.ParticleSystems.Rotator cameraRotator;

		private void Awake()
		{
			(list = GetComponent<global::MirzaBeig.ParticleSystems.Demos.LoopingParticleSystemsManager>()).Init();
		}

		private void Start()
		{
			cameraRotator = global::UnityEngine.Camera.main.GetComponentInParent<global::MirzaBeig.ParticleSystems.Rotator>();
			updateCurrentParticleSystemNameText();
		}

		public void ToggleRotation()
		{
			cameraRotator.enabled = !cameraRotator.enabled;
		}

		public void ResetRotation()
		{
			cameraRotator.transform.eulerAngles = global::UnityEngine.Vector3.zero;
		}

		private void Update()
		{
			if (global::UnityEngine.Input.GetAxis("Mouse ScrollWheel") < 0f)
			{
				Next();
			}
			else if (global::UnityEngine.Input.GetAxis("Mouse ScrollWheel") > 0f)
			{
				previous();
			}
		}

		private void LateUpdate()
		{
			if ((bool)particleCountText)
			{
				particleCountText.text = "PARTICLE COUNT: ";
				particleCountText.text += list.GetParticleCount();
			}
		}

		public void Next()
		{
			list.Next();
			updateCurrentParticleSystemNameText();
		}

		public void previous()
		{
			list.Previous();
			updateCurrentParticleSystemNameText();
		}

		private void updateCurrentParticleSystemNameText()
		{
			if ((bool)currentParticleSystemText)
			{
				currentParticleSystemText.text = list.GetCurrentPrefabName(shorten: true);
			}
		}
	}
}
