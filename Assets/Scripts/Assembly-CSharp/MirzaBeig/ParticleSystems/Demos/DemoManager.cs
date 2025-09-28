namespace MirzaBeig.ParticleSystems.Demos
{
	public class DemoManager : global::UnityEngine.MonoBehaviour
	{
		public enum ParticleMode
		{
			looping = 0,
			oneshot = 1
		}

		public enum Level
		{
			none = 0,
			basic = 1
		}

		public global::UnityEngine.Transform cameraRotationTransform;

		public global::UnityEngine.Transform cameraTranslationTransform;

		public global::UnityEngine.Vector3 cameraLookAtPosition = new global::UnityEngine.Vector3(0f, 3f, 0f);

		public global::MirzaBeig.ParticleSystems.Demos.MouseFollow mouse;

		private global::UnityEngine.Vector3 targetCameraPosition;

		private global::UnityEngine.Vector3 targetCameraRotation;

		private global::UnityEngine.Vector3 cameraPositionStart;

		private global::UnityEngine.Vector3 cameraRotationStart;

		private global::UnityEngine.Vector2 input;

		private global::UnityEngine.Vector3 cameraRotation;

		public float cameraMoveAmount = 2f;

		public float cameraRotateAmount = 2f;

		public float cameraMoveSpeed = 12f;

		public float cameraRotationSpeed = 12f;

		public global::UnityEngine.Vector2 cameraAngleLimits = new global::UnityEngine.Vector2(-8f, 60f);

		public global::UnityEngine.GameObject[] levels;

		public global::MirzaBeig.ParticleSystems.Demos.DemoManager.Level currentLevel = global::MirzaBeig.ParticleSystems.Demos.DemoManager.Level.basic;

		public global::MirzaBeig.ParticleSystems.Demos.DemoManager.ParticleMode particleMode;

		public bool advancedRendering = true;

		public global::UnityEngine.UI.Toggle loopingParticleModeToggle;

		public global::UnityEngine.UI.Toggle oneshotParticleModeToggle;

		public global::UnityEngine.UI.Toggle advancedRenderingToggle;

		public global::UnityEngine.UI.Toggle mouseParticlesToggle;

		private global::UnityEngine.UI.Toggle[] levelToggles;

		public global::UnityEngine.UI.ToggleGroup levelTogglesContainer;

		private global::MirzaBeig.ParticleSystems.Demos.LoopingParticleSystemsManager loopingParticleSystems;

		private global::MirzaBeig.ParticleSystems.Demos.OneshotParticleSystemsManager oneshotParticleSystems;

		public global::UnityEngine.GameObject ui;

		public global::UnityEngine.UI.Text particleCountText;

		public global::UnityEngine.UI.Text currentParticleSystemText;

		public global::UnityEngine.UI.Text particleSpawnInstructionText;

		public global::UnityEngine.UI.Slider timeScaleSlider;

		public global::UnityEngine.UI.Text timeScaleSliderValueText;

		public global::UnityEngine.Camera mainCamera;

		public global::UnityEngine.MonoBehaviour[] mainCameraPostEffects;

		private global::UnityEngine.Vector3 cameraPositionSmoothDampVelocity;

		private global::UnityEngine.Vector3 cameraRotationSmoothDampVelocity;

		private void Awake()
		{
			loopingParticleSystems = global::UnityEngine.Object.FindObjectOfType<global::MirzaBeig.ParticleSystems.Demos.LoopingParticleSystemsManager>();
			oneshotParticleSystems = global::UnityEngine.Object.FindObjectOfType<global::MirzaBeig.ParticleSystems.Demos.OneshotParticleSystemsManager>();
			loopingParticleSystems.Init();
			oneshotParticleSystems.Init();
		}

		private void Start()
		{
			cameraPositionStart = cameraTranslationTransform.localPosition;
			cameraRotationStart = cameraRotationTransform.localEulerAngles;
			ResetCameraTransformTargets();
			switch (particleMode)
			{
			case global::MirzaBeig.ParticleSystems.Demos.DemoManager.ParticleMode.looping:
				SetToLoopingParticleMode(set: true);
				loopingParticleModeToggle.isOn = true;
				oneshotParticleModeToggle.isOn = false;
				break;
			case global::MirzaBeig.ParticleSystems.Demos.DemoManager.ParticleMode.oneshot:
				SetToOneshotParticleMode(set: true);
				loopingParticleModeToggle.isOn = false;
				oneshotParticleModeToggle.isOn = true;
				break;
			default:
				global::UnityEngine.MonoBehaviour.print("Unknown case.");
				break;
			}
			SetAdvancedRendering(advancedRendering);
			advancedRenderingToggle.isOn = advancedRendering;
			levelToggles = levelTogglesContainer.GetComponentsInChildren<global::UnityEngine.UI.Toggle>(includeInactive: true);
			for (int i = 0; i < levels.Length; i++)
			{
				if (i == (int)currentLevel)
				{
					levels[i].SetActive(value: true);
					levelToggles[i].isOn = true;
				}
				else
				{
					levels[i].SetActive(value: false);
					levelToggles[i].isOn = false;
				}
			}
			UpdateCurrentParticleSystemNameText();
			timeScaleSlider.onValueChanged.AddListener(OnTimeScaleSliderValueChanged);
			OnTimeScaleSliderValueChanged(timeScaleSlider.value);
			mouseParticlesToggle.isOn = mouse.gameObject.activeSelf;
		}

		public void OnTimeScaleSliderValueChanged(float value)
		{
			global::UnityEngine.Time.timeScale = value;
			timeScaleSliderValueText.text = value.ToString("0.00");
		}

		public void SetToLoopingParticleMode(bool set)
		{
			if (set)
			{
				oneshotParticleSystems.Clear();
				loopingParticleSystems.gameObject.SetActive(value: true);
				oneshotParticleSystems.gameObject.SetActive(value: false);
				particleSpawnInstructionText.gameObject.SetActive(value: false);
				particleMode = global::MirzaBeig.ParticleSystems.Demos.DemoManager.ParticleMode.looping;
				UpdateCurrentParticleSystemNameText();
			}
		}

		public void SetToOneshotParticleMode(bool set)
		{
			if (set)
			{
				loopingParticleSystems.gameObject.SetActive(value: false);
				oneshotParticleSystems.gameObject.SetActive(value: true);
				particleSpawnInstructionText.gameObject.SetActive(value: true);
				particleMode = global::MirzaBeig.ParticleSystems.Demos.DemoManager.ParticleMode.oneshot;
				UpdateCurrentParticleSystemNameText();
			}
		}

		public void SetLevel(global::MirzaBeig.ParticleSystems.Demos.DemoManager.Level level)
		{
			for (int i = 0; i < levels.Length; i++)
			{
				if (i == (int)level)
				{
					levels[i].SetActive(value: true);
				}
				else
				{
					levels[i].SetActive(value: false);
				}
			}
			currentLevel = level;
		}

		public void SetLevelFromToggle(global::UnityEngine.UI.Toggle toggle)
		{
			if (toggle.isOn)
			{
				SetLevel((global::MirzaBeig.ParticleSystems.Demos.DemoManager.Level)global::System.Array.IndexOf(levelToggles, toggle));
			}
		}

		public void SetAdvancedRendering(bool value)
		{
			advancedRendering = value;
			mainCamera.allowHDR = value;
			for (int i = 0; i < mainCameraPostEffects.Length; i++)
			{
				if ((bool)mainCameraPostEffects[i])
				{
					mainCameraPostEffects[i].enabled = value;
				}
			}
		}

		public void SetMouseParticlesRendering(bool value)
		{
			mouse.gameObject.SetActive(value);
		}

		public static global::UnityEngine.Vector3 DampVector3(global::UnityEngine.Vector3 from, global::UnityEngine.Vector3 to, float speed, float dt)
		{
			return global::UnityEngine.Vector3.Lerp(from, to, 1f - global::UnityEngine.Mathf.Exp((0f - speed) * dt));
		}

		private void Update()
		{
			input.x = global::UnityEngine.Input.GetAxis("Horizontal");
			input.y = global::UnityEngine.Input.GetAxis("Vertical");
			if (global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.LeftShift))
			{
				targetCameraPosition.z += input.y * cameraMoveAmount;
				targetCameraPosition.z = global::UnityEngine.Mathf.Clamp(targetCameraPosition.z, -6.3f, -1f);
			}
			else
			{
				targetCameraRotation.y += input.x * cameraRotateAmount;
				targetCameraRotation.x += input.y * cameraRotateAmount;
				targetCameraRotation.x = global::UnityEngine.Mathf.Clamp(targetCameraRotation.x, cameraAngleLimits.x, cameraAngleLimits.y);
			}
			cameraTranslationTransform.localPosition = global::UnityEngine.Vector3.SmoothDamp(cameraTranslationTransform.localPosition, targetCameraPosition, ref cameraPositionSmoothDampVelocity, 1f / cameraMoveSpeed, float.PositiveInfinity, global::UnityEngine.Time.unscaledDeltaTime);
			cameraRotation = global::UnityEngine.Vector3.SmoothDamp(cameraRotation, targetCameraRotation, ref cameraRotationSmoothDampVelocity, 1f / cameraRotationSpeed, float.PositiveInfinity, global::UnityEngine.Time.unscaledDeltaTime);
			cameraRotationTransform.localEulerAngles = cameraRotation;
			cameraTranslationTransform.LookAt(cameraLookAtPosition);
			if (global::UnityEngine.Input.GetAxis("Mouse ScrollWheel") < 0f)
			{
				Next();
			}
			else if (global::UnityEngine.Input.GetAxis("Mouse ScrollWheel") > 0f)
			{
				Previous();
			}
			if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.U))
			{
				ui.SetActive(!ui.activeSelf);
			}
			if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.O))
			{
				if (particleMode == global::MirzaBeig.ParticleSystems.Demos.DemoManager.ParticleMode.looping)
				{
					SetToOneshotParticleMode(set: true);
				}
				else
				{
					SetToLoopingParticleMode(set: true);
				}
			}
			if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.L))
			{
				SetLevel((global::MirzaBeig.ParticleSystems.Demos.DemoManager.Level)((int)(currentLevel + 1) % global::System.Enum.GetNames(typeof(global::MirzaBeig.ParticleSystems.Demos.DemoManager.Level)).Length));
			}
			else
			{
				global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.R);
			}
			if (particleMode == global::MirzaBeig.ParticleSystems.Demos.DemoManager.ParticleMode.oneshot)
			{
				global::UnityEngine.Vector3 mousePosition = global::UnityEngine.Input.mousePosition;
				if (global::UnityEngine.Input.GetMouseButtonDown(0))
				{
					global::MirzaBeig.ParticleSystems.Demos.CameraShake cameraShake = global::UnityEngine.Object.FindObjectOfType<global::MirzaBeig.ParticleSystems.Demos.CameraShake>();
					cameraShake.Add(0.2f, 5f, 0.2f, global::MirzaBeig.ParticleSystems.Demos.CameraShakeTarget.Position, global::MirzaBeig.ParticleSystems.Demos.CameraShakeAmplitudeCurve.FadeInOut25);
					cameraShake.Add(4f, 5f, 0.5f, global::MirzaBeig.ParticleSystems.Demos.CameraShakeTarget.Rotation, global::MirzaBeig.ParticleSystems.Demos.CameraShakeAmplitudeCurve.FadeInOut25);
					oneshotParticleSystems.InstantiateParticlePrefab(mousePosition, mouse.distanceFromCamera);
				}
				if (global::UnityEngine.Input.GetMouseButton(1))
				{
					oneshotParticleSystems.InstantiateParticlePrefab(mousePosition, mouse.distanceFromCamera);
				}
			}
			if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.R))
			{
				ResetCameraTransformTargets();
			}
		}

		private void LateUpdate()
		{
			particleCountText.text = "PARTICLE COUNT: ";
			if (particleMode == global::MirzaBeig.ParticleSystems.Demos.DemoManager.ParticleMode.looping)
			{
				particleCountText.text += loopingParticleSystems.GetParticleCount();
			}
			else if (particleMode == global::MirzaBeig.ParticleSystems.Demos.DemoManager.ParticleMode.oneshot)
			{
				particleCountText.text += oneshotParticleSystems.GetParticleCount();
			}
		}

		private void ResetCameraTransformTargets()
		{
			targetCameraPosition = cameraPositionStart;
			targetCameraRotation = cameraRotationStart;
		}

		private void UpdateCurrentParticleSystemNameText()
		{
			if (particleMode == global::MirzaBeig.ParticleSystems.Demos.DemoManager.ParticleMode.looping)
			{
				currentParticleSystemText.text = loopingParticleSystems.GetCurrentPrefabName(shorten: true);
			}
			else if (particleMode == global::MirzaBeig.ParticleSystems.Demos.DemoManager.ParticleMode.oneshot)
			{
				currentParticleSystemText.text = oneshotParticleSystems.GetCurrentPrefabName(shorten: true);
			}
		}

		public void Next()
		{
			if (particleMode == global::MirzaBeig.ParticleSystems.Demos.DemoManager.ParticleMode.looping)
			{
				loopingParticleSystems.Next();
			}
			else if (particleMode == global::MirzaBeig.ParticleSystems.Demos.DemoManager.ParticleMode.oneshot)
			{
				oneshotParticleSystems.Next();
			}
			UpdateCurrentParticleSystemNameText();
		}

		public void Previous()
		{
			if (particleMode == global::MirzaBeig.ParticleSystems.Demos.DemoManager.ParticleMode.looping)
			{
				loopingParticleSystems.Previous();
			}
			else if (particleMode == global::MirzaBeig.ParticleSystems.Demos.DemoManager.ParticleMode.oneshot)
			{
				oneshotParticleSystems.Previous();
			}
			UpdateCurrentParticleSystemNameText();
		}
	}
}
